using MvcCore.Helpers;
using MvcCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MvcCore.Repositories
{
    public class RepositoryCochesXML : IRepositoryCoches
    {
        private PathProvider pathProvider;
        private XDocument docxml;
        private String path;

        public RepositoryCochesXML(PathProvider pathProvider)
        {
            this.pathProvider = pathProvider;
            this.path = this.pathProvider.MapPath("coches.xml", Folders.Documents);
            this.docxml = XDocument.Load(this.path);
        }
        public List<Coche> GetCoches()
        {
            var consulta = from datos in this.docxml.Descendants("coche")
                           select new Coche
                           {
                               IdCoche = int.Parse(datos.Element("idcoche").Value),
                               Marca = datos.Element("marca").Value,
                               Modelo = datos.Element("modelo").Value,
                               Conductor = datos.Element("conductor").Value,
                               Imagen = datos.Element("imagen").Value
                           };
            return consulta.ToList();
        }

        public List<Coche> BuscarCocheModelo(string modelo)
        {
            var consulta = from datos in this.docxml.Descendants("coche").Where(z => z.Element("modelo").Value == modelo)
                           select new Coche
                           {
                               IdCoche = int.Parse(datos.Element("idcoche").Value),
                               Marca = datos.Element("marca").Value,
                               Modelo = datos.Element("modelo").Value,
                               Conductor = datos.Element("conductor").Value,
                               Imagen = datos.Element("imagen").Value
                           };
            return consulta.ToList();
        }
        public Coche GetCocheId(int idcoche)
        {
            var consulta = from datos in this.docxml.Descendants("coche").Where(z => z.Element("idcoche").Value == idcoche.ToString())
                           select new Coche
                           {
                               IdCoche = int.Parse(datos.Element("idcoche").Value),
                               Marca = datos.Element("marca").Value,
                               Modelo = datos.Element("modelo").Value,
                               Conductor = datos.Element("conductor").Value,
                               Imagen = datos.Element("imagen").Value
                           };
            return consulta.FirstOrDefault();
        }
        public XElement GetXElementCocheId(int idcoche)
        {
            var consulta = from datos in this.docxml.Descendants("coche").Where(z => z.Element("idcoche").Value == idcoche.ToString())
                           select datos;
            return consulta.FirstOrDefault();
        }
        public void DeleteCoche(int idcoche)
        {
            XElement element = this.GetXElementCocheId(idcoche);
            element.Remove();
            this.docxml.Save(this.path);
        }


        public int GetMaxId()
        {
            var consulta = (from datos in this.docxml.Descendants("coche")
                            select new Coche
                            {
                                IdCoche = int.Parse(datos.Element("idcoche").Value),
                                Marca = datos.Element("marca").Value,
                                Modelo = datos.Element("modelo").Value,
                                Conductor = datos.Element("conductor").Value,
                                Imagen = datos.Element("imagen").Value
                            }).Max(x => x.IdCoche);
            return consulta;
        }

        public void InsertCoche(string marca, string modelo, string conductor, string imagen)
        {
            XElement element = new XElement("coche");
            XElement elidcoche = new XElement("idcoche", this.GetMaxId()+1);
            XElement elmarca = new XElement("marca", marca);
            XElement elmodelo = new XElement("modelo", modelo);
            XElement elconductor = new XElement("conductor", conductor);
            XElement elimagen= new XElement("imagen", imagen);
            element.Add(elidcoche, elmarca, elmodelo, elconductor, elimagen);
            this.docxml.Element("coches").Add(element);
            this.docxml.Save(this.path);
        }

        public void UpdateCoche(int idcoche, string marca, string modelo, string conductor, string imagen)
        {
            XElement element = this.GetXElementCocheId(idcoche);
            element.Element("marca").Value = marca;
            element.Element("modelo").Value = modelo;
            element.Element("conductor").Value = conductor;
            element.Element("imagen").Value = imagen;
            this.docxml.Save(this.path);
        }
    }
}
