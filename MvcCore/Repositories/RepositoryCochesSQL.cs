using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MvcCore.data;
using MvcCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCore.Repositories
{
    public class RepositoryCochesSQL : IRepositoryCoches
    {
        CochesContext context;

        public RepositoryCochesSQL(CochesContext context) 
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

        public void InsertCoche(String marca, String modelo, String conductor, String imagen)
        {
            //PROCEDURE
            //create procedure InsertCoche(@idcoche int, @marca nvarchar(50), @modelo nvarchar(50), @conductor nvarchar(50), @imagen nvarchar(1000))
            //as
            //  insert into coches values(@idcoche, @marca, @modelo, @conductor, @imagen);
            //go
            int idcoche = this.GetMaxId() +1;
            String sql = "InsertCoche @idcoche, @marca, @modelo, @conductor, @imagen";
            SqlParameter pamidcoche = new SqlParameter("@idcoche", idcoche);
            SqlParameter pammarca = new SqlParameter("@marca", marca);
            SqlParameter pammodelo = new SqlParameter("@modelo", modelo);
            SqlParameter pamconductor = new SqlParameter("@conductor", conductor);
            SqlParameter pamimagen = new SqlParameter("@imagen", imagen);

            this.context.Database.ExecuteSqlRaw(sql, pamidcoche, pammarca, pammodelo, pamconductor, pamimagen);
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
