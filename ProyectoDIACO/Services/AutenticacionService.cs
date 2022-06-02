
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using ProyectoDIACO.Data;
using ProyectoDIACO.Herramientas;
using ProyectoDIACO.Models;
using System.Runtime.Serialization.Formatters.Binary;

namespace ProyectoDIACO.Services
{
    public class AutenticacionService
    {
        public bool autenticar(HttpContext propHttp, ProyectoDIACOContext db, string correo, string contrasena)
        {
            var resAuth = db.Usuario.Where(u => u.Correo == correo && u.Contrasena == Herramientas.Encriptar.GetSHA1(contrasena)).FirstOrDefault();
            if(resAuth == null)
            {
                return false;
            }

            propHttp.Session.SetInt32("usuarioId", resAuth.usuarioId);
            propHttp.Session.SetString("nombre", resAuth.Nombre);
            propHttp.Session.SetString("apellido", resAuth.Apellido);
            propHttp.Session.SetString("correo", resAuth.Correo);
            propHttp.Session.SetInt32("rol", (int)resAuth.Rol);

            

            return true;
        }

        public void destruirSesion(HttpContext propHttp)
        {
            propHttp.Session.Clear();
        }


        public bool isLogin(HttpContext propHttp)
        {
            var rol = propHttp.Session.GetInt32("rol");
            
            if(rol ==1 || rol == 2)
            {
                return true;
            }
            return false;
        }

        public bool isAdmin(HttpContext propHttp)
        {
            if (isLogin(propHttp)){
                if (propHttp.Session.GetInt32("rol")==1) return true;
            }
            return false;
        }
        
        public void fillViewData(ViewDataDictionary viewData, HttpContext propHttp)
        {
            if (!isLogin(propHttp))
            {
                return;
            }

            viewData["usr_nombre"]=propHttp.Session.GetString("nombre");
            viewData["usr_apellido"]=propHttp.Session.GetString("apellido");
            viewData["usr_correo"]=propHttp.Session.GetString("correo");
            viewData["usr_rol"] = propHttp.Session.GetInt32("rol");

        }
    }
}
