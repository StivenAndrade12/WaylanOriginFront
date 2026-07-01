namespace WaylanOrigin.Client
{
    public class CartState
    {
        private int _cartCount = 0;

        public int CartCount
        {
            get => _cartCount;
            set
            {
                if (_cartCount != value)
                {
                    _cartCount = value;
                    NotifyStateChanged();
                }
            }
        }

        public event Action? OnChange;

        public void Increment()
        {
            CartCount++;
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
