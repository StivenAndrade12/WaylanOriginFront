using System;
using System.Collections.Generic;
using System.Linq;
using WaylanOrigin.Client.Models;

namespace WaylanOrigin.Client
{
    public class CartState
    {
        public List<CartItem> Items { get; } = new();

        public int CartCount => Items.Sum(i => i.Cantidad);

        public decimal TotalPrice => Items.Sum(i => i.Product.Precio * i.Cantidad);

        public event Action? OnChange;

        public void AddProduct(Product product)
        {
            var existing = Items.FirstOrDefault(i => i.Product.Id == product.Id);
            if (existing != null)
            {
                existing.Cantidad++;
            }
            else
            {
                Items.Add(new CartItem { Product = product, Cantidad = 1 });
            }
            NotifyStateChanged();
        }

        public void RemoveProduct(string productId)
        {
            var item = Items.FirstOrDefault(i => i.Product.Id == productId);
            if (item != null)
            {
                Items.Remove(item);
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
                NotifyStateChanged();
            }
        }

        public void Clear()
        {
            Items.Clear();
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
