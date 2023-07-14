using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using solicitudescooitza.Models;
using solicitudescooitza.Models.Dtos;

namespace solicitudescooitza.Controllers
{
    public class TblSolicitudesController : Controller
    {
        private developerEntities db = new developerEntities();

        // GET: TblSolicitudes
        public ActionResult Index()
        {
            ViewBag.idCatProveedores = new SelectList(db.CatProveedores, "idCatproveedor", "descipcion");
            ViewBag.idCatRubros = new SelectList(db.CatRubros, "idCatRubros", "descripcion");
            var tblSolicitudes = db.TblSolicitudes.Include(t => t.CatProveedores).Include(t => t.CatRubros).Include(t => t.TblUsuarios);
            return View(tblSolicitudes.ToList());
        }
        [HttpGet]
        public ActionResult GetSolicicitudes()
        {
            var respuesta = new
            {
                codigo = 0,
                descripcion = string.Empty,
                listado = new List<TblSolicitudesVM>()
            };
            List<TblSolicitudesVM> solicitudes = new List<TblSolicitudesVM>();
            try
            {
                solicitudes = (from d in db.TblSolicitudes
                                   where d.idCatEstados == 1
                                   orderby d.fechaAlta descending
                                   select new
                                   {
                                       Consulta = d
                                   })
              .ToList()
              .Select(result => new TblSolicitudesVM
              {
                  idTblSolicitudes = result.Consulta.idTblSolicitudes == null ? 0 : result.Consulta.idTblSolicitudes,
                  Proveedor = result.Consulta.CatProveedores == null ? string.Empty : result.Consulta.CatProveedores.descipcion,
                  Rubro = result.Consulta.CatRubros == null ? string.Empty : result.Consulta.CatRubros.descripcion,
                  razon = result.Consulta.razon,
                  monto = result.Consulta.monto,
                  fecha = String.Format("{0:dd/MM/yyyy HH:mm:ss}", result.Consulta.fechaAlta),
                  cantidad = result.Consulta.cantidad,
                  imagen = result.Consulta.imagen
              }).ToList();
            }
            catch (Exception ex)
            {
                respuesta = new
                {
                    codigo = 1,
                    descripcion = ex.Message,
                    listado = new List<TblSolicitudesVM>()
                };
                return Json(respuesta, JsonRequestBehavior.AllowGet);
            }



            respuesta = new
            {
                codigo = 1,
                descripcion = "CORRECTO",
                listado = solicitudes
            };
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        // GET: TblSolicitudes/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblSolicitudes tblSolicitudes = db.TblSolicitudes.Find(id);
            if (tblSolicitudes == null)
            {
                return HttpNotFound();
            }
            return View(tblSolicitudes);
        }

        public ActionResult AddSolicitud()
        {

            TblSolicitudes tblSolicitudes = new TblSolicitudes();
            tblSolicitudes.fechaAlta = DateTime.Now;
            tblSolicitudes.idCatEstados = 1;
            db.TblSolicitudes.Add(tblSolicitudes);
            db.SaveChanges();

            return RedirectToAction("Edit/" + tblSolicitudes.idTblSolicitudes, "TblSolicitudes");


        }
        // GET: TblSolicitudes/Create
        public ActionResult Create()
        {
            ViewBag.idCatProveedores = new SelectList(db.CatProveedores, "idCatproveedor", "descipcion");
            ViewBag.idCatRubros = new SelectList(db.CatRubros, "idCatRubros", "descripcion");
            ViewBag.idTblUsuario = new SelectList(db.TblUsuarios, "idTblUsuarios", "primerNombre");
            return View();
        }

        // POST: TblSolicitudes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idTblSolicitudes,idCatProveedores,idCatRubros,razon,monto,fecha,cantidad,totalQuetzales,totalDolares,imagen,idTblUsuario,fechaAlta")] TblSolicitudes tblSolicitudes)
        {
            if (ModelState.IsValid)
            {
                db.TblSolicitudes.Add(tblSolicitudes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idCatProveedores = new SelectList(db.CatProveedores, "idCatproveedor", "Descripcion_", tblSolicitudes.idCatProveedores);
            ViewBag.idCatRubros = new SelectList(db.CatRubros, "idCatRubros", "descripcion", tblSolicitudes.idCatRubros);
            ViewBag.idTblUsuario = new SelectList(db.TblUsuarios, "idTblUsuarios", "primerNombre", tblSolicitudes.idTblUsuario);
            return View(tblSolicitudes);
        }

        // GET: TblSolicitudes/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblSolicitudes tblSolicitudes = db.TblSolicitudes.Find(id);
            if (tblSolicitudes == null)
            {
                return HttpNotFound();
            }
            ViewBag.idCatProveedores = new SelectList(db.CatProveedores, "idCatproveedor", "descipcion");
            ViewBag.idCatRubros = new SelectList(db.CatRubros, "idCatRubros", "descripcion");
            ViewBag.idTblUsuario = new SelectList(db.TblUsuarios, "idTblUsuarios", "primerNombre");
            return View(tblSolicitudes);
        }

        // POST: TblSolicitudes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit(TblSolicitudesDto tblSolicitudesDto)
        {
            var result = new
            {
                codigo = 0,
                descripcion = string.Empty,
            };



            try
            {
                TblSolicitudes tblSolicitudes = db.TblSolicitudes.Find(tblSolicitudesDto.idTblSolicitudes);
                tblSolicitudes.idTblSolicitudes = tblSolicitudesDto.idTblSolicitudes;
                tblSolicitudes.idCatProveedores = tblSolicitudesDto.idCatProveedores;
                tblSolicitudes.idCatRubros = tblSolicitudesDto.idCatRubros;
                tblSolicitudes.razon = tblSolicitudesDto.razon;
                tblSolicitudes.monto = tblSolicitudesDto.monto == null ? 0 : Convert.ToDecimal(tblSolicitudesDto.monto);

                string[] formatosPosibles = { "yyyy-MM-dd", "dd-MM-yyyy", "yyyy/M/dd", "dd/M/yyyy", "dd/MM/yyyy" }; // Lista de formatos posibles

                DateTime fechaObjeto;
                bool exito = DateTime.TryParseExact(tblSolicitudesDto.fecha, formatosPosibles, null, System.Globalization.DateTimeStyles.None, out fechaObjeto);

                if (!exito)// si el analizador devuelve false se rechaza la solicitud 
                {
                    result = new
                    {
                        codigo = 0,
                        descripcion = "IMPOSIBLE RESOLVER LA FECHA",
                    };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                tblSolicitudes.fecha = fechaObjeto;
                tblSolicitudes.cantidad = tblSolicitudesDto.cantidad;

                tblSolicitudes.idTblUsuario = 1;
                db.Entry(tblSolicitudes).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                result = new
                {
                    codigo = 0,
                    descripcion = "ERROR AL GUARDAR LOS DATOS" + ex.Message,
                };

            }
            result = new
            {
                codigo = 1,
                descripcion = "REGISTRO GUARDADO CORRECTAMENTE",
            };

            return Json(result, JsonRequestBehavior.AllowGet);

        }

        // GET: TblSolicitudes/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblSolicitudes tblSolicitudes = db.TblSolicitudes.Find(id);
            if (tblSolicitudes == null)
            {
                return HttpNotFound();
            }
            return View(tblSolicitudes);
        }

        // POST: TblSolicitudes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            TblSolicitudes tblSolicitudes = db.TblSolicitudes.Find(id);
            db.TblSolicitudes.Remove(tblSolicitudes);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
