using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ML;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
//using System.Data.Objects;

namespace BL
{
    public class Usuario
    {
        //ENTITY FRAMEWORK
        public static Result GetAllEF()
        {
            Result result = new Result();

            try
            {
                using (DL.PruebaDDLEntities context = new DL.PruebaDDLEntities())
                {

                    var usuarios = context.UsuarioGetAll().ToList();

                    result.Objects = new List<object>();

                    if (usuarios != null)
                    {
                        foreach (var obj in usuarios)
                        {
                            ML.Usuario usuario = new ML.Usuario();

                            usuario.IdUsuario = obj.IdUsuario;
                            usuario.Nombre = obj.Nombre;
                            usuario.ApellidoPaterno = obj.ApellidoPaterno;
                            usuario.ApellidoMaterno = obj.ApellidoMaterno;

                            usuario.Direccion = new ML.Direccion();
                            usuario.Direccion.IdDireccion = obj.IdDireccion;
                            usuario.Direccion.Calle = obj.Calle;
                            usuario.Direccion.NumExterior = obj.NumExterior;
                            usuario.Direccion.NumInterior = obj.NumInterior;

                            usuario.Direccion.Colonia = new ML.Colonia();
                            usuario.Direccion.Colonia.IdColonia = obj.IdColonia.Value;
                            usuario.Direccion.Colonia.Nombre = obj.Colonia;
                            usuario.Direccion.Colonia.CodigoPostal = obj.CP;

                            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                            usuario.Direccion.Colonia.Municipio.IdMunicipio = obj.IdMunicipio.Value;
                            usuario.Direccion.Colonia.Municipio.Nombre = obj.Municipio;

                            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                            usuario.Direccion.Colonia.Municipio.Estado.IdEstado = obj.IdEstado.Value;
                            usuario.Direccion.Colonia.Municipio.Estado.Nombre = obj.Estado;

                            usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                            usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais = obj.IdPais.Value;
                            usuario.Direccion.Colonia.Municipio.Estado.Pais.Nombre = obj.Pais;

                            usuario.Imagen = obj.Imagen;

                            result.Objects.Add(usuario);
                        }

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron registros.";
                    }
                }
            }
            catch (Exception ex)
            {

                result.Correct = false;
                result.ErrorMessage = ex.Message;

            }

            return result;
        }//termina getallEF

        public static ML.Result AddEF(ML.Usuario usuario)//Stored Procedure agregar datos Entity framework
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.PruebaDDLEntities context = new DL.PruebaDDLEntities())
                {
                    var resultQuery = context.UsuarioAdd(usuario.Nombre, usuario.ApellidoPaterno, usuario.ApellidoMaterno,usuario.Direccion.IdDireccion,usuario.Imagen);


                    if (resultQuery >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se insertó el registro";
                    }

                    result.Correct = true;

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static Result GetByIdEF(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.PruebaDDLEntities context = new DL.PruebaDDLEntities())
                {
                    var objUsuario = context.UsuarioGetById(IdUsuario).FirstOrDefault();

                    result.Objects = new List<object>();

                    if (objUsuario != null)
                    {
                        ML.Usuario usuario = new ML.Usuario();
                        usuario.IdUsuario = objUsuario.IdUsuario;
                        usuario.Nombre = objUsuario.Nombre;
                        usuario.ApellidoPaterno = objUsuario.ApellidoPaterno;
                        usuario.ApellidoMaterno = objUsuario.ApellidoMaterno;
                        usuario.Direccion = new ML.Direccion();
                        usuario.Direccion.IdDireccion = objUsuario.IdDireccion.Value;
                        usuario.Imagen = objUsuario.Imagen;

                        result.Object = usuario;

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron registros.";
                    }
                }
            }
            catch (Exception ex)
            {

                result.Correct = false;
                result.ErrorMessage = ex.Message;

            }

            return result;
        }// GetById Usuario

        public static Result UpdateEF(ML.Usuario usuario)//stored procedure actualizar datos con Entety Framework
        {
            Result result = new Result();
            try
            {

                using (DL.PruebaDDLEntities context = new DL.PruebaDDLEntities())
                {
                    var updateResult = context.UsuarioUpdate(usuario.IdUsuario, usuario.Nombre, usuario.ApellidoPaterno, usuario.ApellidoMaterno, usuario.Direccion.IdDireccion,usuario.Imagen);
                    if (updateResult >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        //result.ErrorMessage = "No se actualizó el status de la credencial";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        public static DataTable ConvertXSLXtoDataTable(string strFilePath, string connString)
        {
            OleDbConnection oledbConn = new OleDbConnection(connString);
            DataTable dt = new DataTable();
            try
            {

                oledbConn.Open();
                using (OleDbCommand cmd = new OleDbCommand("SELECT * FROM [Hoja1$]", oledbConn))
                {
                    OleDbDataAdapter da = new OleDbDataAdapter();
                    da.SelectCommand = cmd;

                    da.Fill(dt);
                }
            }
            catch 
            {
            }
            finally
            {

                oledbConn.Close();
            }

            return dt;

        }
    }
}

