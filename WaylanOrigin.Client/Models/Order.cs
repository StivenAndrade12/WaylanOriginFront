namespace WaylanOrigin.Client.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public string EmailCliente { get; set; } = string.Empty;
        public int Total { get; set; }
        public string Estado { get; set; } = "Pendiente";

        public List<OrderDetail> Detalles { get; set; } = new List<OrderDetail>();
    }
}
