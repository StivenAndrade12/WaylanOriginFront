namespace WaylanOrigin.Client.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int PedidoId { get; set; }
        public string ProductoId { get; set; } = string.Empty;
        public string NombreProducto { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public int PrecioUnitario { get; set; }
    }
}
