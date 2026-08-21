namespace WaylanOrigin.Client.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
        public string EmailCliente { get; set; } = string.Empty;
        public double Total { get; set; }
        public string Estado { get; set; } = "Pendiente";
        public string EstadoPago { get; set; } = "APPROVED";
        public DateTime Fecha { get; set; }

        public List<OrderDetail> Detalles { get; set; } = new List<OrderDetail>();
    }
}
