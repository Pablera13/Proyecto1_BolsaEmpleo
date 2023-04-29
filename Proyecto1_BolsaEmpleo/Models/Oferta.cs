namespace Proyecto1_BolsaEmpleo.Models
{
    public class Oferta
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }

        //relaciones
        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }
       
    }
}
