using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CashflowCalculator.Models
{
    [Table("loans")]
    public class Loan
    {
        [Key]
        public int Id { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}")]
        public int Balance { get; set; }
        public int Term { get; set; }
        public double Rate { get; set; }
    }
}