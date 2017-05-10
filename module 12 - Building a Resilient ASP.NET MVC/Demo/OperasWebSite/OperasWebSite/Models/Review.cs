using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OperasWebSite.Models
{
    public class Review
    {
        public int ReviewID { get; set; }

        public int OperaID { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayName("Performance Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        
        [Required]
        public string Company { get; set; }

        [Required]
        [DisplayName("Review")]
        public string ReviewText { get; set; }

        public virtual Opera Opera { get; set; }

    }
}