using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _69CSI.Models;
using System.IO;
using System.Text;

namespace csi.Controllers
{
    public class RegistrationController : Controller
    {
        private csidbEntities db = new csidbEntities();

        //
       
        // GET: /Registration/

        public ActionResult Index()
        {
            return View(db.tbl_CSI_69_Con_RegistrationDetails.ToList());
        }
        public ActionResult Csimemberlist()
        {
            return View(db.tbl_CSI_69_Con_RegistrationDetails.Where(m=>m.Category=="1").ToList());
        }
        public ActionResult NonCsimemberlist()
        {
            return View(db.tbl_CSI_69_Con_RegistrationDetails.Where(m => m.Category != "1").ToList());
        }
        public ActionResult PGStudentsmemberlist()
        {
            return View(db.tbl_CSI_69_Con_RegistrationDetails.Where(m => m.Category == "4").ToList());
        }
        public ActionResult IndustryProfessionalMemberlist()
        {
            return View(db.tbl_CSI_69_Con_RegistrationDetails.Where(m => m.Category == "5").ToList());
        }
        public ActionResult NurseorTechnician()
        {
            return View(db.tbl_CSI_69_Con_RegistrationDetails.Where(m => m.Category == "6").ToList());
        }
        public ActionResult SAARC()
        {
            return View(db.tbl_CSI_69_Con_RegistrationDetails.Where(m => m.Category == "7").ToList());
        }
        public ActionResult NonSAARC()
        {
            return View(db.tbl_CSI_69_Con_RegistrationDetails.Where(m => m.Category == "8").ToList());
        }

        //
        // GET: /Registration/Details/5

        public ActionResult Details(long id = 0)
        {
            tbl_CSI_69_Con_RegistrationDetails tbl_csi_69_con_registrationdetails = db.tbl_CSI_69_Con_RegistrationDetails.Find(id);
            if (tbl_csi_69_con_registrationdetails == null)
            {
                return HttpNotFound();
            }
            return View(tbl_csi_69_con_registrationdetails);
        }

        //
        // GET: /Registration/Create

        public ActionResult Create()
        {
            ViewBag.cntlist = new SelectList(db.tblCountryCodes, "Country", "Country", "India");

          ViewBag.State = new SelectList(new[]{new { value="Andaman and Nicobar Islands", Text = "Andaman and Nicobar Islands" }, new { value="Andhra Pradesh", Text = "Andhra Pradesh" },
                          new { value="Arunachal Pradesh", Text = "Arunachal Pradesh" }, new { value="Assam", Text = "Assam" }, new { value="Bihar", Text = "Bihar" }, new { value="Chandigarh", Text = "Chandigarh" },
                          new { value="Chhattisgarh", Text = "Chhattisgarh" }, new { value="Dadra and Nagar Haveli", Text = "Dadra and Nagar Haveli" }, new { value="Daman and Diu", Text = "Daman and Diu" },
                          new { value="Delhi", Text = "Delhi" }, new { value="Goa", Text = "Goa" }, new { value="Gujarat", Text = "Gujarat" }, new { value="Haryana", Text = "Haryana" },
                          new { value="Himachal Pradesh", Text = "Himachal Pradesh" }, new { value="Jammu and Kashmir", Text = "Jammu and Kashmir" }, new { value="Jharkhand", Text = "Jharkhand" },
                          new { value="Karnataka", Text = "Karnataka" }, new { value="Kerala", Text = "Kerala" }, new { value="Lakshadweep", Text = "Lakshadweep" }, new { value="Madhya Pradesh", Text = "Madhya Pradesh" },
                          new { value="Maharashtra", Text = "Maharashtra" }, new { value="Manipur", Text = "Manipur" }, new { value="Meghalaya", Text = "Meghalaya" }, new { value="Mizoram", Text = "Mizoram" },
                          new { value="Nagaland", Text = "Nagaland" }, new { value="Odisha", Text = "Odisha" }, new { value="Puducherry", Text = "Puducherry" },new { value="Punjab", Text = "Punjab" },
                          new { value="Rajasthan", Text = "Rajasthan" }, new { value="Sikkim", Text = "Sikkim" }, new { value="Tamil Nadu", Text = "Tamil Nadu" }, new { value="Telangana", Text = "Telangana" },
                          new { value="Tripura", Text = "Tripura" }, new { value="Uttar Pradesh", Text = "Uttar Pradesh" },new { value="Uttaranchal", Text = "Uttaranchal" }, new { value="West Bengal", Text = "West Bengal" },  }, "value", "Text");
           // var cntlist = new SelectList(db.tblCountryCodes, "Country", "Code");
             var StorageDevice = new SelectList(new[] 
                                { new { value = "1", Text = "CSI Member" },
                                    new { value = "2", Text = "Non-CSI Member" },
                                    //new { value = "3", Text = "Non-CSI Member" },
                                    new { value = "4", Text = "PG Students" },
                                    new { value = "5", Text = "Industry Professional" },
                                    new { value = "6", Text = "Nurse/Technician" },
                                    new { value = "7", Text = "SAARC Countries (Non-CSI Member)" },
                                    new { value = "8", Text = "Non-SAARC Foreign Nationals" },  }, "value", "Text");
            ViewBag.catgrylist = StorageDevice;
            var pfx = new SelectList(new[] { new { value = "Dr.", Text = "Dr." }, new { value = "Mr.", Text = "Mr." }, new { value = "Mrs.", Text = "Mrs." }, new { value = "Ms.", Text = "Ms." }, }, "value", "Text");
            ViewBag.pfx = pfx;
            //decimal apiRate = GlobalData.ExchangeRateFromAPI(1, "USD", "INR");
            ViewBag.exrate = GlobalData.ExchangeRateFromAPI(1, "USD", "INR");

            return View();
        }

        //
        // POST: /Registration/Create

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(tbl_CSI_69_Con_RegistrationDetails tbl_csi_69_con_registrationdetails)
        {
            tbl_CSI_69_Con_Accompanys tbl_csi_69_con_accompanys = new tbl_CSI_69_Con_Accompanys();
            string Hdnid = Request.Form["hdnid"].ToString();
           // for Accompany_Pertion set value
            int count = Convert.ToInt32(Hdnid);


            string Hdnmemberid = Request.Form["hdntypemember"].ToString();
            string regno = "";            
            decimal payableAmt = CalculatePayableAmount(tbl_csi_69_con_registrationdetails.Category, Convert.ToInt32(Hdnid));
           

            if (tbl_csi_69_con_registrationdetails.Badge_Name == null)
            {
                tbl_csi_69_con_registrationdetails.Badge_Name = tbl_csi_69_con_registrationdetails.Prefix + " " + tbl_csi_69_con_registrationdetails.Name;               
            }
            if (tbl_csi_69_con_registrationdetails.Category == "1")
            {
                if (Hdnmemberid == "EC_Member")
                regno = "EC/CSICON17/10" ;
                if (Hdnmemberid == "Ex_President")
                regno = "EP/CSICON17/10" ;
                if (Hdnmemberid == "Ex_President")
                regno = "ES/CSICON17/10";
                if (Hdnmemberid == "CSI_Member")
                regno = "CSIME/CSICON17/10" ;
            }
            if (tbl_csi_69_con_registrationdetails.Category == "2")
            { regno = "NCSI/CSICON17/1000" ; }
            if (tbl_csi_69_con_registrationdetails.Category == "4")
            { regno = "PG/CSICON17/1000" ; }
            if (tbl_csi_69_con_registrationdetails.Category == "5")
            { regno = "IP/CSICON17/1000"; }
            if (tbl_csi_69_con_registrationdetails.Category == "6")
            { regno = "NT/CSICON17/1000" ; }
            if (tbl_csi_69_con_registrationdetails.Category == "7")
            { regno = "SAARC/CSICON17/1000"; }
            if (tbl_csi_69_con_registrationdetails.Category == "8")
            { regno = "IF/CSICON17/1000"; }

            tbl_csi_69_con_registrationdetails.Regno = regno;
            tbl_csi_69_con_registrationdetails.CreatedDate = DateTime.Now;
            tbl_csi_69_con_registrationdetails.ModifiedDate = DateTime.Now;
            tbl_csi_69_con_registrationdetails.ModifiedBy = 1;
            tbl_csi_69_con_registrationdetails.Payment_Id = 1;
            tbl_csi_69_con_registrationdetails.Payment_Amount = payableAmt;
            tbl_csi_69_con_registrationdetails.Sponsorship = " NA";
            tbl_csi_69_con_registrationdetails.Status = 1;
            if (ModelState.IsValid)
            {
                long lastProductId = db.tbl_CSI_69_Con_RegistrationDetails.Max(item => item.RegId);
                lastProductId = lastProductId + 1;
                tbl_csi_69_con_registrationdetails.Regno = regno + lastProductId.ToString();
                db.tbl_CSI_69_Con_RegistrationDetails.Add(tbl_csi_69_con_registrationdetails);
                db.SaveChanges();
                 lastProductId = db.tbl_CSI_69_Con_RegistrationDetails.Max(item => item.RegId);
               
                if (count > 0)
                {
                    string anam = "Accompany_Pertion_Name";
                    string age = "Accompany_Pertion_Age";
                    string sex = "Accompany_Pertion_sex";
                    string meal = "Accompany_Pertion_meal";
                    string AccommodationPtn = "Accompany_Pertion_Accommodation";
                    string Accompany_frmday = "Accompany_frmday";
                    string Accompany_frmmonth = "Accompany_frmmonth";
                    string Accompany_today = "Accompany_today";
                    string Accompany_tomonth = "Accompany_tomonth";
                    string Accompany_Check_inh = "Accompany_Check_inh";
                    string Accompany_Check_inm = "Accompany_Check_inm";
                    string Accompany_Check_inMeridian = "Accompany_Check_inMeridian";
                    string Accompany_Check_outh = "Accompany_Check_outh";
                    string Accompany_Check_outm = "Accompany_Check_outm";
                    string Accompany_Check_outMeridia = "Accompany_Check_outMeridian";

                    for (int i = 1; i <= count; i++)
                    {
                        anam = "Accompany_Pertion_Name" + i.ToString();
                        age = "Accompany_Pertion_Age" + i.ToString();
                        sex = "Accompany_Pertion_sex"+ i.ToString();
                        meal = "Accompany_Pertion_meal" + i.ToString();
                        AccommodationPtn = "Accompany_Pertion_Accommodation" + i.ToString();
                        Accompany_frmday = "Accompany_frmday" + i.ToString();
                        Accompany_frmmonth = "Accompany_frmmonth" + i.ToString();
                        Accompany_today = "Accompany_today" + i.ToString();
                        Accompany_tomonth = "Accompany_tomonth" + i.ToString();
                        Accompany_Check_inh = "Accompany_Check_inh" + i.ToString();
                        Accompany_Check_inm = "Accompany_Check_inm" + i.ToString();
                        Accompany_Check_inMeridian = "Accompany_Check_inMeridian" + i.ToString();
                        Accompany_Check_outh = "Accompany_Check_outh" + i.ToString();
                        Accompany_Check_outm = "Accompany_Check_outm" + i.ToString();
                        Accompany_Check_outMeridia = "Accompany_Check_outMeridian" + i.ToString();

                        tbl_csi_69_con_accompanys.RegId = lastProductId;
                        tbl_csi_69_con_accompanys.Name = Request.Form[anam];
                        tbl_csi_69_con_accompanys.Age = Convert.ToInt32(Request.Form[age]);
                        tbl_csi_69_con_accompanys.Sex =  Request.Form[sex].ToString();
                        tbl_csi_69_con_accompanys.Meal_Preference = Request.Form[meal].ToString();
                        tbl_csi_69_con_accompanys.Accommodation = Request.Form[AccommodationPtn].ToString();
                        tbl_csi_69_con_accompanys.FromDate = Request.Form[Accompany_frmday].ToString() + "/" + Request.Form[Accompany_frmmonth].ToString() + "/2017" ;
                        tbl_csi_69_con_accompanys.ToDate = Request.Form[Accompany_today].ToString() + "/" + Request.Form[Accompany_tomonth].ToString() + "/2017";
                        tbl_csi_69_con_accompanys.Check_in_Time = Request.Form[Accompany_Check_inh].ToString() + ":" + Request.Form[Accompany_Check_inm].ToString() +" "+ Request.Form[Accompany_Check_inMeridian].ToString();
                        tbl_csi_69_con_accompanys.Check_out_Time = Request.Form[Accompany_Check_outh].ToString() + ":" + Request.Form[Accompany_Check_outm].ToString() + " " + Request.Form[Accompany_Check_outMeridia].ToString();
                        db.tbl_CSI_69_Con_Accompanys.Add(tbl_csi_69_con_accompanys);
                        db.SaveChanges();

                    }
                }
                tbl_CSI_69_Con_Transections Trans = new tbl_CSI_69_Con_Transections();
                // Trans.UserId = tbl_csi_69_con_registrationdetails.RegId;
                Trans.UserId = lastProductId;
                Trans.AmountToPay = payableAmt;
                Trans.AmountPaid = 0;
                Trans.TransDate = DateTime.Now.Date;
                Trans.Status = "N";
                Trans.Remarks = "";
                db.tbl_CSI_69_Con_Transections.Add(Trans);
                db.SaveChanges();
                long id1 = db.tbl_CSI_69_Con_Transections.Max(item => item.TransId);
                GlobalData ga = new GlobalData();
                ga.AdminMail("satya@microbaseinfotech.com");
                ga.RegisterSendMail("satya@microbaseinfotech.com", "Satya Bhattacharyya", lastProductId, id1);
               return RedirectToAction("Billdesk", "Registration", new {id =id1  });
                //return RedirectToAction("Index");
            }

            ViewBag.cntlist = new SelectList(db.tblCountryCodes, "Code", "Country");
            var StorageDevice = new SelectList(new[] 
                                { new { value = "1", Text = "CSI Member" },
                                    new { value = "2", Text = "Non-CSI Member" },
                                    //new { value = "3", Text = "Non-CSI Member" },
                                    new { value = "4", Text = "PG Students" },
                                    new { value = "5", Text = "Industry Professional" },
                                    new { value = "6", Text = "Nurse/Technician" },
                                    new { value = "7", Text = "SAARC Countries (Non-CSI Member)" },
                                    new { value = "8", Text = "Non-SAARC Foreign Nationals" },  }, "value", "Text");
            ViewBag.catgrylist = StorageDevice;
            var pfx = new SelectList(new[] { new { value = "Dr.", Text = "Dr." }, new { value = "Mr.", Text = "Mr." }, new { value = "Mrs.", Text = "Mrs." }, new { value = "Ms.", Text = "Ms." }, }, "value", "Text");
            ViewBag.Prefixlst = pfx;
           
        return View(tbl_csi_69_con_registrationdetails);
        }

        public ActionResult Billdesk(long id)
        {
            tbl_CSI_69_Con_Transections Trans = db.tbl_CSI_69_Con_Transections.Find(id);

            string CheckSumKey = "CMcyWPzYC8DH";
            string MerchantID = "CARDIOSOCT";
            string CustomerID = Trans.UserId.ToString();
            string TransactionAmt = "2.00";
            // string TransactionAmt = Trans.AmountToPay.ToString();
            string CurrencyType = "INR";
            string TypeField1 = "R";
            string SecurityID = "cardiosoct";
            string TypeField2 = "F";
            string AdditionalInfo1 = Trans.TransId.ToString();
            string AdditionalInfo2 = "NA";
            string AdditionalInfo3 = "NA";
            string AdditionalInfo4 = "NA";
            string AdditionalInfo5 = "NA";
            string AdditionalInfo6 = "NA";
            string AdditionalInfo7 = "NA";
            string ResponseURL = "http://69thac.csi.org.in/home/thankyou";

            GlobalData myUtility = new GlobalData();
           // string msg = "CARDIOSOCT" + "|" + "14" + "|" + "NA" + "|" + "2.00" + "|" + "NA|NA|NA|" + "INR" + "|" + "NA" + "|" + "R" + "|" + "cardiosoct" + "|" + "NA|NA|" + "F" + "|" + "NA" + "|" + "NA" + "|" + "NA" + "|" + "NA" + "|" + "NA" + "|" + "NA" + "|" + "NA" + "|" + "http://69thac.csi.org.in/home/thankyou";
            //string msg = MerchantID + "|" + CustomerID + "|" + "NA" + "|" + TransactionAmt + "|" + "NA|NA|NA|" + CurrencyType + "|" + "NA" + "|" + TypeField1 + "|" + SecurityID + "|" + "NA|NA|" + TypeField2 + "|" + AdditionalInfo1 + "|" + AdditionalInfo2 + "|" + AdditionalInfo3 + "|" + AdditionalInfo4 + "|" + AdditionalInfo5 + "|" + AdditionalInfo6 + "|" + AdditionalInfo7 + "|" + ResponseURL;
            //string mg1 = "CARDIOSOCT" + "|" + "14" + "|" + "NA" + "|" + "2.00" + "|" + "NA|NA|NA|" + "INR" + "|" + "NA" + "|" + "R" + "|" + "cardiosoct" + "|" + "NA|NA|" + "F" + "|" + "NA" + "|" + "NA" + "|" + "20" + "|" + "NA" + "|" + "NA" + "|" + "NA" + "|" + "NA" + "|" + "http://69thac.csi.org.in/home/thankyou";
            string msg = "CARDIOSOCT" + "|" + CustomerID + "|" + "NA" + "|" + TransactionAmt + "|" + "NA|NA|NA|" + "INR" + "|" + "NA" + "|" + "R" + "|" + "cardiosoct" + "|" + "NA|NA|" + "F" + "|" + AdditionalInfo1 + "|" + "NA" + "|" + "NA" + "|" + "NA" + "|" + "NA" + "|" + "NA" + "|" + "NA" + "|" + "http://69thac.csi.org.in/home/thankyou";
            string hashData = msg;

            string checksumvalue = myUtility.GetHMACSHA256(hashData, "CMcyWPzYC8DH");

            checksumvalue = checksumvalue.ToUpper();

            checksumvalue = hashData + "|" + checksumvalue;

            Response.Redirect("https://pgi.billdesk.com/pgidsk/PGIMerchantPayment?msg=" + checksumvalue);


            Console.Out.WriteLine("HMAC {0}", hashData);

            Console.ReadKey();


            return View();
        }

        private decimal CalculatePayableAmount(string category, int count)
        {
            decimal payableAmt = 0;
            DateTime dtfrom = new DateTime();
            DateTime dtto = new DateTime();
            if (category == "1")
            {
                // CSI MEMBER
                //dtfrom = Convert.ToDateTime("2016-12-12");
                //dtto = Convert.ToDateTime("2017-01-31");
                DateTime d1 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                dtfrom = DateTime.Parse("2016-12-12");
                dtto = DateTime.Parse("2017-01-31");
              
                 DateTime StartDate = new DateTime(2016, 12, 12);
                 DateTime EndDate = new DateTime(2017, 01, 31);
                 int DayInterval = 1;

        List<DateTime> dateList = new List<DateTime>();
        while (StartDate.AddDays(DayInterval) <= EndDate)
        {
            StartDate = StartDate.AddDays(DayInterval);
            dateList.Add(StartDate);
            if (StartDate == d1)
            {
                payableAmt = 8000 + (14000 * count);
            }
        }        
 
              
                //if (DateTime.Now >= dtfrom && DateTime.Now <= dtto)
                //    payableAmt = 8000 + (14000 * count);

                //dtfrom = Convert.ToDateTime("2017-02-01");
                //dtto = Convert.ToDateTime("2017-04-30");
                dtfrom = DateTime.Parse("2017-02-01");
                dtto = DateTime.Parse("2017-04-30");
                if (DateTime.Now >= dtfrom && DateTime.Now <= dtto)
                    payableAmt = 13000 + (15500 * count);

                dtfrom = Convert.ToDateTime("2017-05-01");
                dtto = Convert.ToDateTime("2017-07-31");
                if (DateTime.Now >= dtfrom && DateTime.Now < dtto)
                    payableAmt = 14000 + (16500 * count);

                dtfrom = Convert.ToDateTime("2017-08-01");
                dtto = Convert.ToDateTime("2017-11-15");
                if (DateTime.Now >= dtfrom && DateTime.Now < dtto)
                    payableAmt = 18000 + (23000 * count);
            }

            if (category == "2")
            {
                // NON CSI MEMBER
                dtfrom = Convert.ToDateTime("2016-12-12");
                dtto = Convert.ToDateTime("2017-01-31");
                if (DateTime.Now >= dtfrom && DateTime.Now < dtto)
                    payableAmt = 14000 + (14000 * count);

                dtfrom = Convert.ToDateTime("2017-02-01");
                dtto = Convert.ToDateTime("2017-04-30");
                if (DateTime.Now >= dtfrom && DateTime.Now < dtto)
                    payableAmt = 15500 + (15500 * count);

                dtfrom = Convert.ToDateTime("2017-05-01");
                dtto = Convert.ToDateTime("2017-07-31");
                if (DateTime.Now >= dtfrom && DateTime.Now < dtto)
                    payableAmt = 16500 + (16500 * count);

                dtfrom = Convert.ToDateTime("2017-08-01");
                dtto = Convert.ToDateTime("2017-11-15");
                if (DateTime.Now >= dtfrom && DateTime.Now < dtto)
                    payableAmt = 23000 + (23000 * count);
            }

            if (category == "4")
            {
                // PG Students
                dtfrom = Convert.ToDateTime("2016-12-12");
                dtto = Convert.ToDateTime("2017-01-31");
                if (DateTime.Now >= dtfrom && DateTime.Now < dtto)
                    payableAmt = 4500 + (14000 * count);

                dtfrom = Convert.ToDateTime("2017-02-01");
                dtto = Convert.ToDateTime("2017-04-30");
                if (DateTime.Now >= dtfrom && DateTime.Now < dtto)
                    payableAmt = 5500 + (15500 * count);

                dtfrom = Convert.ToDateTime("2017-05-01");
                dtto = Convert.ToDateTime("2017-07-31");
                if (DateTime.Now >= dtfrom && DateTime.Now < dtto)
                    payableAmt = 7000 + (16500 * count);

                dtfrom = Convert.ToDateTime("2017-08-01");
                dtto = Convert.ToDateTime("2017-11-15");
                if (DateTime.Now >= dtfrom && DateTime.Now < dtto)
                    payableAmt = 9500 + (23000 * count);
            }

            if (category == "5")
            {
                // Industry Professional
                dtfrom = Convert.ToDateTime("2016-12-12");
                dtto = Convert.ToDateTime("2017-01-31");
                if (DateTime.Now >= dtfrom && DateTime.Now < dtto)
                    payableAmt = 14000 + (14000 * count);

                dtfrom = Convert.ToDateTime("2017-02-01");
                dtto = Convert.ToDateTime("2017-04-30");
                if (DateTime.Now >= dtfrom && DateTime.Now < dtto)
                    payableAmt = 16500 + (15500 * count);

                dtfrom = Convert.ToDateTime("2017-05-01");
                dtto = Convert.ToDateTime("2017-07-31");
                if (DateTime.Now >= dtfrom && DateTime.Now < dtto)
                    payableAmt = 18000 + (16500 * count);

                dtfrom = Convert.ToDateTime("2017-08-01");
                dtto = Convert.ToDateTime("2017-11-15");
                if (DateTime.Now >= dtfrom && DateTime.Now < dtto)
                    payableAmt = 26000 + (23000 * count);
            }

            if (category == "6")
            {
                // Nurse/Technician
                dtfrom = Convert.ToDateTime("2016-12-12");
                dtto = Convert.ToDateTime("2017-01-31");
                if (DateTime.Now >= dtfrom && DateTime.Now < dtto)
                    payableAmt = 4500 + (14000 * count);

                dtfrom = Convert.ToDateTime("2017-02-01");
                dtto = Convert.ToDateTime("2017-04-30");
                if (DateTime.Now >= dtfrom && DateTime.Now < dtto)
                    payableAmt = 6500 + (15500 * count);

                dtfrom = Convert.ToDateTime("2017-05-01");
                dtto = Convert.ToDateTime("2017-07-31");
                if (DateTime.Now >= dtfrom && DateTime.Now < dtto)
                    payableAmt = 7500 + (16500 * count);

                dtfrom = Convert.ToDateTime("2017-08-01");
                dtto = Convert.ToDateTime("2017-11-15");
                if (DateTime.Now >= dtfrom && DateTime.Now < dtto)
                    payableAmt = 9500 + (23000 * count);
            }

            if (category == "7")
            {
                // SAARC Countries (Non-CSI Member)
                dtfrom = Convert.ToDateTime("2016-12-12");
                dtto = Convert.ToDateTime("2017-01-31");
                if (DateTime.Now >= dtfrom && DateTime.Now < dtto)
                    payableAmt = 14000 + (14000 * count);

                dtfrom = Convert.ToDateTime("2017-02-01");
                dtto = Convert.ToDateTime("2017-04-30");
                if (DateTime.Now >= dtfrom && DateTime.Now < dtto)
                    payableAmt = 15500 + (15500 * count);

                dtfrom = Convert.ToDateTime("2017-05-01");
                dtto = Convert.ToDateTime("2017-07-31");
                if (DateTime.Now >= dtfrom && DateTime.Now < dtto)
                    payableAmt = 16500 + (16500 * count);

                dtfrom = Convert.ToDateTime("2017-08-01");
                dtto = Convert.ToDateTime("2017-11-15");
                if (DateTime.Now >= dtfrom && DateTime.Now < dtto)
                    payableAmt = 23000 + (23000 * count);
            }

            if (category == "8")
            {
                decimal apiRate = GlobalData.ExchangeRateFromAPI(1, "USD", "INR");
                // Non-SAARC Foreign Nationals
                dtfrom = Convert.ToDateTime("2016-12-12");
                dtto = Convert.ToDateTime("2017-01-31");
                if (DateTime.Now >= dtfrom && DateTime.Now < dtto)
                    payableAmt = (750 * apiRate * (count + 1));

                dtfrom = Convert.ToDateTime("2017-02-01");
                dtto = Convert.ToDateTime("2017-04-30");
                if (DateTime.Now >= dtfrom && DateTime.Now < dtto)
                    payableAmt = (800 * apiRate * (count + 1));

                dtfrom = Convert.ToDateTime("2017-05-01");
                dtto = Convert.ToDateTime("2017-07-31");
                if (DateTime.Now >= dtfrom && DateTime.Now < dtto)
                    payableAmt = (1000 * apiRate * (count + 1));

                dtfrom = Convert.ToDateTime("2017-08-01");
                dtto = Convert.ToDateTime("2017-11-15");
                if (DateTime.Now >= dtfrom && DateTime.Now < dtto)
                    payableAmt = (1100 * apiRate *( count+1));
            }
            return payableAmt;
        }

     


        public ActionResult FatchDoctorsDetails(string lno)
        {
            string Message = string.Empty;
            var DoctorsDetailsL = from p in db.life_member where p.memberno == lno select p;
            var DoctorsDetailsLA = from p in db.life_associate_member where p.memberno == lno select p;
            var DoctorsDetailsA = from p in db.associate_member where p.memberno == lno select p;
            var DoctorsDetailsO = from p in db.other_member where p.memberno == lno select p;

            //tblStudentOrder StudentOrder = db.tblStudentOrders.Find(OrderId);
            if (DoctorsDetailsL.Count() > 0)
            {
                return Json(DoctorsDetailsL);
            }
            else if (DoctorsDetailsLA.Count() > 0)
            {
                return Json(DoctorsDetailsLA);
            }
            else if (DoctorsDetailsA.Count() > 0)
            {
                return Json(DoctorsDetailsA);
            }
            else if (DoctorsDetailsO.Count() > 0)
            {
                return Json(DoctorsDetailsO);
            }
            else
            {
                DoctorsDetailsL = null;
                return Json(DoctorsDetailsL);
            }


        }


     
        // GET: /Registration/Edit/5
        public ActionResult test()
        {
            return View();
        }
     

        [HttpPost]
        public ActionResult UploadFiles()
        {
            long id = Convert.ToInt64(TempData["id"]);

            tbl_CSI_69_Con_RegistrationDetails tbl_csi_69_con_registrationdetails = db.tbl_CSI_69_Con_RegistrationDetails.Find(id);
            if (tbl_csi_69_con_registrationdetails == null)
            {
                return HttpNotFound();
            }

            // Checking no of files injected in Request object  
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                        //string filename = Path.GetFileName(Request.Files[i].FileName);  

                        HttpPostedFileBase file = files[i];
                        string fname;

                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = tbl_csi_69_con_registrationdetails.Extra3;
                        }
                        else
                        {
                            fname = tbl_csi_69_con_registrationdetails.Extra3;
                        }

                        // Get the complete folder path and store the file inside it.  
                        fname = Path.Combine(Server.MapPath("~/Uploads/"), fname);
                        file.SaveAs(fname);
                    }
                    // Returns message that successfully uploaded  
                    return Json("File Uploaded Successfully!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }

        public ActionResult Edit(long id = 0)
        {
            tbl_CSI_69_Con_RegistrationDetails tbl_csi_69_con_registrationdetails = db.tbl_CSI_69_Con_RegistrationDetails.Find(id);
            if (tbl_csi_69_con_registrationdetails == null)
            {
                return HttpNotFound();
            }

            ViewBag.cntlist = new SelectList(db.tblCountryCodes, "Country", "Country", tbl_csi_69_con_registrationdetails.Country);

            ViewBag.State = new SelectList(new[]{new { value="Andaman and Nicobar Islands", Text = "Andaman and Nicobar Islands" }, new { value="Andhra Pradesh", Text = "Andhra Pradesh" },
                          new { value="Arunachal Pradesh", Text = "Arunachal Pradesh" }, new { value="Assam", Text = "Assam" }, new { value="Bihar", Text = "Bihar" }, new { value="Chandigarh", Text = "Chandigarh" },
                          new { value="Chhattisgarh", Text = "Chhattisgarh" }, new { value="Dadra and Nagar Haveli", Text = "Dadra and Nagar Haveli" }, new { value="Daman and Diu", Text = "Daman and Diu" },
                          new { value="Delhi", Text = "Delhi" }, new { value="Goa", Text = "Goa" }, new { value="Gujarat", Text = "Gujarat" }, new { value="Haryana", Text = "Haryana" },
                          new { value="Himachal Pradesh", Text = "Himachal Pradesh" }, new { value="Jammu and Kashmir", Text = "Jammu and Kashmir" }, new { value="Jharkhand", Text = "Jharkhand" },
                          new { value="Karnataka", Text = "Karnataka" }, new { value="Kerala", Text = "Kerala" }, new { value="Lakshadweep", Text = "Lakshadweep" }, new { value="Madhya Pradesh", Text = "Madhya Pradesh" },
                          new { value="Maharashtra", Text = "Maharashtra" }, new { value="Manipur", Text = "Manipur" }, new { value="Meghalaya", Text = "Meghalaya" }, new { value="Mizoram", Text = "Mizoram" },
                          new { value="Nagaland", Text = "Nagaland" }, new { value="Odisha", Text = "Odisha" }, new { value="Puducherry", Text = "Puducherry" },new { value="Punjab", Text = "Punjab" },
                          new { value="Rajasthan", Text = "Rajasthan" }, new { value="Sikkim", Text = "Sikkim" }, new { value="Tamil Nadu", Text = "Tamil Nadu" }, new { value="Telangana", Text = "Telangana" },
                          new { value="Tripura", Text = "Tripura" }, new { value="Uttar Pradesh", Text = "Uttar Pradesh" },new { value="Uttaranchal", Text = "Uttaranchal" }, new { value="West Bengal", Text = "West Bengal" },  }, "value", "Text", tbl_csi_69_con_registrationdetails.State);
            // var cntlist = new SelectList(db.tblCountryCodes, "Country", "Code");
            var StorageDevice = new SelectList(new[] 
                                { new { value = "1", Text = "CSI Member" },
                                    new { value = "2", Text = "Non-CSI Member" },
                                    //new { value = "3", Text = "Non-CSI Member" },
                                    new { value = "4", Text = "PG Students" },
                                    new { value = "5", Text = "Industry Professional" },
                                    new { value = "6", Text = "Nurse/Technician" },
                                    new { value = "7", Text = "SAARC Countries (Non-CSI Member)" },
                                    new { value = "8", Text = "Non-SAARC Foreign Nationals" },  }, "value", "Text", tbl_csi_69_con_registrationdetails.Category);
            ViewBag.catgrylist = StorageDevice;
            var pfx = new SelectList(new[] { new { value = "Dr.", Text = "Dr." }, new { value = "Mr.", Text = "Mr." }, new { value = "Mrs.", Text = "Mrs." }, new { value = "Ms.", Text = "Ms." }, }, "value", "Text", tbl_csi_69_con_registrationdetails.Prefix);
            ViewBag.pfx = pfx;
          
            ViewBag.exrate = GlobalData.ExchangeRateFromAPI(1, "USD", "INR");


            return View(tbl_csi_69_con_registrationdetails);
        }

        //
        // POST: /Registration/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tbl_CSI_69_Con_RegistrationDetails tbl_csi_69_con_registrationdetails)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_csi_69_con_registrationdetails).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_csi_69_con_registrationdetails);
        }

        //
        // GET: /Registration/Delete/5

        public ActionResult Delete(long id = 0)
        {
            tbl_CSI_69_Con_RegistrationDetails tbl_csi_69_con_registrationdetails = db.tbl_CSI_69_Con_RegistrationDetails.Find(id);
            if (tbl_csi_69_con_registrationdetails == null)
            {
                return HttpNotFound();
            }
            return View(tbl_csi_69_con_registrationdetails);
        }

        //
        // POST: /Registration/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            tbl_CSI_69_Con_RegistrationDetails tbl_csi_69_con_registrationdetails = db.tbl_CSI_69_Con_RegistrationDetails.Find(id);
            db.tbl_CSI_69_Con_RegistrationDetails.Remove(tbl_csi_69_con_registrationdetails);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //[HttpPost]
        //public ActionResult Registar(string Prefix, string Name, string DOB, string Membership_Number, string Institution, string Department, string Designation, string Degree, string Nationality, string Address, string City, string Pin, string Category, string State, string Country, string WorkPhone, string Mobile, string Fax, string Email, string Badge, string Username, string Password, string Hdnid, string Acname, string Acage, string Acsex, string Acmeal, string Acaccom, string Acfdate, string Actdate, string Acchk)
        //{
        //    decimal payableAmt = CalculatePayableAmount(Category, Convert.ToInt32(Hdnid));

        //    tbl_CSI_69_Con_Accompanys tbl_csi_69_con_accompanys = new tbl_CSI_69_Con_Accompanys();
        //    tbl_CSI_69_Con_RegistrationDetails tbl_csi_69_con_registrationdetails = new tbl_CSI_69_Con_RegistrationDetails();
        //    tbl_csi_69_con_registrationdetails.Prefix = Prefix;
        //    tbl_csi_69_con_registrationdetails.Name = Name;
        //    tbl_csi_69_con_registrationdetails.DOB = DOB;
        //    tbl_csi_69_con_registrationdetails.Membership_Number = Membership_Number;
        //    tbl_csi_69_con_registrationdetails.Institution = Institution;
        //    tbl_csi_69_con_registrationdetails.Department = Department;
        //    tbl_csi_69_con_registrationdetails.Designation = Designation;
        //    tbl_csi_69_con_registrationdetails.Degree = Degree;
        //    tbl_csi_69_con_registrationdetails.Nationality = Nationality;
        //    tbl_csi_69_con_registrationdetails.Address = Address;
        //    tbl_csi_69_con_registrationdetails.City = City;
        //    tbl_csi_69_con_registrationdetails.Pin = Pin;
        //    tbl_csi_69_con_registrationdetails.Category = Category;
        //    //Registration no  Category wise ex:EC/CSICON17/10001,EP/CSICON17/10001,ES/CSICON17/10001
        //    tbl_csi_69_con_registrationdetails.State = State;
        //    tbl_csi_69_con_registrationdetails.Country = Country;
        //    tbl_csi_69_con_registrationdetails.WorkPhone = WorkPhone;
        //    tbl_csi_69_con_registrationdetails.Mobile = Mobile;
        //    tbl_csi_69_con_registrationdetails.Fax = Fax;
        //    tbl_csi_69_con_registrationdetails.Email = Email;
        //    tbl_csi_69_con_registrationdetails.CreatedDate = DateTime.Now;
        //    tbl_csi_69_con_registrationdetails.ModifiedDate = DateTime.Now;
        //    tbl_csi_69_con_registrationdetails.ModifiedBy = 1;
        //    tbl_csi_69_con_registrationdetails.Payment_Id = 1;
        //    tbl_csi_69_con_registrationdetails.Payment_Amount = payableAmt;
        //    tbl_csi_69_con_registrationdetails.Sponsorship = " NA";
        //    tbl_csi_69_con_registrationdetails.Status = 1;
        //    tbl_csi_69_con_registrationdetails.Username = Username;
        //    tbl_csi_69_con_registrationdetails.Password = Password;
        //    tbl_csi_69_con_registrationdetails.Badge_Name = Badge;

        //    int count = Convert.ToInt32(Hdnid);
        //    // int count1 = Convert.ToInt32(File);
        //    string filename = DateTime.Now + "_" + Name;
        //    tbl_csi_69_con_registrationdetails.Extra3 = filename;
        //    TempData["filnm"] = filename;


        //    string[] words1 = Acname.Split(',');
        //    string[] words2 = Acage.Split(',');
        //    string[] words3 = Acsex.Split(',');
        //    string[] words4 = Acmeal.Split(',');
        //    string[] words5 = Acaccom.Split(',');
        //    string[] words6 = Acfdate.Split(',');
        //    string[] words7 = Actdate.Split(',');
        //    string[] words8 = Acchk.Split(',');

        //    //string acname[]=


        //    if (ModelState.IsValid)
        //    {
        //        db.tbl_CSI_69_Con_RegistrationDetails.Add(tbl_csi_69_con_registrationdetails);
        //        db.SaveChanges();
        //        //tbl_csi_69_con_accompanys.RegId = tbl_csi_69_con_registrationdetails.RegId;
        //        long lastProductId = db.tbl_CSI_69_Con_RegistrationDetails.Max(item => item.RegId);
        //        tbl_csi_69_con_accompanys.RegId = lastProductId;
        //       TempData["id"] = tbl_csi_69_con_registrationdetails.RegId;
        //       Session["regid"] = lastProductId;
        //        for (int i = 1; i <= count; i++)
        //        {
        //            tbl_csi_69_con_accompanys.Name = words1[i];
        //            tbl_csi_69_con_accompanys.Age = Convert.ToInt32(words2[i]);
        //            tbl_csi_69_con_accompanys.Sex = words3[i];
        //            tbl_csi_69_con_accompanys.Meal_Preference = words4[i];
        //            tbl_csi_69_con_accompanys.Accommodation = words5[i];
        //            tbl_csi_69_con_accompanys.FromDate = words6[i];
        //            tbl_csi_69_con_accompanys.ToDate = words7[i];
        //            tbl_csi_69_con_accompanys.Check_in_Time = "12:00:00 AM";
        //            tbl_csi_69_con_accompanys.Check_out_Time = "12:00:00 AM";
        //            db.tbl_CSI_69_Con_Accompanys.Add(tbl_csi_69_con_accompanys);
        //            db.SaveChanges();
        //        }
        //        tbl_CSI_69_Con_Transections Trans = new tbl_CSI_69_Con_Transections();
        //        // Trans.UserId = tbl_csi_69_con_registrationdetails.RegId;
        //        Trans.UserId = lastProductId;
        //        Trans.AmountToPay = payableAmt;
        //        Trans.AmountPaid = 0;
        //        Trans.TransDate = DateTime.Now.Date;
        //        Trans.Status = "N";
        //        Trans.Remarks = "";
        //        db.tbl_CSI_69_Con_Transections.Add(Trans);
        //        db.SaveChanges();
        //        return RedirectToAction("Billdesk","Registration");

        //    }

        //   return View(0);
        //}

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}