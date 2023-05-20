using DataAccess.Models;
using DataAccess.Response_Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.RequestObjects
{
    public class CandidatoVmGET
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }

        public string Fecha_Nacimiento { get; set; }
        public string Direccion { get; set; }
        public int Telefono { get; set; }
        public string Descripcion { get; set; }

        //Relaciones
        public List<FormacionVmGET> Formaciones { get; set; }
        public List<CandidatoHabilidadVmGET> Habilidades { get; set; }
        public List<CandidatoOfertaVmGET> Ofertas { get; set; }

        public CandidatoVmGET()
        {
            Formaciones = new List<FormacionVmGET>();
            Habilidades = new List<CandidatoHabilidadVmGET>();
            Ofertas = new List<CandidatoOfertaVmGET>();

        }

    }
}
