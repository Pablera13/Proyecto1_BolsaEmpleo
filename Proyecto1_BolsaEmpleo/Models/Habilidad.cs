namespace Proyecto1_BolsaEmpleo.Models
{
    public class Habilidad
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public List<Candidato> candidatos { get; set; }
        public List<Empresa> empresas { get; set; }
    }
}
