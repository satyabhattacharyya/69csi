<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html>
<script runat="server">
    private void Page_Init(object sender, System.EventArgs e)
    {
        Context.Handler = this.Page;
    }   
    
    protected void Page_Load(object sender, EventArgs e)
    {     
      
        string CheckSumKey = "CMcyWPzYC8DH";
        string MerchantID = "CARDIOSOCT";
        string CustomerID = "14";
        string TransactionAmt = "2.00";
        string CurrencyType = "INR";
        string TypeField1 = "R";
        string SecurityID = "cardiosoct";
        string TypeField2 = "F";
        string AdditionalInfo1 = "NA";
        string AdditionalInfo2 = "NA";
        string AdditionalInfo3 = "NA";
        string AdditionalInfo4 = "NA";
        string AdditionalInfo5 = "NA";
        string AdditionalInfo6 = "NA"; 
        string AdditionalInfo7 = "NA";
        string ResponseURL = "http://69thac.csi.org.in/home/thankyou";

        GlobalData myUtility = new GlobalData();
       
        string msg = MerchantID + "|" + CustomerID + "|" + "NA" + "|" + TransactionAmt + "|" + "NA|NA|NA|" + CurrencyType + "|" + "NA" + "|" + TypeField1 + "|" + SecurityID + "|" + "NA|NA|" + TypeField2 + "|" + AdditionalInfo1 + "|" + AdditionalInfo2 + "|" + AdditionalInfo3 + "|" + AdditionalInfo4 + "|" + AdditionalInfo5 + "|" + AdditionalInfo6 + "|" + AdditionalInfo7 + "|" + ResponseURL;

        string hashData = msg;

        string checksumvalue =myUtility.GetHMACSHA256(hashData, CheckSumKey);

        checksumvalue = checksumvalue.ToUpper();

        checksumvalue = hashData + "|" + checksumvalue;

        Response.Redirect("https://pgi.billdesk.com/pgidsk/PGIMerchantPayment?msg=" + checksumvalue);


        Console.Out.WriteLine("HMAC {0}", hashData);

        Console.ReadKey();     
      }
</script>
<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
           

        </div>
    </form>
</body>
</html>
