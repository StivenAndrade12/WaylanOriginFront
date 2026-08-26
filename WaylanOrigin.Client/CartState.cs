using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using WaylanOrigin.Client.Models;

namespace WaylanOrigin.Client
{
    public class CartState
    {
        private IJSRuntime? _js;
        private string _activeUserEmail = "guest";

        public List<CartItem> Items { get; } = new();

        public int CartCount => Items.Sum(i => i.Cantidad);

        public decimal TotalPrice => Items.Sum(i => i.Product.Precio * i.Cantidad);

        public event Action? OnChange;
        public event Action? OnCartRequested;

        public void RequestOpenCart() => OnCartRequested?.Invoke();

        public async Task InitializeCartForUserAsync(IJSRuntime js, string? userEmail)
        {
            _js = js;
            _activeUserEmail = string.IsNullOrWhiteSpace(userEmail) ? "guest" : userEmail.Trim().ToLowerInvariant();
            await LoadCartFromStorageAsync();
        }

        private string StorageKey => $"waylan_cart_{_activeUserEmail}";

        private async Task LoadCartFromStorageAsync()
        {
            if (_js == null) return;
            try
            {
                var json = await _js.InvokeAsync<string>("localStorage.getItem", StorageKey);
                Items.Clear();
                if (!string.IsNullOrEmpty(json))
                {
                    var loadedItems = System.Text.Json.JsonSerializer.Deserialize<List<CartItem>>(json);
                    if (loadedItems != null && loadedItems.Any())
                    {
                        Items.AddRange(loadedItems);
                    }
                }
                NotifyStateChanged();
            }
            catch
            {
            }
        }

        private async Task SaveCartToStorageAsync()
        {
            if (_js == null) return;
            try
            {
                var json = System.Text.Json.JsonSerializer.Serialize(Items);
                await _js.InvokeVoidAsync("localStorage.setItem", StorageKey, json);
            }
            catch
            {
            }
        }

        public void AddProduct(Product product)
        {
            if (product.Stock <= 0)
            {
                return; // Cannot add out of stock items
            }

            var existing = Items.FirstOrDefault(i => i.Product.Id == product.Id);
            if (existing != null)
            {
                if (existing.Cantidad >= product.Stock)
                {
                    NotifyStateChanged();
                    return; // Cannot exceed available stock
                }
                existing.Cantidad++;
            }
            else
            {
                Items.Add(new CartItem { Product = product, Cantidad = 1 });
            }
            _ = SaveCartToStorageAsync();
            NotifyStateChanged();
        }

        public void RemoveProduct(string productId)
        {
            var item = Items.FirstOrDefault(i => i.Product.Id == productId);
            if (item != null)
            {
                Items.Remove(item);
                _ = SaveCartToStorageAsync();
                NotifyStateChanged();
            }
        }

        public void DecreaseProduct(string productId)
        {
            var existing = Items.FirstOrDefault(i => i.Product.Id == productId);
            if (existing != null)
            {
                existing.Cantidad--;
                if (existing.Cantidad <= 0)
                {
                    Items.Remove(existing);
                }
                _ = SaveCartToStorageAsync();
                NotifyStateChanged();
            }
        }

        public void Clear()
        {
            Items.Clear();
            _ = SaveCartToStorageAsync();
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }

    public class CartItem
    {
        public Product Product { get; set; } = new();
        public int Cantidad { get; set; }
    }
}
