namespace DataAccess.Models
{
    public class CandidatoHabilidad
    {
        public int CandidatoId { get; set; }
        public Candidato Candidato { get; set; }
        public int HabilidadId { get; set; }
        public Habilidad Habilidad { get; set; }
    }
}
