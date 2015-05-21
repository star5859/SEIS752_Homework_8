using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SEIS752_MVC_WebApp_stark.Models;

namespace SEIS752_MVC_WebApp_stark.Controllers
{
    public class UserController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /User/
        public ActionResult Index()
        {
            return View(db.FFUsers.ToList());
        }

        // GET: /User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FFUser ffuser = db.FFUsers.Find(id);
            if (ffuser == null)
            {
                return HttpNotFound();
            }
            return View(ffuser);
        }

        // GET: /User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,Name,Username,lat,lon,ImageUrl")] FFUser ffuser)
        {
            if (ModelState.IsValid)
            {
                db.FFUsers.Add(ffuser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ffuser);
        }

        // GET: /User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FFUser ffuser = db.FFUsers.Find(id);
            if (ffuser == null)
            {
                return HttpNotFound();
            }
            return View(ffuser);
        }

        // POST: /User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,Name,Username,lat,lon,ImageUrl")] FFUser ffuser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ffuser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ffuser);
        }

        // GET: /User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FFUser ffuser = db.FFUsers.Find(id);
            if (ffuser == null)
            {
                return HttpNotFound();
            }
            return View(ffuser);
        }

        // POST: /User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FFUser ffuser = db.FFUsers.Find(id);
            db.FFUsers.Remove(ffuser);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // John added this (a re-named copy of Detalis)
        // then later renamed it to FindNeighbors2  so that he could work
        // with a new version that queried that dababase

        // GET: /User/FindNeighbors/5
        public ActionResult FindNeighbors2(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FFUser ffuser = db.FFUsers.Find(id);
            if (ffuser == null)
            {
                return HttpNotFound();
            }
             return View(ffuser);
      }
        // GET: /User/FindNeighbors
        // reference for the query is:
        // h...p://chsakell.com/2013/08/24/retrieving-data-with-dbcontext/
        // start simple – just try to get users with lat less than what was 
        // passed in. Once that works, build a more complex query

        public ActionResult FindNeighbors(double? _lat, double? _lon)
        {
           // return View(db.FFUsers.ToList());

            var query = (from u in db.FFUsers
                         where (u.lat < _lat + 1) && (u.lat > _lat - 1) 
                            && (u.lon < _lon +1) && (u.lon > _lon -1)
                        orderby u.lat
                        select u).ToList();
      
        var q = query.ToList();

        List<FFUser> areClose = new List<FFUser>();
            areClose.Add(q[1]);

        double LAT; double LON; double miles;
        for (int i = 0; i < q.Count; i++)
            {
                LAT = q[i].lat;
                LON = q[i].lon;
                miles = distanceTo( _lat, _lon, LAT, LON);

                if( miles < 1){
                    areClose.Add(q[i]);
                 }
            }

            return View(areClose);
           
        }

        private double distanceTo(double? _lat, double? _lon, double? LAT, double? LON)
        {
            
            double rlat1 = (double) (Math.PI * _lat / 180);
            double rlat2 = (double) (Math.PI * LAT / 180);
            double rlon1 = (double) (Math.PI * _lon / 180);
            double rlon2 = (double) (Math.PI * LON / 180);
            double theta = (double) (_lon - LON);
            double rtheta = (double) (Math.PI * theta / 180);

            double dist = Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) * Math.Cos(rlat2) * Math.Cos(rtheta);
            dist = Math.Acos(dist);
            dist = dist * 180 / Math.PI;
            dist = dist * 60 * 1.1515;
            dist = dist * 0.8684;
            return dist;
        }


    }
}

