﻿namespace DataAccess.Models
{
    public class Empresa
    {

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public int Telefono { get; set; }

        //relaciones

        public List<Oferta> ofertas { get; set; }
  
    }
}
