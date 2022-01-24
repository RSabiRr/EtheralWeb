using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EtheralWeb.Models
{
    public class Testimonial
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(500)]
        public string Text { get; set; }
        [MaxLength(250)]
        public string Image { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        [MaxLength(30)]
        public string UserName { get; set; }
        [MaxLength(30)]
        public string UserSurname { get; set; }
    }
}
