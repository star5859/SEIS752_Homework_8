using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SEIS752_MVC_WebApp_stark.Models
{
    
    public class FFUser
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 3)]
        public string Name {get; set;}
        public string Username { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
        [Url]
        public string ImageUrl { get; set; }
    }
 
}