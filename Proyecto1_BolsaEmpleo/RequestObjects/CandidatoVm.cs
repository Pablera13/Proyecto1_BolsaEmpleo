namespace Proyecto1_BolsaEmpleo.RequestObjects
{
    public class CandidatoVm
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }

        public string Fecha_Nacimiento { get; set; }
        public string Direccion { get; set; }
        public int Telefono { get; set; }
        public string Descripcion { get; set; }

        //public List<HabilidadVm> habilidades { get; set; }

    }
}
