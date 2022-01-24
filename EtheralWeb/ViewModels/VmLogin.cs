using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EtheralWeb.ViewModels
{
    public class VmLogin
    {
        [MaxLength(30)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [MaxLength(30)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
