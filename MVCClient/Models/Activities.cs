using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVCClient.Models
{
    public class Activities 
    {
        [Key]
        public int ActivitiesId { get; set; }

        [MaxLength(500)]
        [Required]
        public string Description { get; set; }

        public int CustomerId { get; set; }

        [ForeignKey(nameof(CustomerId))]
        public virtual Customers Customer { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int ActivityTypeId { get; set; }

        [ForeignKey(nameof(ActivityTypeId))]
        public virtual ActivityTypes ActivityType { get; set; }
    }
}
