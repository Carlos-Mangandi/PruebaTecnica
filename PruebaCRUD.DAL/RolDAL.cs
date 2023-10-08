using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PruebaCRUD.EN;
using Microsoft.EntityFrameworkCore;

namespace PruebaCRUD.DAL
{
    public class RolDAL
    {
       public static async Task<int> Crear(Rol pRol)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                bdContexto.Add(pRol);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<int> Modificar(Rol pRol)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var rol = await bdContexto.Rol.FirstOrDefaultAsync(r => r.Id == pRol.Id);
                rol.Nombre = pRol.Nombre;
                bdContexto.Update(rol);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<int> Eliminar(Rol pRol)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var rol = await bdContexto.Rol.FirstOrDefaultAsync(r => r.Id == pRol.Id);
                bdContexto.Rol.Remove(rol);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<Rol> ObtenerPorId(Rol pRol)
        {
            var rol = new Rol();
            using (var bdContexto = new BDContexto())
            {
                rol = await bdContexto.Rol.FirstOrDefaultAsync(r => r.Id == pRol.Id);
            }
            return rol;
        }

        public static async Task<List<Rol>> ObtenerTodos()
        {
            var roles = new List<Rol>();
            using( var bdContexto = new BDContexto())
            {
                roles = await bdContexto.Rol.ToListAsync();
            }
            return roles;
        }

        internal static IQueryable<Rol> QuerySelect(IQueryable<Rol> pQuery, Rol pRol)
        {
            if (pRol.Id > 0)
                pQuery = pQuery.Where(r => r.Id == pRol.Id);
            if (!string.IsNullOrWhiteSpace(pRol.Nombre))
                pQuery = pQuery.Where(r => r.Id == pRol.Id);
            pQuery = pQuery.OrderByDescending(r => r.Id).AsQueryable();

            return pQuery;
        }

        public static async Task<List<Rol>> Buscar(Rol pRol)
        {
            var roles = new List<Rol>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.Rol.AsQueryable();
                select = QuerySelect(select, pRol);
                roles = await select.ToListAsync();
            }
            return roles;
        }
    }
}
