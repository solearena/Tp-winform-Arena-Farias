using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using dominio;

namespace negocio
{
    public class ArticuloService
    {
        public List<Articulo> listar()
        {
            List<Articulo> lista = new List<Articulo>();
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;

            try
            {
                conexion.ConnectionString = "server =.\\SQLEXPRESS; database = CATALOGO_DB; integrated security=true";
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "SELECT ar.Id,ar.Codigo,ar.Nombre,ar.Descripcion,ar.ImagenUrl,ar.Precio,ma.Id as IdMarca,ma.Descripcion as Marca, ca.Id as IdCategoria,ca.Descripcion as Categoria from Articulos ar inner join Marcas ma on ar.IdMarca = ma.Id inner join Categorias ca on ar.IdCategoria = ca.Id";
                comando.Connection = conexion;

                conexion.Open();
                lector = comando.ExecuteReader();

                while (lector.Read()) //A CORREGIR -- PROBLEMA CON LOS 3 ID CON MISMO NOMBRE
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)lector["Id"];
                    aux.CodigoArticulo = (string)lector["Codigo"];
                    aux.Nombre = (string)lector["Nombre"];
                    aux.Descripcion = (string)lector["Descripcion"];
                    aux.Precio = Math.Round((decimal)lector["Precio"],2); 
                    if (!(lector["ImagenUrl"] is DBNull))
                    {
                        aux.ImagenUrl = (string)lector["ImagenUrl"];
                    }
                    aux.DescripcionMarca = new Marca();
                    aux.DescripcionMarca.Id = (int)lector["IdMarca"];
                    aux.DescripcionMarca.Descripcion = (string)lector["Marca"];
                    aux.DescripcionCategoria = new Categoria();
                    aux.DescripcionCategoria.Id = (int)lector["IdCategoria"];
                    aux.DescripcionCategoria.Descripcion = (string)lector["Categoria"];
                    lista.Add(aux);

                }
                conexion.Close();

                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void agregar(Articulo art)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO ARTICULOS (Codigo,Nombre,Descripcion,Precio, IdMarca, IdCategoria) VALUES ('" + art.CodigoArticulo + "','" + art.Nombre + "','" + art.Descripcion + "'," + art.Precio + ", @IdMarca, @IdCategoria)");
                datos.setearParametro("@IdMarca",art.DescripcionMarca.Id);
                datos.setearParametro("@IdCategoria", art.DescripcionCategoria.Id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void modificar(Articulo art)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE ARTICULOS SET Codigo = @codigo, Nombre = @nombre, Descripcion = @descripcion, Precio = @precio, ImagenUrl = @img, IdMarca = @idMarca, IdCategoria = @idCategoria WHERE Id = @id");
                datos.setearParametro("@codigo", art.CodigoArticulo);
                datos.setearParametro("@nombre", art.Nombre);
                datos.setearParametro("@descripcion", art.Descripcion);
                datos.setearParametro("@precio", art.Precio);
                datos.setearParametro("@img", art.ImagenUrl);
                datos.setearParametro("@idMarca", art.DescripcionMarca.Id); //ver error 
                datos.setearParametro("@idCategoria", art.DescripcionCategoria.Id);
                datos.setearParametro("@id", art.Id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
    
}
