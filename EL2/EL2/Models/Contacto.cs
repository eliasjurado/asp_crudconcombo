using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EL2.Models
{
    public class Contacto
    {
        [Display(Name = "codigo", Order = 0)]
        public int codcontac { get; set; }
        [Display(Name = "nombre", Order = 1)]
        public string nomcontac { get; set; }
        [Display(Name = "direccion", Order = 2)]
        public string dircontac { get; set; }
        [Display(Name = "pais", Order = 3)]
        public string nompais { get; set; }
    }
}