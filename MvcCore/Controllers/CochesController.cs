using Microsoft.AspNetCore.Mvc;
using MvcCore.Models;
using MvcCore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCore.Controllers
{
    public class CochesController : Controller
    {
        IRepositoryCoches repo;

        public CochesController(IRepositoryCoches repo)
        {
            this.repo = repo;
        }
        public IActionResult Index()
        {
            List<Coche> coches = this.repo.GetCoches();
            return View(coches);
        }
        public IActionResult Details(int idcoche)
        {
            Coche coche = this.repo.GetCocheId(idcoche);
            return View(coche);
        }

        public IActionResult Edit (int idcoche)
        {
            Coche coche = this.repo.GetCocheId(idcoche);
            return View(coche);
        }
        [HttpPost]
        public IActionResult Edit(int idcoche, String marca, String modelo, String conductor, String imagen)
        {
            this.repo.UpdateCoche(idcoche, marca, modelo, conductor, imagen);

            return RedirectToAction("Index");

        }


        public IActionResult Delete(int idcoche)
        {
            this.repo.DeleteCoche(idcoche);
            return RedirectToAction("Index");
        }

        public IActionResult Insert()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Insert(String marca, String modelo, String conductor, String imagen)
        {
            this.repo.InsertCoche(marca, modelo, conductor, imagen);
            return RedirectToAction("Index");
        }

        public IActionResult BuscarCocheModelo()
        {
            return View();
        }
        [HttpPost]
        public IActionResult BuscarCocheModelo(String modelo)
        {
            List<Coche>coches = this.repo.BuscarCocheModelo(modelo);
            return View(coches);
        }
    }
}
