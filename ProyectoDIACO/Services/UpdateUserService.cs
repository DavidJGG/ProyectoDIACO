using ProyectoDIACO.Data;
using ProyectoDIACO.Herramientas;
using ProyectoDIACO.Models;

namespace ProyectoDIACO.Services
{
    public class UpdateUserService
    {
        public async Task<Dictionary<string, object>> update(ProyectoDIACOContext db, Usuario model)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();

            Usuario oldData = db.Usuario.Where(u=>u.usuarioId==model.usuarioId).FirstOrDefault();
            if (oldData == null)
            {
                result.Add("err", true);
                result.Add("msg", "No se encontró el usuario");
                return result;
            }

            if (db.Usuario.Where(u => u.Correo == model.Correo && u.Correo!=oldData.Correo).Count() > 0)
            {
                result.Add("err", true);
                result.Add("msg", "El correo ya esta registrado");
                return result;
            }

            if (model.Contrasena == "++++++++")
                model.Contrasena = oldData.Contrasena;
            else
                model.Contrasena = Encriptar.GetSHA1(model.Contrasena);

            oldData.Nombre = model.Nombre;
            oldData.Apellido=model.Apellido;
            oldData.Correo = model.Correo;
            oldData.Contrasena = model.Contrasena;
            oldData.Nit = model.Nit;
            oldData.Fecha = model.Fecha;
            oldData.Rol = model.Rol;

            db.Update(oldData);
            await db.SaveChangesAsync();

            result.Add("err", false);
            result.Add("msg", "");

            return result;
        }
    }
}
