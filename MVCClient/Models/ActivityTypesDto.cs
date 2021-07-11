using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCClient.Models
{
    public class ActivityTypesDto
    {
        [Key]
        public int ActivityTypeId { get; set; }

        [StringLength(200)]
        [Required]
        public string Description { get; set; }
    }
}
