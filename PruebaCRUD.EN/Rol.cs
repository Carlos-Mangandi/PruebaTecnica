using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;


namespace PruebaCRUD.EN
{
    public class Rol
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }

        [ValidateNever]
        public List<Usuario> Usuario { get; set; }
    }
}
