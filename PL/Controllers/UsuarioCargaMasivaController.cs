using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace PL.Controllers
{
    public class UsuarioCargaMasivaController : Controller
    {
        [HttpGet]

        public ActionResult CargaMasiva()
        {
            ML.Usuario usuario = new ML.Usuario();

            return View(usuario);
        }

        [HttpPost]
        public ActionResult CargaMasiva(ML.Usuario usuario)
        {
            HttpPostedFileBase file = Request.Files["ExcelUsuarios"];


            if (file.ContentLength > 0)
            {
                string extension = Path.GetExtension(file.FileName).ToLower();
                if (extension == ".xlsx")
                {
                    string direccionExcel = Server.MapPath("~/ExcelCargaMasivaUsuario/") + Path.GetFileNameWithoutExtension(file.FileName) + '-' + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

                    if (!System.IO.File.Exists(direccionExcel))
                    {
                        file.SaveAs(direccionExcel);
                        string connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + direccionExcel + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                        BL.Usuario.ConvertXSLXtoDataTable(direccionExcel, connString);
                    }

                }
                else
                {
                    ViewBag.Message = "Seleccione un archivo con extensión .xlsx";
                }
            }
            else
            {
                ViewBag.Message = "Seleccione un archivo";
            }
            return View();
        }



    }
}
