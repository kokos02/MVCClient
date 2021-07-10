using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVCClient.Models
{
    public class Customers 
    {
        [Key]
        public int CustomerId { get; set; }

        [StringLength(150)]
        [Required]
        public string Name { get; set; }

        [StringLength(250)]
        [Required]
        public string Address { get; set; }

        public int CustomerTypeId { get; set; }

        [ForeignKey(nameof(CustomerTypeId))]
        public virtual CustomerTypes CustomerType { get; set; }

        public virtual ICollection<Activities> Activities { get; set; } = new HashSet<Activities>();
    }
}
