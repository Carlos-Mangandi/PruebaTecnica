using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaCRUD.EN
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Rol")]
        [Display(Name = "Rol")]
        public int IdRol { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        [ValidateNever]
        public Rol Rol { get; set; }
    }
}
