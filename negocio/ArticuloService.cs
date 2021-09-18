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
                comando.CommandText = "SELECT A.Codigo, A.Nombre, A.Descripcion, A.Precio, A.ImagenUrl, M.Descripcion AS Marca, C.Descripcion AS Categoria, A.Id, M.Id, C.Id FROM ARTICULOS AS A, MARCAS AS M, CATEGORIAS AS C WHERE A.IdCategoria = C.Id AND A.IdMarca = M.Id";
                comando.Connection = conexion;

                conexion.Open();
                lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)lector["Id"];
                    aux.CodigoArticulo = (string)lector["Codigo"];
                    aux.Nombre = (string)lector["Nombre"];
                    aux.Descripcion = (string)lector["Descripcion"];
                    aux.Precio = (decimal)lector["Precio"]; //ver como sacar algunos decimales
                    if (!(lector["ImagenUrl"] is DBNull))
                    {
                        aux.ImagenUrl = (string)lector["ImagenUrl"];
                    }
                    aux.DescripcionMarca = new Marca();
                    aux.DescripcionMarca.Id = (int)lector["Id"];
                    aux.DescripcionMarca.Descripcion = (string)lector["Marca"];
                    aux.DescripcionCategoria = new Categoria();
                    aux.DescripcionCategoria.Id = (int)lector["Id"];
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
                datos.setearConsulta("INSERT INTO ARTICULOS (Codigo,Nombre,Descripcion,Precio, IdMarca, IdCategoria, Id) VALUES ('" + art.CodigoArticulo + "','" + art.Nombre + "','" + art.Descripcion + "'," + art.Precio + ", @IdMarca, @IdCategoria, @Id)");
                datos.setearParametro("@IdMarca",art.DescripcionMarca.Id);
                datos.setearParametro("@IdCategoria", art.DescripcionCategoria.Id);
                datos.setearParametro("@Id", art.Id);
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
                datos.setearConsulta("UPDATE ARTICULOS SET Codigo = @codigo, Nombre = @nombre, Descripcion = @descripcion, Precio = @precio, UrlImagen = @img, IdMarca = @idMarca, IdCategoria = @idCategoria WHERE Id = @id");
                datos.setearParametro("@codigo", art.CodigoArticulo);
                datos.setearParametro("@nombre", art.Nombre);
                datos.setearParametro("@descripcion", art.Descripcion);
                datos.setearParametro("@precio", art.Precio);
                datos.setearParametro("@img", art.ImagenUrl);
                datos.setearParametro("@idMarca", art.DescripcionMarca.Id);
                datos.setearParametro("@idCategoria", art.DescripcionCategoria.Id);

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
