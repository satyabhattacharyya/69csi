using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _69CSI.Models;

namespace _69CSI.Controllers
{
    public class khgfvController : Controller
    {
        private csidbEntities db = new csidbEntities();

        //
        // GET: /khgfv/

        public ActionResult Index()
        {
            return View(db.tbl_CSI_69_Con_RegistrationDetails.ToList());
        }

        //
        // GET: /khgfv/Details/5

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
        // GET: /khgfv/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /khgfv/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tbl_CSI_69_Con_RegistrationDetails tbl_csi_69_con_registrationdetails)
        {
            if (ModelState.IsValid)
            {
                db.tbl_CSI_69_Con_RegistrationDetails.Add(tbl_csi_69_con_registrationdetails);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_csi_69_con_registrationdetails);
        }

        //
        // GET: /khgfv/Edit/5

        public ActionResult Edit(long id = 0)
        {
            tbl_CSI_69_Con_RegistrationDetails tbl_csi_69_con_registrationdetails = db.tbl_CSI_69_Con_RegistrationDetails.Find(id);
            if (tbl_csi_69_con_registrationdetails == null)
            {
                return HttpNotFound();
            }
            return View(tbl_csi_69_con_registrationdetails);
        }

        //
        // POST: /khgfv/Edit/5

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
        // GET: /khgfv/Delete/5

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
        // POST: /khgfv/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            tbl_CSI_69_Con_RegistrationDetails tbl_csi_69_con_registrationdetails = db.tbl_CSI_69_Con_RegistrationDetails.Find(id);
            db.tbl_CSI_69_Con_RegistrationDetails.Remove(tbl_csi_69_con_registrationdetails);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}