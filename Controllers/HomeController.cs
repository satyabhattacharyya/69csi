using _69CSI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace csi.Controllers
{
    public class HomeController : Controller
    {
        private csidbEntities db = new csidbEntities();
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Return()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
        public ActionResult Dashboard()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        [HttpPost]
        public ActionResult Login(tbl_CSI_69_Con_RegistrationDetails tblusermastre, FormCollection form)
        {
            List<ValidateUser_Result> lstUserMaster = db.ValidateUser(tblusermastre.Username, tblusermastre.Password).ToList();
            if (lstUserMaster.Count > 0)
            {
                Session["UserloginId"] = lstUserMaster[0].Username;
                Session["UserId"] = lstUserMaster[0].RegId;
                long id = Convert.ToInt64(Session["UserId"]);
                return RedirectToAction("Dashboard", "Home");

            }
            else
            {
                ModelState.AddModelError("", "The user name or password provided is incorrect.");
                return View(tblusermastre);
            }

        }
        public ActionResult Thankyou1(string msg)
        {
            string[] arrResponse = msg.Split('|'); //PG
            string merchantId = arrResponse[0];
            string _customerId = arrResponse[1];
            string txnReferenceNo = arrResponse[2];
            string bankReferenceNo = arrResponse[3];
            string txnAmount = Convert.ToDecimal(arrResponse[4]).ToString();
            string bankId = arrResponse[5];
            string bankMerchantId = arrResponse[6];
            string txnType = arrResponse[7];
            string currency = arrResponse[8];
            string itemCode = arrResponse[9];
            string securityType = arrResponse[10];
            string securityId = arrResponse[11];
            string securityPassword = arrResponse[12];
            string txnDate = arrResponse[13]; //dd-mm-yyyy
            string authStatus = arrResponse[14];
            string settlementType = arrResponse[15];
            string additionalInfo1 = arrResponse[16];
            //string additionalInfo1 = "10";
            string additionalInfo2 = arrResponse[17];
            string additionalInfo3 = arrResponse[18];
            string additionalInfo4 = arrResponse[19];
            string additionalInfo5 = arrResponse[20];
            string additionalInfo6 = arrResponse[21];
            string additionalInfo7 = arrResponse[22];
            string errorStatus = arrResponse[23];
            string _errorDescription = arrResponse[24];
            string _CheckSum = arrResponse[25];
            GlobalData ga = new GlobalData();           

            if (authStatus == "0300")
            {
                long tranid = Convert.ToInt64(additionalInfo1);
                tbl_CSI_69_Con_Transections Trans = db.tbl_CSI_69_Con_Transections.Find(tranid);
                Trans.AmountPaid = Convert.ToDecimal(txnAmount);
                Trans.Extra3 = txnReferenceNo;
                Trans.Status = "Y";

                db.Entry(Trans).State = EntityState.Modified;
                db.SaveChanges();

                long custId = Convert.ToInt64(_customerId);
                tbl_CSI_69_Con_RegistrationDetails registration = db.tbl_CSI_69_Con_RegistrationDetails.Find(custId);
                ViewBag.Name = registration.Prefix + " " + registration.Name;
                int count = registration.tbl_CSI_69_Con_Accompanys.Count();
                if (count > 0)
                    CSIMemberMail(registration, "Yes");
                else
                    CSIMemberMail(registration, "No");
               // ga.AdminMail("satya@microbaseinfotech.com");
                ga.RegisterSendMail("satya@microbaseinfotech.com", "Satya Bhattacharyya", custId, tranid);
            }
           

            return View();

        }
        public ActionResult Thankyou()
        {
            //string msg1 ="";
            //if (Request.Form["msg"] != null)
            //{
            //    string _paymentResp = Request.Form["msg"].ToString();
            //    msg1 = _paymentResp;
            //    //return RedirectToAction("Thankyou1", "Home", new { msg = msg1 });
            //}

            //string[] arrResponse = msg1.Split('|'); //PG
            //string merchantId = arrResponse[0];
            //string _customerId = arrResponse[1];
            //string txnReferenceNo = arrResponse[2];
            //string bankReferenceNo = arrResponse[3];
            //string txnAmount = Convert.ToDecimal(arrResponse[4]).ToString();
            //string bankId = arrResponse[5];
            //string bankMerchantId = arrResponse[6];
            //string txnType = arrResponse[7];
            //string currency = arrResponse[8];
            //string itemCode = arrResponse[9];
            //string securityType = arrResponse[10];
            //string securityId = arrResponse[11];
            //string securityPassword = arrResponse[12];
            //string txnDate = arrResponse[13]; //dd-mm-yyyy
            //string authStatus = arrResponse[14];
            //string settlementType = arrResponse[15];
            //string additionalInfo1 = arrResponse[16];
            ////string additionalInfo1 = "10";
            //string additionalInfo2 = arrResponse[17];
            //string additionalInfo3 = arrResponse[18];
            //string additionalInfo4 = arrResponse[19];
            //string additionalInfo5 = arrResponse[20];
            //string additionalInfo6 = arrResponse[21];
            //string additionalInfo7 = arrResponse[22];
            //string errorStatus = arrResponse[23];
            //string _errorDescription = arrResponse[24];
            //string _CheckSum = arrResponse[25];
            //GlobalData ga = new GlobalData();

            //if (authStatus == "0300")
            //{
            //    long tranid = Convert.ToInt64(additionalInfo1);
            //    tbl_CSI_69_Con_Transections Trans = db.tbl_CSI_69_Con_Transections.Find(tranid);
            //    Trans.AmountPaid = Convert.ToDecimal(txnAmount);
            //    Trans.Extra3 = txnReferenceNo;
            //    Trans.Status = "Y";

            //    db.Entry(Trans).State = EntityState.Modified;
            //    db.SaveChanges();

            //    long custId = Convert.ToInt64(_customerId);
            //    tbl_CSI_69_Con_RegistrationDetails registration = db.tbl_CSI_69_Con_RegistrationDetails.Find(custId);
            //    ViewBag.Name = registration.Prefix + " " + registration.Name;
            //    int count = registration.tbl_CSI_69_Con_Accompanys.Count();
            //    //if (count > 0)
            //    //  //  CSIMemberMail(registration, "Yes");
            //    //else
            //    //    CSIMemberMail(registration, "No");
            //    // ga.AdminMail("satya@microbaseinfotech.com");
            //    ga.RegisterSendMail("satya@microbaseinfotech.com", "Satya Bhattacharyya", custId, tranid);
            //}
           
           
            return View();
           
        }

        private void CSIMemberMail(tbl_CSI_69_Con_RegistrationDetails registration, string accompany)
        {
            MailAddress mailFrom = new MailAddress("secretary@csicon2017.org", "69th Annual Conference of CSI");
            MailAddress mailTo = new MailAddress(registration.Email, registration.Name);
            MailMessage mail = new MailMessage(mailFrom, mailTo);
            mail.IsBodyHtml = true;
            string strMessage = "Thank you for your registration";
            mail.Subject = strMessage;


            strMessage += "<div align=\"center\">";
            strMessage += "<table border=\"1\" width=\"798\" cellpadding=\"0\" style=\"border-collapse: collapse\" bordercolor=\"#F0F0F0\">";
            strMessage += "<tr>";
            strMessage += "<td>";
            strMessage += "<table border=\"0\" width=\"100%\" cellspacing=\"0\" cellpadding=\"0\">";
            strMessage += "<tr>";
            strMessage += "<td colspan=\"5\">";
            strMessage += "<img border=\"0\" src=\"http://csicon2017.org/images/logo0000.png\" width=\"302\"></td>";
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
            strMessage += "<font style=\"font-family:Trebuchet ms;font-size:14pt;font-weight:bold;color:#E4252C;\">Dear " + registration.Prefix + " " + registration.Name + "</font></b></td>";
            strMessage += "<td width=\"4%\">&nbsp;</td>";
            strMessage += "</tr>";
            strMessage += "<tr>";
            strMessage += "<td width=\"3%\">&nbsp;</td>";
            strMessage += "<td width=\"93%\" colspan=\"3\">&nbsp;</td>";
            strMessage += "<td width=\"4%\">&nbsp;</td>";
            strMessage += "</tr>";
            strMessage += "<tr>";
            strMessage += "<td width=\"3%\">&nbsp;</td>";
            strMessage += "<td width=\"93%\" colspan=\"3\"><font style=\"font-family:Trebuchet ms;font-size:10pt;letter-spacing:1px;font-weight:normal;color:#333;line-height:20px;\">Your Registration and Online Payment has been successful for the <b>69th Annual Conference of Cardiological Society of India to be held from 7<sup>th</sup> to 10<sup>th</sup> December, 2017</b></font></td>";
            strMessage += "<td width=\"4%\">&nbsp;</td>";
            strMessage += "</tr>";
            strMessage += "<tr>";
            strMessage += "<td width=\"3%\">&nbsp;</td>";
            strMessage += "<td width=\"93%\" colspan=\"3\">&nbsp;</td>";
            strMessage += "<td width=\"4%\">&nbsp;</td>";
            strMessage += "</tr>";
            strMessage += "<tr height=\"35\">";
            strMessage += "<td width=\"3%\">&nbsp;</td>";
            strMessage += "<td width=\"31%\"><font style=\"font-family:Trebuchet ms;font-size:13pt;letter-spacing:1px;font-weight:normal;color:#333;line-height:22px;font-weight:bold;\">";
            strMessage += "Name</font></td>";
            strMessage += "<td width=\"31%\"><font style=\"font-family:Trebuchet ms;font-size:13pt;letter-spacing:1px;font-weight:normal;color:#E4252C;line-height:22px;font-weight:bold;\">Dr. " + registration.Prefix + " " + registration.Name + "</font></td>";
            strMessage += "<td width=\"31%\">&nbsp;</td>";
            strMessage += "<td width=\"4%\">&nbsp;</td>";
            strMessage += "</tr>";
            strMessage += "<tr height=\"35\">";
            strMessage += "<td width=\"3%\">&nbsp;</td>";
            strMessage += "<td width=\"31%\">";
            strMessage += "<span style=\"font-family: Trebuchet ms; font-weight: 700; font-size: 13pt; color: #333333; letter-spacing: 1px\">";
            strMessage += "Amount</span></td>";
            strMessage += "<td width=\"31%\">";
            strMessage += "<span style=\"font-family: Trebuchet ms; font-weight: 700; font-size: 13pt; color: #E4252C; letter-spacing: 1px\">";
            strMessage += "Rs. " + registration.Payment_Amount.ToString() + "/-</span></td>";
            strMessage += "<td width=\"31%\">&nbsp;</td>";
            strMessage += "<td width=\"4%\">&nbsp;</td>";
            strMessage += "</tr>";
            strMessage += "<tr height=\"35\">";
            strMessage += "<td width=\"3%\">&nbsp;</td>";
            strMessage += "<td width=\"31%\">";
            strMessage += "<span style=\"font-family: Trebuchet ms; font-weight: 700; font-size: 13pt; color: #333333; letter-spacing: 1px\">";
            strMessage += "Accompanying Person</span></td>";
            strMessage += "<td width=\"31%\">";
            strMessage += "<span style=\"font-family: Trebuchet ms; font-weight: 700; font-size: 13pt; color: #E4252C; letter-spacing: 1px\">";
            strMessage += accompany + "</span></td>";
            strMessage += "<td width=\"31%\">&nbsp;</td>";
            strMessage += "<td width=\"4%\">&nbsp;</td>";
            strMessage += "</tr>";
            strMessage += "<tr height=\"35\">";
            strMessage += "<td width=\"3%\">&nbsp;</td>";
            strMessage += "<td width=\"31%\">";
            strMessage += "<span style=\"font-family: Trebuchet ms; font-weight: 700; font-size: 13pt; color: #333333; letter-spacing: 1px\">";
            strMessage += "Date of Registration</span></td>";
            strMessage += "<td width=\"31%\">";
            strMessage += "<span style=\"font-family: Trebuchet ms; font-weight: 700; font-size: 13pt; color: #E4252C; letter-spacing: 1px\">";
            strMessage += registration.CreatedDate.ToString("dd MMMM, yyyy") + "</span></td>";
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
            strMessage += "<span style=\"letter-spacing: 1px;font-family: Trebuchet ms;font-style:italic;color:#333333;font-size:12pt;\">Kindly keep this ";
            strMessage += "mail as a valid Online Payment acknowledgement and carry ";
            strMessage += "during the <b>Conference</b></span></td>";
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
            strMessage += "Indian Heart House, P-60, C.I.T Road,<br>";
            strMessage += "Scheme - VII-M, Kankurgachi,<br>";
            strMessage += "Kolkata - 700054, India<br>";
            strMessage += "Telephone: +91 33 2355 7837, 2355 6308, 2355 1500<br>";
            strMessage += "Email: </font></b><font style=\"letter-spacing: 1px;font-family: Trebuchet ms;font-style:italic;color:#333333;font-size:11pt;line-height:25px\"> ";
            strMessage += "<a href=\"mailto:secretary@csicon2017.org\">";
            strMessage += "<font color=\"#E4252C\"><span style=\"text-decoration: none\">secretary@csicon2017.org</span></font></a></font></td>";
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
            SmtpClient client = new SmtpClient("127.0.0.1");
            client.EnableSsl = false;
            client.Port = 8025;
           // client.Send(mail);
        }

        public ActionResult Test()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult Test(long id = 0)
        {
            MailAddress mailFrom = new MailAddress("secretary@csicon2017.org", "69th Annual Conference of CSI");
            MailAddress mailTo = new MailAddress("rana.chakraborty@microbaseinfotech.com", "Rana Chakraborty");
            MailMessage mail = new MailMessage(mailFrom, mailTo);
            mail.IsBodyHtml = true;
            string strMessage = "Thank you for your registration";
            mail.Subject = strMessage;


            strMessage += "<div align=\"center\">";
            strMessage += "<table border=\"1\" width=\"798\" cellpadding=\"0\" style=\"border-collapse: collapse\" bordercolor=\"#F0F0F0\">";
            strMessage += "<tr>";
            strMessage += "<td>";
            strMessage += "<table border=\"0\" width=\"100%\" cellspacing=\"0\" cellpadding=\"0\">";
            strMessage += "<tr>";
            strMessage += "<td colspan=\"5\">";
            strMessage += "<img border=\"0\" src=\"http://csicon2017.org/images/logo0000.png\" width=\"302\"></td>";
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
            strMessage += "<font style=\"font-family:Trebuchet ms;font-size:14pt;font-weight:bold;color:#E4252C;\">Dear Dr. Rana Chakraborty</font></b></td>";
            strMessage += "<td width=\"4%\">&nbsp;</td>";
            strMessage += "</tr>";
            strMessage += "<tr>";
            strMessage += "<td width=\"3%\">&nbsp;</td>";
            strMessage += "<td width=\"93%\" colspan=\"3\">&nbsp;</td>";
            strMessage += "<td width=\"4%\">&nbsp;</td>";
            strMessage += "</tr>";
            strMessage += "<tr>";
            strMessage += "<td width=\"3%\">&nbsp;</td>";
            strMessage += "<td width=\"93%\" colspan=\"3\"><font style=\"font-family:Trebuchet ms;font-size:10pt;letter-spacing:1px;font-weight:normal;color:#333;line-height:20px;\">Your Registration and Online Payment has been successful for the <b>69th Annual Conference of Cardiological Society of India to be held from 7<sup>th</sup> to 10<sup>th</sup> December, 2017</b></font></td>";
            strMessage += "<td width=\"4%\">&nbsp;</td>";
            strMessage += "</tr>";
            strMessage += "<tr>";
            strMessage += "<td width=\"3%\">&nbsp;</td>";
            strMessage += "<td width=\"93%\" colspan=\"3\">&nbsp;</td>";
            strMessage += "<td width=\"4%\">&nbsp;</td>";
            strMessage += "</tr>";
            strMessage += "<tr height=\"35\">";
            strMessage += "<td width=\"3%\">&nbsp;</td>";
            strMessage += "<td width=\"31%\"><font style=\"font-family:Trebuchet ms;font-size:13pt;letter-spacing:1px;font-weight:normal;color:#333;line-height:22px;font-weight:bold;\">CSI Membership No.</font></td>";
            strMessage += "<td width=\"31%\"><font style=\"font-family:Trebuchet ms;font-size:13pt;letter-spacing:1px;font-weight:normal;color:#E4252C;line-height:22px;font-weight:bold;\">L112099</font></td>";
            strMessage += "<td width=\"31%\">&nbsp;</td>";
            strMessage += "<td width=\"4%\">&nbsp;</td>";
            strMessage += "</tr>";
            strMessage += "<tr height=\"35\">";
            strMessage += "<td width=\"3%\">&nbsp;</td>";
            strMessage += "<td width=\"31%\"><font style=\"font-family:Trebuchet ms;font-size:13pt;letter-spacing:1px;font-weight:normal;color:#333;line-height:22px;font-weight:bold;\">";
            strMessage += "Name</font></td>";
            strMessage += "<td width=\"31%\"><font style=\"font-family:Trebuchet ms;font-size:13pt;letter-spacing:1px;font-weight:normal;color:#E4252C;line-height:22px;font-weight:bold;\">Dr. Rana Chakraborty</font></td>";
            strMessage += "<td width=\"31%\">&nbsp;</td>";
            strMessage += "<td width=\"4%\">&nbsp;</td>";
            strMessage += "</tr>";
            strMessage += "<tr height=\"35\">";
            strMessage += "<td width=\"3%\">&nbsp;</td>";
            strMessage += "<td width=\"31%\">";
            strMessage += "<span style=\"font-family: Trebuchet ms; font-weight: 700; font-size: 13pt; color: #333333; letter-spacing: 1px\">";
            strMessage += "Amount</span></td>";
            strMessage += "<td width=\"31%\">";
            strMessage += "<span style=\"font-family: Trebuchet ms; font-weight: 700; font-size: 13pt; color: #E4252C; letter-spacing: 1px\">";
            strMessage += "Rs. 10,000/-</span></td>";
            strMessage += "<td width=\"31%\">&nbsp;</td>";
            strMessage += "<td width=\"4%\">&nbsp;</td>";
            strMessage += "</tr>";
            strMessage += "<tr height=\"35\">";
            strMessage += "<td width=\"3%\">&nbsp;</td>";
            strMessage += "<td width=\"31%\">";
            strMessage += "<span style=\"font-family: Trebuchet ms; font-weight: 700; font-size: 13pt; color: #333333; letter-spacing: 1px\">";
            strMessage += "Accompanying Person</span></td>";
            strMessage += "<td width=\"31%\">";
            strMessage += "<span style=\"font-family: Trebuchet ms; font-weight: 700; font-size: 13pt; color: #E4252C; letter-spacing: 1px\">";
            strMessage += "Yes</span></td>";
            strMessage += "<td width=\"31%\">&nbsp;</td>";
            strMessage += "<td width=\"4%\">&nbsp;</td>";
            strMessage += "</tr>";
            strMessage += "<tr height=\"35\">";
            strMessage += "<td width=\"3%\">&nbsp;</td>";
            strMessage += "<td width=\"31%\">";
            strMessage += "<span style=\"font-family: Trebuchet ms; font-weight: 700; font-size: 13pt; color: #333333; letter-spacing: 1px\">";
            strMessage += "Date of Registration</span></td>";
            strMessage += "<td width=\"31%\">";
            strMessage += "<span style=\"font-family: Trebuchet ms; font-weight: 700; font-size: 13pt; color: #E4252C; letter-spacing: 1px\">";
            strMessage += "23<sup>rd</sup> December 2016</span></td>";
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
            strMessage += "<span style=\"letter-spacing: 1px;font-family: Trebuchet ms;font-style:italic;color:#333333;font-size:12pt;\">Kindly keep this ";
            strMessage += "mail as a valid Online Payment acknowledgement and carry ";
            strMessage += "during the <b>Conference</b></span></td>";
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
            strMessage += "Indian Heart House, P-60, C.I.T Road,<br>";
            strMessage += "Scheme - VII-M, Kankurgachi,<br>";
            strMessage += "Kolkata - 700054, India<br>";
            strMessage += "Telephone: +91 33 2355 7837, 2355 6308, 2355 1500<br>";
            strMessage += "Email: </font></b><font style=\"letter-spacing: 1px;font-family: Trebuchet ms;font-style:italic;color:#333333;font-size:11pt;line-height:25px\"> ";
            strMessage += "<a href=\"mailto:secretary@csicon2017.org\">";
            strMessage += "<font color=\"#E4252C\"><span style=\"text-decoration: none\">secretary@csicon2017.org</span></font></a></font></td>";
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
            SmtpClient client = new SmtpClient("127.0.0.1");
            client.EnableSsl = false;
            client.Port = 8025;
           // client.Send(mail);

            return View();
        }
    }
}
