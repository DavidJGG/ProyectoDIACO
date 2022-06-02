using ProyectoDIACO.Models;
using ProyectoDIACO.Data;
using ProyectoDIACO.Herramientas;
using Microsoft.EntityFrameworkCore;

namespace ProyectoDIACO.Services
{
    public class CreateUserService
    {
        public async Task<Dictionary<string, object>> create(ProyectoDIACOContext db, Usuario model)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            if(db.Usuario.Where(u => u.Correo == model.Correo).Count() > 0)
            {
                result.Add("err", true);
                result.Add("msg", "El correo ya esta registrado");
                return result;
            }
            model.Contrasena = Encriptar.GetSHA1(model.Contrasena);
            db.Usuario.Add(model);
            db.SaveChanges();


            result.Add("err", false);
            result.Add("msg", "");

            return result;
        }
    }
}
