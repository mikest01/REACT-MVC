namespace Bodega
{
    public class Movimientos
    {
        public int Id { get; set; }
        public int ArticuloId { get; set; }
        public string? TipoMovimiento { get; set; } // Entrada o Salida
        public int Cantidad { get; set; }
        public DateTime FechaHora { get; set; }

        public Articulos? Articulos { get; set; }
    }
}
