﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HiNSimulator2014.Models;

namespace HiNSimulator2014.Controllers.Admin
{
    [Authorize]
    public class LocationsController : Controller
    {
        private IRepository repository;

        public LocationsController()
        {
            this.repository = new Repository();
        }

        // GET: Locations
        public ActionResult Index()
        {
            var locations = repository.GetAllLocationWithImage();
            return View("~/Views/Admin/Locations/Index.cshtml", locations);
        }

        // GET: Locations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = repository.GetLocation(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/Admin/Locations/Details.cshtml", location);
        }

        // GET: Locations/Create
        public ActionResult Create()
        {
            ViewBag.ImageID = new SelectList(repository.GetImageSet(), "ImageID", "ImageText");
            return View("~/Views/Admin/Locations/Create.cshtml");
        }

        // POST: Locations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LocationID,LocationName,LocationType,AcessTypeRole,ShortDescription,LongDescription,ImageID")] Location location)
        {
            if (ModelState.IsValid)
            {
                repository.SaveLocation(location);
                return RedirectToAction("Index");
            }

            ViewBag.ImageID = new SelectList(repository.GetImageSet(), "ImageID", "ImageText", location.ImageID);
            return View("~/Views/Admin/Locations/Create.cshtml", location);
        }

        // GET: Locations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = repository.GetLocation(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            ViewBag.ImageID = new SelectList(repository.GetImageSet(), "ImageID", "ImageText", location.ImageID);
            return View("~/Views/Admin/Locations/Edit.cshtml", location);
        }

        // POST: Locations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LocationID,LocationName,LocationType,AcessTypeRole,ShortDescription,LongDescription,ImageID")] Location location)
        {
            if (ModelState.IsValid)
            {
                repository.UpdateLocation(location);
                return RedirectToAction("Index");
            }
            ViewBag.ImageID = new SelectList(repository.GetImageSet(), "ImageID", "ImageText", location.ImageID);
            return View("~/Views/Admin/Locations/Edit.cshtml", location);
        }

        // GET: Locations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = repository.GetLocation(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/Admin/Locations/Delete.cshtml", location);
        }

        // POST: Locations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Location location = repository.GetLocation(id);
            repository.RemoveLocation(location);
            return RedirectToAction("Index");
        }

    }
}
