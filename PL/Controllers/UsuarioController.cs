using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();

            usuario.Nombre = (usuario.Nombre == null) ? "" : usuario.Nombre; //operador ternario
            usuario.ApellidoPaterno = (usuario.ApellidoPaterno == null) ? "" : usuario.ApellidoPaterno; //operador ternario
            usuario.ApellidoMaterno = (usuario.ApellidoMaterno == null) ? "" : usuario.ApellidoMaterno; //operador ternario 

            ML.Result result = BL.Usuario.GetAllEF(usuario);

            if (result.Correct)
            {
                usuario.Usuarios = result.Objects;
                return View(usuario);
            }
            else
            {
                ViewBag.Message = "Ocurrió un error al obtener la información" + result.ErrorMessage;
                return PartialView("ValidationModal");
            }

        }// termina getall

        [HttpPost]
        public ActionResult GetAll(ML.Usuario usuario)
        {
            //ML.Usuario usuario = new ML.Usuario();

            usuario.Nombre = (usuario.Nombre == null) ? "" : usuario.Nombre;
            usuario.ApellidoPaterno = (usuario.ApellidoPaterno == null) ? "" : usuario.ApellidoPaterno;
            usuario.ApellidoMaterno = (usuario.ApellidoMaterno == null) ? "" : usuario.ApellidoMaterno;

            ML.Result result = BL.Usuario.GetAllEF(usuario);

            if (result.Correct)
            {
                usuario.Usuarios = result.Objects;

                return View(usuario);
            }
            else
            {
                ViewBag.Message = "Ocurrió un error al obtener la información" + result.ErrorMessage;
                return PartialView("ValidationModal");
            }

        }// termina getall

        [HttpGet]
        public ActionResult Form(int? IdUsuario) //Add { Id null } //Update {Id > 0 }
        {
            ML.Usuario usuario = new ML.Usuario();
            ML.Result resultDireccion = BL.Direccion.GetAll();

            usuario.Direccion = new ML.Direccion();
            usuario.Direccion.Direcciones = resultDireccion.Objects;

                if (IdUsuario == null)
                {
                    return View(usuario);
                }
                else
                {
                    //GetById
                    ML.Result result = BL.Usuario.GetByIdEF(IdUsuario.Value);


                    if (result.Correct)
                    {
                        usuario.IdUsuario = ((ML.Usuario)result.Object).IdUsuario;
                        usuario.Nombre = ((ML.Usuario)result.Object).Nombre;
                        usuario.ApellidoPaterno = ((ML.Usuario)result.Object).ApellidoPaterno;
                        usuario.ApellidoMaterno = ((ML.Usuario)result.Object).ApellidoMaterno;
                        usuario.Direccion.IdDireccion = ((ML.Usuario)result.Object).Direccion.IdDireccion;
                        usuario.Direccion.Calle = ((ML.Usuario)result.Object).Direccion.Calle;
                        usuario.Imagen = ((ML.Usuario)result.Object).Imagen;




                        return View(usuario);

                    }

                    else
                    {
                        ViewBag.Message = "Ocurrió un error al obtener la información" + result.ErrorMessage;
                        return PartialView("ValidationModal");
                    }
                }


        }

        [HttpPost]
        public ActionResult Form(ML.Usuario usuario) //Add { Id null } //Update {Id > 0 }
        {
            ML.Result result = new ML.Result();

            HttpPostedFileBase file = Request.Files["ImagenData"];

            if (file.ContentLength > 0)
            {
                usuario.Imagen = ConvertToBytes(file);
            }

            if (usuario.IdUsuario == 0)
            {
                result = BL.Usuario.AddEF(usuario);
                if (result.Correct)
                {
                    ViewBag.Message = "Usuario agregado correctamente";
                }
            }
            else
            {
                result = BL.Usuario.UpdateEF(usuario);
                if (result.Correct)
                {
                    ViewBag.Message = "Usuario actualizado correctamente";
                }
            }

            if (!result.Correct)
            {
                ViewBag.Message = "No se pudo agregar correctamente el usuario " + result.ErrorMessage;
            }

            return PartialView("ValidationModal");

        }

        public byte[] ConvertToBytes(HttpPostedFileBase Imagen)
        {
            byte[] data = null;
            System.IO.BinaryReader reader = new System.IO.BinaryReader(Imagen.InputStream);
            data = reader.ReadBytes((int)Imagen.ContentLength);

            return data;
        }
    }
}