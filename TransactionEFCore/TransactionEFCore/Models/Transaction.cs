using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace TransactionEFCore.Models
{
    public class Transaction
    {
        [Key]
        [Required]
        public int TransactioId { get; set; }
        [Required]
        [StringLength(100,ErrorMessage ="Enter the Name of Bank")]
        public string BankName { get;set; }
        [Required]
        [StringLength(50,ErrorMessage ="Enter the Branch of Bank")]
        public string Branch {  get;set; }
        [Required]
        [StringLength(14,MinimumLength =12,ErrorMessage ="Account no must be in 12 to 14 digits")]
        public int AccountNo { get;set; }
        public string Balance { get;set; }

        [Required]
        [StringLength(12,ErrorMessage ="IFS Code must be in 12 digits")]
        public int IFSCode { get;set; }

        [Required]
        [StringLength(100,ErrorMessage ="Enter the Name of Customer in words only")]
        public string CustomerName { get; set; }
    }
}
