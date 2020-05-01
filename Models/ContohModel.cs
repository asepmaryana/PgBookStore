using System;
using System.ComponentModel.DataAnnotations;

namespace PgBookStore.Models
{
    public partial class ContohModel
    {
        [Display(Name = "Contoh Text")]
        public String ContohText {get; set;}

        [Display(Name = "Contoh Date Time")]
        public DateTime ContohDateTime {get; set;}

        [Display(Name = "Contoh Number")]
        public double ContohNumber {get; set;}

        [Display(Name = "Contoh Boolean")]
        public bool ContohBoolean {get; set;}
    }
}