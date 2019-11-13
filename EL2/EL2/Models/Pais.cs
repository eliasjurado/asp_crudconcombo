using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EL2.Models
{
    public class Pais
    {
        [Display(Name = "codigo", Order = 0)]
        public string idpais { get; set; }
        [Display(Name = "nombre", Order = 1)]
        public string nompais { get; set; }
    }
}