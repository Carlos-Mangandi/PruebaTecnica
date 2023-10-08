using Microsoft.EntityFrameworkCore;
using PruebaCRUD.EN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PruebaCRUD.DAL
{
    public class UsuarioDAL
    {
        private static void Encriptar(Usuario pUsuario)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.ASCII.GetBytes(pUsuario.Password));
                var strEncriptar = "";
                for (int i = 0; i < result.Length; i++)
                    strEncriptar += result[i].ToString("x2").ToLower();
                pUsuario.Password = strEncriptar;
            }
        }

        private static async Task<bool> ExisteLogin(Usuario pUsuario, BDContexto pBdContexto)
        {
            bool result = false;
            var loginUsuarioExiste = await pBdContexto.Usuario.FirstOrDefaultAsync(s => s.Login == pUsuario.Login && s.Id != pUsuario.Id);
            if (loginUsuarioExiste != null && loginUsuarioExiste.Id > 0 && loginUsuarioExiste.Login == pUsuario.Login)
                result = true;
            return result;
        }

        public static async Task<int> CrearAsync(Usuario pUsuario)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                bool existeLogin = await ExisteLogin(pUsuario, bdContexto);
                if (existeLogin == false)
                {
                    Encriptar(pUsuario);
                    bdContexto.Add(pUsuario);
                    result = await bdContexto.SaveChangesAsync();
                }
                else
                    throw new Exception("Login ya existe");
                
            }
            return result;
        }

        public static async Task<int> ModificarAsync(Usuario pUsuario)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                bool existeLogin = await ExisteLogin(pUsuario, bdContexto);
                if (existeLogin == false)
                {
                    var usuario = await bdContexto.Usuario.FirstOrDefaultAsync(s => s.Id == pUsuario.Id);
                    usuario.Nombres = pUsuario.Nombres;
                    usuario.Apellidos = pUsuario.Apellidos;
                    usuario.Login = pUsuario.Login;
                    bdContexto.Update(usuario);
                    result = await bdContexto.SaveChangesAsync();
                }
                else
                    throw new Exception("Login ya existe");
                
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
            if (!string.IsNullOrWhiteSpace(pUsuario.Login))
                pQuery = pQuery.Where(s => s.Login.Contains(pUsuario.Login));
            pQuery = pQuery.OrderByDescending(s => s.Id).AsQueryable();

            return pQuery;
        }

        public static async Task<List<Usuario>> BuscarAsync(Usuario pUsuario)
        {
            var Usuarios = new List<Usuario>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.Usuario.AsQueryable();
                select = QuerySelect(select, pUsuario);
                Usuarios = await select.ToListAsync();
            }
            return Usuarios;
        }

        public static async Task<List<Usuario>> BuscarIncluirRolesAsync(Usuario pUsuario)
        {
            var usuarios = new List<Usuario>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.Usuario.AsQueryable();
                select = QuerySelect(select, pUsuario).Include(s => s.Rol).AsQueryable();
                usuarios = await select.ToListAsync();
            }
            return usuarios;
        }

        public static async Task<Usuario> LoginAsync(Usuario pUsuario)
        {
            var usuario = new Usuario();
            using (var bdContexto = new BDContexto())
            {
                Encriptar(pUsuario);
                usuario = await bdContexto.Usuario.FirstOrDefaultAsync(s => s.Login == pUsuario.Login && s.Password == pUsuario.Password);
            }
            return usuario;
        }
    }
}
