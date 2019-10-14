using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E4S.Helpers
{
    public static class StaticContent
    {
        public const string CLIENT_CREAT_MESSAGE= "Client created successfully, please create lead.";
        public const string LEAD_CREAT_MESSAGE = "Lead created successfully.";
        public const string RECEIPT_UPLOAD_SUCCESS_MSG = "Receipt uploaded successfully.";
        public const string PAYMENT_GENERATE_SUCCESS_MSG = "Payment generated successfully.";
        public const string DEBITNOTE_GENERATE_SUCCESS_MSG = "Debit note generated successfully.";
        public const string POLICYNO_UPDATE = "Policy no updated successfully.";
        public const string DEBITNOTE_SUBJECT_TEMPLATE = "Debit note against @policyname";
        public const string DEBITNOTE_BODY_TEMPLATE = "@Greet @reciever,</br></br>Please find attached the debit note against your <b>@policyname</b>.</br></br></br>Regards,</br>@agentname</br>@organization";
        public const string PAYMENTRECEIPT_SUBJECT_TEMPLATE = "Payment Receipt note against @policyname";
        public const string PAYMENTRECEIPT_BODY_TEMPLATE = "@Greet @reciever,</br></br>Please find attached the receipt for your payment of @currency @receiptamount dated @receiptdate against your <b>@policyname</b>.</br></br></br>Regards,</br>@agentname</br>@organization";
        public const string LEAD_ENDORSEMENT_CREATE_MESSAGE = "Lead endorsement created successfully.";
        public const string INSURER_CREAT_MESSAGE = "Insurer created successfully.";
        public const string INSURER_Edit_MESSAGE = "Insurer saved successfully.";
        public const string LEAD_ENDORSEMENT_UPDATE_MESSAGE = "Lead endorsement updated successfully.";
        public const string ENDORSEMENTNO_UPDATE = "Endorsement no updated successfully.";
        public const string LEAD_UPDATE_MESSAGE = "Lead updated successfully.";
    }
}
