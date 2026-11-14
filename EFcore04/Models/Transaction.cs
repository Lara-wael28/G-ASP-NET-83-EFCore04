using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFcore04.Models
{
    public class Transaction
    {
        public int TransactionNumber { get; set; }

        public DateTime TransactionDate { get; set; }

        public decimal Amount { get; set; }

        public string TransactionType { get; set; }

        public string Note { get; set; }
        public string AccountNumber { get; set; }
        public Account Account { get; set; }

    }
}
