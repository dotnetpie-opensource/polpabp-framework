using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolpAbp.Framework.Maintenance.Dtos
{
    public class OperationGuard
    {
        [Required]
        public string Password { get; set; }
    }
}
