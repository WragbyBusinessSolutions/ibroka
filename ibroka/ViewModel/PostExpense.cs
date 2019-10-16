using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ibroka.ViewModel
{
    public class PostExpense
    {
        public string AId { get; set; }
        public Guid ExpenseTypeId { get; set; }
        public string Description { get; set; }
        public float Amount { get; set; }
    }
}
