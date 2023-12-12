using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Proyecto_Final.Models;

namespace Proyecto_Final.Controllers
{
    public class CuentaController : Controller
    {
        public IActionResult IniciarSesion()
        {
            return View();
        }


        [HttpPost]
        public ActionResult IniciarSesion(TUsuario usuario)
        {
            if (VerificarCredenciales(usuario.CorreoElectronico, usuario.Contrasena))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Mensaje = "Usuario o Contrasena Incorrectos";
                return View();
            }
            
        }

        private bool VerificarCredenciales(string nombreUsuario, string contraseña)
        {
            bool credencialesValidas = false;

            // Configura la cadena de conexión a tu base de datos
            string cadenaConexion = "Server=(localdb)\\MSSQLLocalDB;Initial Catalog=DB_RECOLECCION_RECICLAJE;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";


            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                
                conexion.Open();

                
                string consulta = "Select count (*) from [SCH_RECIQUEST].[T_USUARIOS] where CORREO_ELECTRONICO = @usuario and CONTRASENA = @password";

               
                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    
                    comando.Parameters.AddWithValue("@usuario", nombreUsuario);
                    comando.Parameters.AddWithValue("@password", contraseña);

                    
                    int resultado = (int)comando.ExecuteScalar();
                    credencialesValidas = resultado > 0;
                }
            }

            return credencialesValidas;
        }

    }
}
