namespace DataAccess.Models
{
    public class Habilidad
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public List<CandidatoHabilidad> CandidatoHabilidades { get; set; }
        public List<OfertaHabilidad> OfertaHabilidades { get; set; }
    }
}
