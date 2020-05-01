using System;
using System.ComponentModel.DataAnnotations;

namespace PgBookStore.Models
{
    public partial class ContohAtributModel
    {
        [Display(Name = "Contoh Email")]
        [DataType(DataType.EmailAddress)]
        public string ContohEmail {get; set;}

        [Display(Name = "Contoh URL")]
        [DataType(DataType.Url)]
        public string ContohUrl {get; set;}

        [Display(Name = "Contoh Phone")]
        [DataType(DataType.PhoneNumber)]
        public string ContohPhone {get; set;}

        [Display(Name = "Contoh Password")]
        [DataType(DataType.Password)]
        public string ContohPassword {get; set;}

        [Display(Name = "Contoh Date")]
        [DataType(DataType.Date)]
        public DateTime ContohDate {get; set;}

        [Display(Name = "Contoh Time")]
        [DataType(DataType.Time)]
        public DateTime ContohTime {get; set;}
    }
}