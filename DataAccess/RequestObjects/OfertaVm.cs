namespace DataAccess.RequestObjects
{
    public class OfertaVm
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }

        //relaciones
        public int EmpresaId { get; set; }
    }
}
