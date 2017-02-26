<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html>
<script runat="server">
    private void Page_Init(object sender, System.EventArgs e)
    {
        Context.Handler = this.Page;
    }
    
    
    protected void Page_Load(object sender, EventArgs e)
    {
        _69CSI.Models.csidbEntities db = new _69CSI.Models.csidbEntities();
        _69CSI.Models.tbl_CSI_69_Con_Transections Trans = new _69CSI.Models.tbl_CSI_69_Con_Transections();
        Trans.UserId = Convert.ToInt64("1");
        Trans.AmountToPay = Convert.ToDecimal(2);
        Trans.AmountPaid = Convert.ToDecimal(0);
        Trans.TransDate = DateTime.Now.Date;
        
        Trans.Status = "N";
        Trans.Remarks = "";
        int instResult = 0;
        String bdUserId = "1";
        String bdAmount = Trans.AmountToPay.ToString();
        String bdTransId = Trans.TransId.ToString();
        //instResult = db.tbl_CSI_69_Con_Transections(Trans);
        db.tbl_CSI_69_Con_Transections.Add(Trans);
        db.SaveChanges();
        if (instResult == 0)
        {
            
            GlobalData myUtility = new GlobalData();
            //String data = "CARDIOSOCT|1000000000|NA|12.00|NA|NA|NA|INR|DIRECT|R|cardiosoct|NA|NA|F|111111111|NA|NA|NA|NA|NA|NA|NA";
            //String data = "CARDIOSOCT|14|NA|2.00|NA|NA|NA|INR|NA|NA|cardiosoct|NA|NA|NA|03|NA|NA|NA|NA|NA|NA|http://69thac.csi.org.in/home/thankyou";
            String data = "CARDIOSOCT|14|NA|2.00|NA|NA|NA|INR|NA|R|cardiosoct|NA|NA|F|03|NA|NA|NA|NA|NA|NA|http://69thac.csi.org.in/home/thankyou";
            // String commonkey = "Your checksum key here";
            String commonkey = "CMcyWPzYC8DH";
            // SHASample dataprg = new SHASample();
            String hash = String.Empty;
            // hash = dataprg.GetHMACSHA256(data, commonkey);
            hash = myUtility.GetHMACSHA256(data, commonkey);
            hash = hash + "|" + hash.ToUpper() + "|" + commonkey;
            //hash = hash + "|" + "http://69thac.csi.org.in/home/thankyou";
            Response.Redirect("https://www.billdesk.com/pgidsk/PGIMerchantPayment?" + hash);
        }

  
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
