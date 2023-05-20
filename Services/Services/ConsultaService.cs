using DataAccess.Data;
using DataAccess.Models;
using DataAccess.RequestObjects;
using DataAccess.Response_Objects;
using Microsoft.EntityFrameworkCore;
using Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class ConsultaService : IConsultaService
    {
        private readonly MyApiContext _context;

        public ConsultaService(MyApiContext context)
        {
            _context = context;
        }
        public async Task<List<CandidatoVmGET>> Ver_potenciales_candidatos(int id_oferta)
        {
            List<Candidato> listaCandidato = await _context.Candidato
           .Include(o => o.formaciones)
           .Include(o => o.CandidatoHabilidades)
           .Include(o => o.CandidatoOfertas)
            .ToListAsync();

            List<CandidatoVmGET> listaCandidatoVm = new List<CandidatoVmGET>();

            var oferta = await _context.Oferta
           .Include(c => c.OfertaHabilidades).FirstOrDefaultAsync(c => c.Id == id_oferta);

            if (oferta == null)
            {
                return listaCandidatoVm;
            }

            var habilidades = oferta.OfertaHabilidades.Select(ch => ch.HabilidadId).ToArray();

            foreach (var habilidadId in habilidades)
            {
                foreach (Candidato candidato in listaCandidato)
                {
                    CandidatoVmGET newCandidatoVmGET = new CandidatoVmGET();

                    newCandidatoVmGET.Id = candidato.Id;
                    newCandidatoVmGET.Nombre = candidato.Nombre;
                    newCandidatoVmGET.Apellido1 = candidato.Apellido1;
                    newCandidatoVmGET.Apellido2 = candidato.Apellido2;
                    newCandidatoVmGET.Fecha_Nacimiento = candidato.Fecha_Nacimiento;
                    newCandidatoVmGET.Direccion = candidato.Direccion;
                    newCandidatoVmGET.Telefono = candidato.Telefono;
                    newCandidatoVmGET.Descripcion = candidato.Descripcion;

                    foreach (Formacion formacion in candidato.formaciones)
                    {
                        FormacionVmGET newFormacionVmGET = new FormacionVmGET();

                        newFormacionVmGET.Nombre = formacion.Nombre;
                        newFormacionVmGET.Años_Estudio = formacion.Años_Estudio;
                        newFormacionVmGET.Fecha_Culminacion = formacion.Fecha_Culminacion;

                        newCandidatoVmGET.Formaciones.Add(newFormacionVmGET);

                        foreach (CandidatoHabilidad candidatoHabilidad in candidato.CandidatoHabilidades)
                        {
                            CandidatoHabilidadVmGET newCandidatoHabilidadVmGET = new CandidatoHabilidadVmGET();

                            Habilidad habilidad = await _context.Habilidad
                            .FirstOrDefaultAsync(c => c.Id == candidatoHabilidad.HabilidadId);

                            newCandidatoHabilidadVmGET.Nombre = habilidad.Nombre;

                            newCandidatoVmGET.Habilidades.Add(newCandidatoHabilidadVmGET);

                            foreach (CandidatoOferta candidatoOferta in candidato.CandidatoOfertas)
                            {
                                CandidatoOfertaVmGET newCandidatoOfertaVmGET = new CandidatoOfertaVmGET();

                                Oferta oferta2 = await _context.Oferta
                                .FirstOrDefaultAsync(c => c.Id == candidatoOferta.OfertaId);

                                newCandidatoOfertaVmGET.OfertaId = oferta2.Id;
                                newCandidatoOfertaVmGET.Descripcion = oferta2.Descripcion;

                                newCandidatoVmGET.Ofertas.Add(newCandidatoOfertaVmGET);

                                if (candidatoHabilidad.HabilidadId == habilidadId)
                                {
                                    listaCandidatoVm.Add(newCandidatoVmGET);
                                }

                            }

                        }

                    }
                }
            }
            return listaCandidatoVm;
        }

        public async Task<List<OfertaVmGET>> Ver_potenciales_ofertas(int id_Candidato)
        {
            List<Oferta> listaOferta = await _context.Oferta
           .Include(o => o.OfertaHabilidades)
           .ToListAsync();

            List<OfertaVmGET> listaOfertaVm = new List<OfertaVmGET>();

            var candidato = await _context.Candidato
           .Include(c => c.CandidatoHabilidades).FirstOrDefaultAsync(c => c.Id == id_Candidato);

            if (candidato == null)
            {
                return listaOfertaVm;
            }

            var habilidades = candidato.CandidatoHabilidades.Select(ch => ch.HabilidadId).ToArray();

            foreach (var habilidadId in habilidades)
            {
                foreach (Oferta oferta in listaOferta)
                {
                    OfertaVmGET newOfertaVm = new OfertaVmGET();

                    Empresa empresa = await _context.Empresa
                    .FirstOrDefaultAsync(c => c.Id == oferta.EmpresaId);

                    newOfertaVm.Id = oferta.Id;
                    newOfertaVm.Descripcion = oferta.Descripcion;
                    newOfertaVm.Empresa = empresa.Nombre;

                    foreach (OfertaHabilidad ofertahabilidad in oferta.OfertaHabilidades)
                    {
                        OfertaHabilidadVmGET newOfertaHabilidadVm = new OfertaHabilidadVmGET();

                        Habilidad habilidad = await _context.Habilidad
                        .FirstOrDefaultAsync(c => c.Id == ofertahabilidad.HabilidadId);

                        newOfertaHabilidadVm.Nombre = habilidad.Nombre;

                        newOfertaVm.Habilidades.Add(newOfertaHabilidadVm);

                        if (ofertahabilidad.HabilidadId == habilidadId)
                        {
                            listaOfertaVm.Add(newOfertaVm);
                        }
                    }
                }
            }
            return listaOfertaVm;
        }

    }
}
