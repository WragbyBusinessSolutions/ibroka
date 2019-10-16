using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ibroka.Models.Report
{
  public class MonthlyPremiumReport
  {
    public Guid Id { get; set; }

    public string PolicyNo { get; set; }
    public string EndorsementNo { get; set; }
    public string TransactionType { get; set; }
    public DateTime StartDatePeriodOfCover { get; set; }
    public DateTime EndDatePeriodOfCover { get; set; }
    public string NameOfAssured { get; set; }
    public string CustomerName { get; set; }
    public string NameOfOtherBrokersAgents { get; set; }
    public float SumInsured { get; set; }
    public float GrossPremium { get; set; }
    public float BrokerageCommission { get; set; }
    public float NetPremium { get; set; }
    public string PolicyTenorDays { get; set; }
    public string DebitNoteNo { get; set; }
    public string CreditNoteNo { get; set; }
    public DateTime CreditNoteDate { get; set; }
    public float AmountReceived { get; set; }
    public float DateOfReceiptOfPremiun { get; set; }
    public string ReceiptNoToInsured { get; set; } 
    public string NameOfBankOfLodgement { get; set; }
    public DateTime DateOfLodgement { get; set; }
    public string NameOfInsurer { get; set; }
    public float AmountRemitted { get; set; }
    public float AmountUnremitted { get; set; }
    public DateTime DatePaidTransferred { get; set; }
    public string NameOfBank { get; set; }
    public string ChequeNo { get; set; }
    public string InsurerReceiptNo { get; set; }
    public DateTime DateToRemitBalance { get; set; }
    public string Remarks { get; set; }


  }
}
