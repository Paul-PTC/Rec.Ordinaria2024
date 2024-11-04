using Refuerzo2024.Model.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refuerzo2024.Model.DAO
{
    internal class DAOEspecialidad : DTOEspecialidad
    {
        //Variable con la conexión
        SqlConnection con = obtenerConexion();

        public DataSet ObtenerFacultades()
        {
            try
            {
                //Se crea la instrucción de lo que se quiere hacer
                string query = "SELECT * FROM Especialidades";
                //Se crea el comando de tipo Sql que recibe la instrucción y la conexión
                SqlCommand cmd = new SqlCommand(query, con);
                //ExecuteNonQuery se utiliza para acciones como INSERT, UPDATE, DELETE
                //ExecuteScalar se utiliza para acciones como SELECT
                cmd.ExecuteScalar();
                //Toma los valores y los pone en una tabla generica
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                //Se crea el objeto según el tipo de dato a retornar
                DataSet ds = new DataSet();
                //Se rellena el objeto a retornar con los datos de la tabla generica
                adp.Fill(ds, "Especialidades");
                //Se retorna el objeto
                return ds;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                con.Close();
            }
        }
        public DataSet ObtenerEstudiantes()
        {
            try
            {
                string query = "SELECT * FROM Facultades";
                SqlCommand cmdObtener = new SqlCommand(query, con);
                cmdObtener.ExecuteScalar();
                DataSet ds = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter(cmdObtener);
                adp.Fill(ds, "Facultades");
                return ds;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                con.Close();
            }
        }

        public bool RegistrarEstudiante()
        {
            try
            {
                string query = "INSERT INTO Especialidades VALUES (@param1, @param2)";
                SqlCommand cmdInsert = new SqlCommand(query, con);
                cmdInsert.Parameters.AddWithValue("param1", NombreEspecialidad);
                cmdInsert.Parameters.AddWithValue("param2", IdFacultad);
                cmdInsert.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                con.Close();
            }
        }



        public bool ActualizarEstudiante()
        {
            try
            {
                //Crea la instrucción de lo que se quiere hacer
                string query = "UPDATE Especialidades SET nombreEspecialidad = @nombreEspecialidades, idFacultad = @idEstudiante WHERE idEspecialidad = @idEstudiantes";
                //Crea el comando con la instrucción y la conexión
                SqlCommand cmdUpdate = new SqlCommand(query, con);
                cmdUpdate.Parameters.AddWithValue("nombreEspecialidades", NombreEspecialidad);
                cmdUpdate.Parameters.AddWithValue("idEstudiante", IdFacultad);
                cmdUpdate.Parameters.AddWithValue("idEstudiantes", IdEspecialidad);
                //Ejecuta la instrucciones
                cmdUpdate.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally { con.Close(); }
        }

        public bool EliminarEstudiante()
        {
            try
            {
                string query = "DELETE FROM Especialidades WHERE idEspecialidad = @param1";
                SqlCommand cmdDelete = new SqlCommand(query, con);
                cmdDelete.Parameters.AddWithValue("param1", IdEspecialidad);
                cmdDelete.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                con.Close();
            }
        }

        public DataSet BuscarEstudiante(string valor)
        {
            try
            {
                string query = "SELECT * FROM Especialidad WHERE nombreEspecialidad LIKE @param1 OR idEspecialidad LIKE @param2";
                SqlCommand cmdObtener = new SqlCommand(query, con);
                cmdObtener.Parameters.AddWithValue("param1", "%" + valor + "%");
                cmdObtener.Parameters.AddWithValue("param2", "%" + valor + "%");
                cmdObtener.ExecuteScalar();
                DataSet ds = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter(cmdObtener);
                adp.Fill(ds, "Especialidades");
                return ds;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                con.Close();
            }
        }
    }
}