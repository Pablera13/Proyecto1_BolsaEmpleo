namespace DataAccess.Models
{
    public class CandidatoOferta
    {
        public int CandidatoId { get; set; }
        public Candidato Candidato { get; set; }
        public int OfertaId { get; set; }
        public Oferta Oferta { get; set; }
    }
}
