using Microsoft.EntityFrameworkCore;
using PruebaCRUD.EN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaCRUD.DAL
{
    public class UsuarioDAL
    {
        public static async Task<int> CrearAsync(Usuario pUsuario)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                bdContexto.Add(pUsuario);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<int> ModificarAsync(Usuario pUsuario)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var usuario = await bdContexto.Usuario.FirstOrDefaultAsync(s => s.Id == pUsuario.Id);
                usuario.Nombres = pUsuario.Nombres;
                usuario.Apellidos = pUsuario.Apellidos;
                bdContexto.Update(usuario);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<int> EliminarAsync(Usuario pUsuario)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var usuario = await bdContexto.Usuario.FirstOrDefaultAsync(s => s.Id == pUsuario.Id);
                bdContexto.Usuario.Remove(usuario);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<Usuario> ObtenerPorIdAsync(Usuario pUsuario)
        {
            var usuario = new Usuario();
            using (var bdContexto = new BDContexto())
            {
                usuario = await bdContexto.Usuario.FirstOrDefaultAsync(s => s.Id == pUsuario.Id);
            }
            return usuario;
        }

        public static async Task<List<Usuario>> ObtenerTodosAsync()
        {
            var usuarios = new List<Usuario>();
            using (var bdContexto = new BDContexto())
            {
                usuarios = await bdContexto.Usuario.ToListAsync();
            }
            return usuarios;
        }

        internal static IQueryable<Usuario> QuerySelect(IQueryable<Usuario> pQuery, Usuario pUsuario)
        {
            if (pUsuario.Id > 0)
                pQuery = pQuery.Where(s => s.Id == pUsuario.Id);
            if (!string.IsNullOrWhiteSpace(pUsuario.Nombres))
                pQuery = pQuery.Where(s => s.Nombres == pUsuario.Nombres);
            if (!string.IsNullOrWhiteSpace(pUsuario.Apellidos))
                pQuery = pQuery.Where(s => s.Apellidos.Contains(pUsuario.Apellidos));
            pQuery = pQuery.OrderByDescending(s => s.Id).AsQueryable();

            return pQuery;
        }
    }

}
