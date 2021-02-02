using MvcCore.data;
using MvcCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCore.Repositories
{
    public class RepositoryCochesMYSQL : IRepositoryCoches
    {
        CochesContext context;

        public RepositoryCochesMYSQL(CochesContext context)
        {
            this.context = context;
        }
        public List<Coche> GetCoches()
        {
            var consulta = from datos in this.context.Coches select datos;

            return consulta.ToList();
        }

        public Coche GetCocheId(int idcoche)
        {
            var consulta = from datos in this.context.Coches.Where(z => z.IdCoche == idcoche)
                           select datos;
            return consulta.FirstOrDefault();
        }

        public int GetMaxId()
        {
            var consulta = (from datos in this.context.Coches
                            select datos).Max(x => x.IdCoche);
            return consulta;
        }

        public void InsertCoche(string marca, string modelo, string conductor, string imagen)
        {
            Coche coche = new Coche();
            coche.IdCoche = this.GetMaxId()+1;
            coche.Marca = marca;
            coche.Modelo = modelo;
            coche.Conductor = conductor;
            coche.Imagen = imagen;
            this.context.Add(coche);
            this.context.SaveChanges();
        }

        public void UpdateCoche(int idcoche, String marca, String modelo, String conductor, String imagen)
        {
            Coche coche = this.GetCocheId(idcoche);
            coche.Marca = marca;
            coche.Modelo = modelo;
            coche.Conductor = conductor;
            coche.Imagen = imagen;
            this.context.SaveChanges();
        }

        public void DeleteCoche(int idcoche)
        {
            Coche coche = this.GetCocheId(idcoche);
            this.context.Coches.Remove(coche);
            this.context.SaveChanges();
        }

        public List<Coche> BuscarCocheModelo(String modelo)
        {
            var consulta = from datos in this.context.Coches.Where(z => z.Modelo == modelo) select datos;

            return consulta.ToList();
        }

        
    }
}
