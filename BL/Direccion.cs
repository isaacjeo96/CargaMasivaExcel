using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Direccion
    {
        public static ML.Result DireccionGetByIdColonia(int IdDireccion)
        {
            ML.Result resultado = new ML.Result();
            try
            {
                using (DL.PruebaDDLEntities context = new DL.PruebaDDLEntities())
                {
                    var query = context.DireccionGetByIdColonia(IdDireccion).ToList();

                    resultado.Objects = new List<object>();

                    if (query != null)
                    {
                        resultado.Objects = new List<object>();
                        foreach (var obj in query)
                        {
                            ML.Direccion direccion = new ML.Direccion();
                            direccion.Colonia = new ML.Colonia();
                            direccion.IdDireccion = obj.IdDireccion;
                            direccion.Calle = obj.Calle;
                            direccion.NumExterior = obj.NumExterior;
                            direccion.NumInterior = obj.NumInterior;
                            direccion.Colonia.IdColonia = obj.IdColonia.Value;

                            resultado.Objects.Add(direccion);

                        }
                        resultado.Correct = true;

                    }
                    else
                    {

                        resultado.Correct = false;
                        resultado.ErrorMessage = " No existen registros en la tabla Pais";
                    }
                }
            }
            catch (Exception ex)
            {

                resultado.Correct = false;
                resultado.ErrorMessage = ex.Message;
                resultado.Ex = ex;
            }
            return resultado;

        }

        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.PruebaDDLEntities context = new DL.PruebaDDLEntities())
                {

                    var lista = context.DireccionGetAll().ToList();

                    result.Objects = new List<object>();

                    if (lista != null)
                    {
                        foreach (var obj in lista )
                        {
                            ML.Direccion direccion = new ML.Direccion();

                            direccion.IdDireccion = obj.IdDireccion;
                            direccion.NumExterior = obj.NumExterior;
                            direccion.NumInterior = obj.NumInterior;
                            direccion.Calle = obj.Calle;
                            direccion.Colonia = new ML.Colonia();
                            direccion.Colonia.IdColonia = obj.IdColonia.Value;


                            result.Objects.Add(direccion);
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
        }
    }
}
