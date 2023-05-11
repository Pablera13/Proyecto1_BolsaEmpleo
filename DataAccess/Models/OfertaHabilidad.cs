namespace DataAccess.Models
{
    public class OfertaHabilidad
    {
        public int OfertaId { get; set; }
        public Oferta Oferta { get; set; }
        public int HabilidadId { get; set; }
        public Habilidad Habilidad { get; set; }
    }
}
