using DataAccess.RequestObjects;
using DataAccess.Response_Objects;

namespace DataAccess.Models
{
    public class Oferta
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }

        //relaciones
        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }

        public List<OfertaHabilidad> OfertaHabilidades { get; set; }
        public List<CandidatoOferta> CandidatoOfertas { get; set; }

        public Oferta()
        {
            OfertaHabilidades = new List<OfertaHabilidad>();

        }

    }
}
