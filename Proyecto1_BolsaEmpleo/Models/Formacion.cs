namespace Proyecto1_BolsaEmpleo.Models
{
    public class Formacion
    {
        public int Id { get; set; }

        public string Nombre { get; set; }
        public int Años_Estudio { get; set; }
        public string Fecha_Culminacion { get; set; }

        //Relaciones
        public int CandidatoId { get; set; }

        public Candidato Candidato { get; set; }
    }
}
