using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Net;
using System.Collections.Generic;
using System.Web.WebPages.Html;
using System.Text;
using System.Security.Cryptography;
using System.Net.Mail;
using _69CSI.Models;


public class GlobalData
{
    private csidbEntities db = new csidbEntities();
    public GlobalData()
    {
        //
        // TODO: Add constructor logic here
        //
    }


    public string GetHMACSHA256(string text, string key)
    {
        UTF8Encoding encoder = new UTF8Encoding();

        byte[] hashValue;
        byte[] keybyt = encoder.GetBytes(key);
        byte[] message = encoder.GetBytes(text);

        HMACSHA256 hashString = new HMACSHA256(keybyt);
        string hex = "";

        hashValue = hashString.ComputeHash(message);
        foreach (byte x in hashValue)
        {
            hex += String.Format("{0:x2}", x);
        }
        return hex;
    }

    public string GetSHA256(string text)
    {
        UTF8Encoding encoder = new UTF8Encoding();

        byte[] hashValue;
        byte[] message = encoder.GetBytes(text);

        SHA256Managed hashString = new SHA256Managed();
        string hex = "";

        hashValue = hashString.ComputeHash(message);
        foreach (byte x in hashValue)
        {
            hex += String.Format("{0:x2}", x);
        }
        return hex;
    }



    //public static void Main(string[] args)
    //{
    //    String data = "TEST|123456|NA|2.00|NA|NA|NA|INR|DIRECT|R|test|NA|NA|F|31111111|TEST|test@test.com|1111111111|TEST|NA|NA|https://www.billdesk.com|Test123";
    //    GlobalData dataprg = new GlobalData();
    //    String hash = String.Empty;
    //    hash = dataprg.GetSHA256(data);
    //    Console.Out.WriteLine("CRC32_2 {0}", hash);
    //    Console.ReadKey();

    //}
    public static string UploadFile(HttpPostedFile file, string strFolderName, bool bAddTimeStamp)
    {
        try
        {
            if (file != null && !String.IsNullOrEmpty(file.FileName) && file.ContentLength > 0)
            {
                int l_pos = file.FileName.LastIndexOf(@"\");
                string l_strFileName = file.FileName.Substring(l_pos + 1);

                if (bAddTimeStamp)
                {
                    string[] splittedStrings = SplitStringFromLast(l_strFileName, @".");
                    if (splittedStrings != null)
                    {
                        string strExtension = splittedStrings[1];
                        string strFileNameOnly = splittedStrings[0];
                        l_strFileName = strFileNameOnly.Replace(" ", "_") + "_" + DateTime.Now.Ticks + "." + strExtension;
                    }
                }
                file.SaveAs(strFolderName + l_strFileName);
                return l_strFileName;
            }
            else if (file.FileName == "")
                return "";

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return null;
    }

    public static string[] SplitStringFromLast(string strInput, string splitBy)
    {
        int pos = strInput.LastIndexOf(splitBy);
        if (pos > 0)
        {
            string strLast = strInput.Substring(pos + 1);
            string strFirst = strInput.Substring(0, pos);
            return new string[] { strFirst, strLast };
        }
        return null;
    }

    public static string ReceiptNumber()
    {
        Random rnd = new Random();
        int myRandomNo = rnd.Next(10000000, 99999999);

        return myRandomNo.ToString();
    }
    public static string ImageUploadFile(HttpPostedFileBase[] ImageData, string path)
    {
        int i = 0;
        string l_strFileName = "";
        string l_strFileName2 = "";
        try
        {
            if (ImageData[i] != null && !String.IsNullOrEmpty(ImageData[i].FileName) && ImageData[i].ContentLength > 0)
            {
                int l_pos = ImageData[i].FileName.LastIndexOf(@"\");
                l_strFileName = ImageData[i].FileName.Substring(l_pos + 1);
                string[] splittedStrings = null;
                int pos = l_strFileName.LastIndexOf(@".");
                if (pos > 0)
                {
                    string strLast = l_strFileName.Substring(pos + 1);
                    string strFirst = l_strFileName.Substring(0, pos);
                    splittedStrings = new string[] { strFirst, strLast };
                }

                if (splittedStrings != null)
                {
                    string strExtension = splittedStrings[1];
                    string strFileNameOnly = splittedStrings[0];
                    l_strFileName = strFileNameOnly + "." + strExtension;
                    l_strFileName = strFileNameOnly.Replace(" ", "_") + "_" + DateTime.Now.Ticks + "." + strExtension;
                    l_strFileName2 = strFileNameOnly + "." + strExtension;
                }

                ImageData[i].SaveAs(path + l_strFileName);
                l_strFileName = "";
            }
        }
        catch (Exception ex)
        {

            l_strFileName = "";
        }
        return l_strFileName2;
    }

    public static string GetCurrentFinancialYear()
    {

        int CurrentYear = DateTime.Today.Year;
        int PreviousYear = CurrentYear - 1;
        int NextYear = DateTime.Today.Year + 1;
        string PreYear = PreviousYear.ToString();
        string NexYear = NextYear.ToString();
        string CurYear = CurrentYear.ToString();
        string FinYear = null;

        if (DateTime.Today.Month > 3)
            FinYear = CurYear + "-" + NexYear;
        else
            FinYear = PreYear + "-" + CurYear;

        return FinYear.Trim();

    }
    public static List<string> GetFinancialYearList()
    {
        List<string> cartdetail = new List<string>();

        for (int yr = System.DateTime.Now.Year; yr >= 2000; yr--)
        {
            int mnth = System.DateTime.Now.Month;
            if (mnth > 3)
                cartdetail.Add(yr + "-" + (yr + 1));
            else
                cartdetail.Add((yr - 1) + "-" + yr);
        }
        return cartdetail.ToList();
    }
    public static List<string> GetYearList()
    {
        List<string> cartdetail = new List<string>();
        for (int yr = System.DateTime.Now.Year; yr >= 2000; yr--)
        {
            cartdetail.Add(yr.ToString());
        }
        return cartdetail.ToList();
    }
    public static List<SelectListItem> GenerateYearList()
    {

        var numbers = (from p in GetYearList()
                       select new SelectListItem
                       {
                           Value = p.ToString(),
                           Text = p.ToString()

                       });
        return numbers.ToList();
    }

    public static decimal ExchangeRateFromAPI(decimal amount, string firstCcode, string lastCcode)
    {

        try
        {
            WebClient web = new WebClient();

            const string urlPattern = "http://finance.yahoo.com/d/quotes.csv?s={0}{1}=X&f=l1";

            string url = String.Format(urlPattern, firstCcode, lastCcode);

            // Get response as string

            string response = new WebClient().DownloadString(url);

            // Convert string to number

            decimal exchangeRate = decimal.Parse(response, System.Globalization.CultureInfo.InvariantCulture);

            return exchangeRate;

        }

        catch (Exception ex)
        {

            return 0;

        }

    }

    public void AdminMail(string email)
    {
        try
        {

            MailAddress mailFrom = new MailAddress(email);
            MailAddress mailTo = new MailAddress("satya@microbaseinfotech.com");
            // MailMessage mail = new MailMessage(mailFrom, mailTo);
           MailMessage mail = new MailMessage("satya@microbaseinfotech.com", "microbase@gmail.com");
           // MailMessage mail = new MailMessage("microbase@gmail.com", "satya@microbaseinfotech.com");
           //MailMessage mail = new MailMessage("microbase@gmail.com", "prasenjit@microbaseinfotech.com");

            mail.IsBodyHtml = true;
           string strMessage = "New Registration";
         
            mail.Subject = strMessage;
          
            strMessage = "<div align='center'><table border='0' width='560' cellspacing='0' cellpadding='0' id='table1'>";
            strMessage += "<tr><td colspan='3'><hr style='width: 100%' /></td></tr>";
            strMessage += "<tr><td style='vertical-align: top; width: 10px;'>&nbsp;</td><td style='text-align: left;'><br />A new Registration has been done in 69th Annual Conference of Cardiological Society of India Kolkata 2017 Website.<br /><br /><b>Administrator</b><br />69th Annual Conference of Cardiological Society of India Kolkata 2017</td><td>&nbsp;</td></tr>";
            strMessage += "<tr><td colspan='3'><hr style='width: 100%' /></td></tr></table></div>";
            mail.Body = strMessage;
           
          
            SmtpClient client = new SmtpClient();
            client.Host = "127.0.0.1";
            client.EnableSsl = false;
            client.Port = 26;
            client.Send(mail);
            //==================================================================================
            //mail = new MailMessage("microbase@gmail.com", "satya@microbaseinfotech.com");
            //mail.IsBodyHtml = true;
            //client.Host = "127.0.0.1";
            //client.EnableSsl = false;
            //client.Port = 26;
            //client.Send(mail);
        }
        catch (Exception ex)
        {
            //m_lblMessage.Text = (string)("Mail seding failed");
            throw ex;
            // return false;
        }
        // return true;
    }

    public void RegisterSendMail(string email, string name, long id, long tranid)
    {
        tbl_CSI_69_Con_RegistrationDetails tbl_csi_69_con_registrationdetails = db.tbl_CSI_69_Con_RegistrationDetails.Find(id);
        tbl_CSI_69_Con_Transections Trans = db.tbl_CSI_69_Con_Transections.Find(tranid);
        List<tbl_CSI_69_Con_Accompanys> objgroup = new List<tbl_CSI_69_Con_Accompanys>();
        objgroup = db.tbl_CSI_69_Con_Accompanys.Where(t => t.RegId == id).ToList();

        //SelectList <tbl_CSI_69_Con_Accompanys> acmp = new SelectListItem<tbl_CSI_69_Con_Accompanys>
        string type = tbl_csi_69_con_registrationdetails.Category;
        string Date = tbl_csi_69_con_registrationdetails.CreatedDate.ToString("dd'/'MMM'/'yyyy");
        string Reference_No = Trans.Extra3;
        string Amount = tbl_csi_69_con_registrationdetails.Payment_Amount.ToString()+"/-";
        string Accompanying_Person = "0";
        int veg=0; int nonveg=0;


        if (objgroup.Count() > 0) { Accompanying_Person = (objgroup.Count()).ToString(); } else { Accompanying_Person = "NA"; }

        string Choice_of_food = "";

        if (tbl_csi_69_con_registrationdetails.Foodtype == "vegetarian")
        {veg++;}
        else if (tbl_csi_69_con_registrationdetails.Foodtype == "nonvegetarian")
        {nonveg++;}
      

        if (objgroup.Count() > 0) 
        {
            foreach (var item in objgroup)
            {
                if (item.Meal_Preference == "Veg") 
                { veg++; }

                else if (tbl_csi_69_con_registrationdetails.Foodtype == "Non-Veg")
                { nonveg++; }               
            }
        
        }

        Choice_of_food = veg.ToString() + " " + "Veg" + " ," + nonveg.ToString() + " " + "Non-Veg";

             if (type == "1") { type = "CSI Member"; }
        else if (type == "2") { type = " Non-CSI Member "; }
        else if (type == "3") { type = "CSI Member"; }
        else if (type == "4") { type = " PG Students"; }
        else if (type == "5") { type = " Industry Professional"; }
        else if (type == "6") { type = " Nurse/Technician"; }
        else if (type == "7") { type = " SAARC Countries (Non-CSI Member)"; }
        else if (type == "8") { type = " Non-SAARC Foreign Nationals"; }

        try
        {
            MailAddress mailTo = new MailAddress(email, name);
            MailAddress mailFrom = new MailAddress("satya@microbaseinfotech.com");
            // MailMessage mail = new MailMessage(mailFrom, mailTo);
            MailMessage mail = new MailMessage("satya@microbaseinfotech.com", "microbase@gmail.com");
           // MailMessage mail = new MailMessage("microbase@gmail.com", "satya@microbaseinfotech.com");
           // MailMessage mail = new MailMessage("microbase@gmail.com", "prasenjit@microbaseinfotech.com");
            mail.IsBodyHtml = true;
            string strsubj = "Welcome to 69th Annual Conference of CSI Kolkata-2017";
            mail.Subject = strsubj;
            string strMessage = "";
            strMessage = "<div align=\"center\">";
            strMessage += "<table border=\"1\" width=\"798\" cellpadding=\"0\" style=\"border-collapse: collapse\" bordercolor=\"#F0F0F0\">";
            strMessage += "<tr>";
            strMessage += "<td>";
            strMessage += "<table border=\"0\" width=\"100%\" cellspacing=\"0\" cellpadding=\"0\">";
            strMessage += "<tr>";
            strMessage += "<td colspan=\"2\"><img border=\"0\" src=\"http://csicon2017.org/images/logo0000.png\" width=\"302\"></td>";
            strMessage += "<td colspan=\"3\"><h3 style=\"padding:0 25px; font-family: Trebuchet ms;\">Welcome to 69th Annual Conference of Cardiological Society of India <span class=\"span\">Kolkata, 30<sup>th</sup> Nov. to 3rd Dec. 2017</span></h3></td>";
            strMessage += "</tr>";
            strMessage += "<tr>";
            strMessage += "<td colspan=\"5\"><hr style=\"border-top:1px solid #e5e5e5;border-left:none;border-right:none;border-bottom:none;\"></td>";
            strMessage += "</tr>";
            strMessage += "<tr>";
            strMessage += "<td width=\"3%\">&nbsp;</td>";
            strMessage += "<td width=\"93%\" colspan=\"3\">&nbsp;</td>";
            strMessage += "<td width=\"4%\">&nbsp;</td>";
            strMessage += "</tr>";
            strMessage += "<tr>";
            strMessage += "<td width=\"3%\">&nbsp;</td>";
            strMessage += "<td width=\"93%\" colspan=\"3\"><b>";
            strMessage += "<font style=\"font-family:Trebuchet ms;font-size:14pt;font-weight:bold;color:#E4252C;\">Dear "+tbl_csi_69_con_registrationdetails.Prefix+"&nbsp;"+tbl_csi_69_con_registrationdetails.Name+"</font></b></td>";
            strMessage += "<td width=\"4%\">&nbsp;</td>";
            strMessage += "</tr>";
            strMessage += "<tr>";
            strMessage += "<td width=\"3%\">&nbsp;</td>";
            strMessage += "<td width=\"93%\" colspan=\"3\">&nbsp;</td>";
            strMessage += "<td width=\"4%\">&nbsp;</td>";
            strMessage += "</tr>";
            strMessage += "<tr>";
            strMessage += "<td width=\"3%\">&nbsp;</td>";
            strMessage += "<td width=\"93%\" colspan=\"3\"><font style=\"font-family:Trebuchet ms;font-size:10pt;letter-spacing:1px;font-weight:normal;color:#333;line-height:20px;\">Your Registration has been accepted with successful confirmation of payment as per the below details:</font></td>";
            strMessage += "<td width=\"4%\">&nbsp;</td>";
            strMessage += "</tr>";
            strMessage += "<tr>";
            strMessage += "<td width=\"3%\">&nbsp;</td>";
            strMessage += "<td width=\"93%\" colspan=\"3\">&nbsp;</td>";
            strMessage += "<td width=\"4%\">&nbsp;</td>";
            strMessage += "</tr>";
            strMessage += "<tr height=\"35\">";
            strMessage += "<td width=\"3%\">&nbsp;</td>";
            strMessage += "<td width=\"31%\"><font style=\"font-family:Trebuchet ms;font-size:13pt;letter-spacing:1px;font-weight:normal;color:#333;line-height:22px;font-weight:bold;\">Category :</font></td>";
            strMessage += "<td width=\"31%\"><font style=\"font-family:Trebuchet ms;font-size:13pt;letter-spacing:1px;font-weight:normal;color:#E4252C;line-height:22px;font-weight:bold;\">" + type + "</font></td>";
            strMessage += "<td width=\"31%\">&nbsp;</td>";
            strMessage += "<td width=\"4%\">&nbsp;</td>";
            strMessage += "</tr>";
            strMessage += "<tr height=\"35\">";
            strMessage += "<td width=\"3%\">&nbsp;</td>";
            strMessage += "<td width=\"31%\"><font style=\"font-family:Trebuchet ms;font-size:13pt;letter-spacing:1px;font-weight:normal;color:#333;line-height:22px;font-weight:bold;\">";
            strMessage += "Payment Date :</font></td>";
            strMessage += "<td width=\"31%\"><font style=\"font-family:Trebuchet ms;font-size:13pt;letter-spacing:1px;font-weight:normal;color:#E4252C;line-height:22px;font-weight:bold;\">"+Date+"</font></td>";
            strMessage += "<td width=\"31%\">&nbsp;</td>";
            strMessage += "<td width=\"4%\">&nbsp;</td>";
            strMessage += "</tr>";
            strMessage += "<tr height=\"35\">";
            strMessage += "<td width=\"3%\">&nbsp;</td>";
            strMessage += "<td width=\"31%\">";
            strMessage += "<span style=\"font-family: Trebuchet ms; font-weight: 700; font-size: 13pt; color: #333333; letter-spacing: 1px\">";
            strMessage += "Payment Reference No :</span></td>";
            strMessage += "<td width=\"31%\">";
            strMessage += "<span style=\"font-family: Trebuchet ms; font-weight: 700; font-size: 13pt; color: #E4252C; letter-spacing: 1px\">";
            strMessage += Reference_No +"</span></td>";
            strMessage += "<td width=\"31%\">&nbsp;</td>";
            strMessage += "<td width=\"4%\">&nbsp;</td>";
            strMessage += "</tr>";
            strMessage += "<tr height=\"35\">";
            strMessage += "<td width=\"3%\">&nbsp;</td>";
            strMessage += "<td width=\"31%\">";
            strMessage += "<span style=\"font-family: Trebuchet ms; font-weight: 700; font-size: 13pt; color: #333333; letter-spacing: 1px\">";
            strMessage += "Amount :</span></td>";
            strMessage += "<td width=\"31%\">";
            strMessage += "<span style=\"font-family: Trebuchet ms; font-weight: 700; font-size: 13pt; color: #E4252C; letter-spacing: 1px\">";
            strMessage += Amount+"</span></td>";
            strMessage += "<td width=\"31%\">&nbsp;</td>";
            strMessage += "<td width=\"4%\">&nbsp;</td>";
            strMessage += "</tr>";
            strMessage += "<tr height=\"35\">";
            strMessage += "<td width=\"3%\">&nbsp;</td>";
            strMessage += "<td width=\"31%\">";
            strMessage += "<span style=\"font-family: Trebuchet ms; font-weight: 700; font-size: 13pt; color: #333333; letter-spacing: 1px\">";
            strMessage += "Accompanying Person :</span></td>";
            strMessage += "<td width=\"31%\">";
            strMessage += "<span style=\"font-family: Trebuchet ms; font-weight: 700; font-size: 13pt; color: #E4252C; letter-spacing: 1px\">";
            strMessage += Accompanying_Person+"</span></td>";
            strMessage += "<td width=\"31%\">&nbsp;</td>";
            strMessage += "<td width=\"4%\">&nbsp;</td>";
            strMessage += "</tr>";
            strMessage += "<tr height=\"35\">";
            strMessage += "<td width=\"3%\">&nbsp;</td>";
            strMessage += "<td width=\"31%\">";
            strMessage += "<span style=\"font-family: Trebuchet ms; font-weight: 700; font-size: 13pt; color: #333333; letter-spacing: 1px\">";
            strMessage += "Choice of food :</span></td>";
            strMessage += "<td width=\"31%\">";
            strMessage += "<span style=\"font-family: Trebuchet ms; font-weight: 700; font-size: 13pt; color: #E4252C; letter-spacing: 1px\">";
            strMessage += Choice_of_food+"</span></td>";
            strMessage += "<td width=\"31%\">&nbsp;</td>";
            strMessage += "<td width=\"4%\">&nbsp;</td>";
            strMessage += "</tr>";
            strMessage += "<tr>";
            strMessage += "<td width=\"3%\">&nbsp;</td>";
            strMessage += "<td width=\"93%\" colspan=\"3\">&nbsp;</td>";
            strMessage += "<td width=\"4%\">&nbsp;</td>";
            strMessage += "</tr>";
            strMessage += "<tr>";
            strMessage += "<td width=\"3%\">&nbsp;</td>";
            strMessage += "<td width=\"93%\" colspan=\"3\">";
            strMessage += "<span style=\"letter-spacing: 1px;font-family: Trebuchet ms;font-style:italic;color:#333333;font-size:12pt;\">You can login to the Conference Portal using the following details:</b></span></td>";
            strMessage += "<td width=\"4%\">&nbsp;</td>";
            strMessage += "</tr>";
            strMessage += "<tr>";
            strMessage += "<td width=\"3%\">&nbsp;</td>";
            strMessage += "<td width=\"93%\" colspan=\"3\">&nbsp;</td>";
            strMessage += "<td width=\"4%\">&nbsp;</td>";
            strMessage += "</tr>";
            strMessage += "<tr height=\"35\">";
            strMessage += "<td width=\"3%\">&nbsp;</td>";
            strMessage += "<td width=\"31%\">";
            strMessage += "<span style=\"font-family: Trebuchet ms; font-weight: 700; font-size: 13pt; color: #333333; letter-spacing: 1px\">";
            strMessage += "Username :</span></td>";
            strMessage += "<td width=\"31%\">";
            strMessage += "<span style=\"font-family: Trebuchet ms; font-weight: 300; font-size: 16pt; color: #E4252C; letter-spacing: 1px\">";
            strMessage += tbl_csi_69_con_registrationdetails.Username+"</span></td>";
            strMessage += "<td width=\"31%\">&nbsp;</td>";
            strMessage += "<td width=\"4%\">&nbsp;</td>";
            strMessage += "</tr>";
            strMessage += "<tr height=\"35\">";
            strMessage += "<td width=\"3%\">&nbsp;</td>";
            strMessage += "<td width=\"31%\">";
            strMessage += "<span style=\"font-family: Trebuchet ms; font-weight: 700; font-size: 13pt; color: #333333; letter-spacing: 1px\">";
            strMessage += "Password :</span></td>";
            strMessage += "<td width=\"31%\">";
            strMessage += "<span style=\"font-family: Trebuchet ms; font-weight: 300; font-size: 16pt; color: #E4252C; letter-spacing: 1px\">";
            strMessage += tbl_csi_69_con_registrationdetails.Password+"</span></td>";
            strMessage += "<td width=\"31%\">&nbsp;</td>";
            strMessage += "<td width=\"4%\">&nbsp;</td>";
            strMessage += "</tr>";
            strMessage += "<tr>";
            strMessage += "<td width=\"3%\">&nbsp;</td>";
            strMessage += "<td width=\"93%\" colspan=\"3\">&nbsp;</td>";
            strMessage += "<td width=\"4%\">&nbsp;</td>";
            strMessage += "</tr>";
            strMessage += "<tr>";
            strMessage += "<td width=\"3%\">&nbsp;</td>";
            strMessage += "<td width=\"93%\" colspan=\"3\">";
            strMessage += "<span style=\"letter-spacing: 1px;font-family: Trebuchet ms;font-style:italic;color:#E4252C;font-size:12pt;\">Kindly treat this email as original receipt of your transaction.</b></span></td>";
            strMessage += "<td width=\"4%\">&nbsp;</td>";
            strMessage += "</tr>";
            strMessage += "<tr>";
            strMessage += "<td width=\"3%\">&nbsp;</td>";
            strMessage += "<td width=\"93%\" colspan=\"3\">&nbsp;</td>";
            strMessage += "<td width=\"4%\">&nbsp;</td>";
            strMessage += "</tr>";
            strMessage += "<tr>";
            strMessage += "<td width=\"3%\">&nbsp;</td>";
            strMessage += "<td width=\"93%\" colspan=\"3\">&nbsp;</td>";
            strMessage += "<td width=\"4%\">&nbsp;</td>";
            strMessage += "</tr>";
            strMessage += "<tr>";
            strMessage += "<td width=\"3%\">&nbsp;</td>";
            strMessage += "<td width=\"93%\" colspan=\"3\"><font style=\"letter-spacing: 1px;font-family: Trebuchet ms;font-style:italic;color:#333333;font-size:12pt;line-height:25px;\">Regards";
            strMessage += "<br>";
            strMessage += "</font>";
            strMessage += "<b>";
            strMessage += "<font style=\"letter-spacing: 1px;font-family: Trebuchet ms;font-style:italic;font-size:12pt;line-height:25px\" color=\"#E4252C\">Dr. Mrinal Kanti Das, Organizing Secretary</font><font style=\"letter-spacing: 1px;font-family: Trebuchet ms;font-style:italic;color:#333333;font-size:12pt;line-height:25px;\"> ";
            strMessage += "<br>";
            strMessage += "</font>";
            strMessage += "<font style=\"letter-spacing: 1px;font-family: Trebuchet ms;font-style:italic;color:#333333;font-size:11pt;line-height:25px\"> ";
            strMessage += "Indian Heart House, P-60, C.I.T Road,<br>Scheme - VII-M, Kankurgachi,<br>Kolkata - 700054, India<br>Telephone: +91 33 2355 7837, 2355 6308, 2355 1500<br>Email: </font></b><font style=\"letter-spacing: 1px;font-family: Trebuchet ms;font-style:italic;color:#333333;font-size:11pt;line-height:25px\"> ";
            strMessage += "<a href=\"mailto:secretary@csicon2017.org\">";
            strMessage += "<font color=\"#E4252C\"><span style=\"text-decoration: none\">secretary@csicon2017.org</span></font></a> / <a href=\"http://www.csicon2017.org\">";
            strMessage += "<font color=\"#E4252C\"><span style=\"text-decoration: none\">www.csicon2017.org</span></font></a></font></td>";
            strMessage += "<td width=\"4%\">&nbsp;</td>";
            strMessage += "</tr>";
            strMessage += "<tr>";
            strMessage += "<td width=\"3%\">&nbsp;</td>";
            strMessage += "<td width=\"93%\" colspan=\"3\">&nbsp;</td>";
            strMessage += "<td width=\"4%\">&nbsp;</td>";
            strMessage += "</tr>";
            strMessage += "<tr height=\"40\">";
            strMessage += "<td width=\"3%\" bgcolor=\"#ed3237\">&nbsp;</td>";
            strMessage += "<td width=\"93%\" bgcolor=\"#ed3237\" align=\"right\" colspan=\"3\">";
            strMessage += "<a style=\"text-decoration: none;\" target=\"_blank\" href=\"http://csicon2017.org/\">";
            strMessage += "<span style=\"text-decoration: none;font-family: Trebuchet ms;color:#fff;font-weight:bold;font-size:10pt;\">www.csicon2017.org</span></a></td>";
            strMessage += "<td width=\"4%\" bgcolor=\"#ed3237\">&nbsp;</td>";
            strMessage += "</tr>";
            strMessage += "</table>";
            strMessage += "</td>";
            strMessage += "</tr>";
            strMessage += "</table>";
            strMessage += "</div>";

            mail.Body = strMessage;
            //SmtpClient client = new SmtpClient((string)ConfigurationManager.AppSettings["NIC2013.smtpserver"]);
            //client.EnableSsl = false;
            // client.Port = 26;
            //client.Send(mail);
            SmtpClient client = new SmtpClient();
            client.Host = "127.0.0.1";
            client.EnableSsl = false;
            client.Port = 26;
            client.Send(mail);
            //==================================================
            //mail = new MailMessage("microbase@gmail.com", "satya@microbaseinfotech.com");
            //mail.IsBodyHtml = true;
            //client.Host = "127.0.0.1";
            //client.EnableSsl = false;
            //client.Port = 26;
            //client.Send(mail);


        }
        catch (Exception ex)
        {
            //m_lblMessage.Text = (string)("Mail seding failed");
            throw ex;

        }

    }
}


