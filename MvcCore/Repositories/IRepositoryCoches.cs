using MvcCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCore.Repositories
{
    public interface IRepositoryCoches
    {
        List<Coche> GetCoches();

        Coche GetCocheId(int idcoche);

        int GetMaxId();

        void InsertCoche(String marca, String modelo, String conductor, String imagen);

        void UpdateCoche(int idcoche, String marca, String modelo, String conductor, String imagen);

        void DeleteCoche(int idcoche);
        List<Coche> BuscarCocheModelo(String modelo);

    }
}
