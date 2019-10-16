using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ibroka.ViewModel
{
    public class PostIncome
    {
        public string AId { get; set; }
        public Guid IncomeTypeId { get; set; }
        public string Description { get; set; }
        public float Amount { get; set; }
    }
}
