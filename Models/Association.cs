using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace activity_center.Models
{
    public class Association
    {
        [Key]
        public int AssociationId { get; set; }  
        public int UserId { get; set; }
        public int ActivityId { get; set; }

        public User User { get; set; }
        public Activity Activity { get; set; }
    }
}