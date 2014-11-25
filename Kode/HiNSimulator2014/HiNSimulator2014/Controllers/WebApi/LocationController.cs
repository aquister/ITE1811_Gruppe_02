﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Host.SystemWeb;
using HiNSimulator2014.Models;
using HiNSimulator2014.Classes;
using System.Diagnostics;

namespace HiNSimulator2014.Controllers.WebApi
{
    /// <summary>
    /// LocationController - tar seg av forespørseler som er relaterte til navigasjon
    /// og andre Location-baserte tjenester.
    /// 
    /// @author Andreas Dyrøy Jansson
    /// </summary>
    [Authorize]
    public class LocationController : ApiController
    {
        private IRepository repository;
        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }


        public LocationController()
        {
            repository = new Repository();


        }

        public LocationController(IRepository ir)
        {
            repository = ir;
            
        }

        private void UpdatePlayerLocation(int id)
        {
            Debug.Write("flytter til: " + id);
            ApplicationUser user = UserManager.FindById(User.Identity.GetUserId());
            user.CurrentLocation = repository.GetLocation(id);
            UserManager.Update(user);
            //repository.UpdatePlayerLocation(User.Identity.Name, id);
        }

        // do later: http://stackoverflow.com/questions/1877225/how-do-i-unit-test-a-controller-method-that-has-the-authorize-attribute-applie
        // Sender med et ApplicationUser-objekt for å kunne brukes i test-metoden
        public int CheckAccess(int id, ApplicationUser user)
        {
            // Sjekker om spilleren har tilgang til ønsket rom, enten ved at døren er åpen, eller
            // Spilleren har en Thing i sitt Inventory med påkrevd KeyLevel.
            // startbetingelse -1 gir 0, som betyr åpen dør.
            if (id == -1)
                return 0;

            Location currentLocation = GetCurrentLocation(user);
            LocationConnection lc = repository.GetLocationConnection(currentLocation.LocationID, id);
                
            List<Thing> currentInventory = repository.GetThingsForOwner(user);
            Debug.Write("\nCurrentLocation: " + currentLocation.LocationID + ", NextLocation: " + id);
            if (lc != null)
            {
                // Hvis døren er default åpen
                if (lc.RequiredKeyLevel <= 0)
                    return 0;

                Debug.Write("\nLocationConnection from: " + lc.LocationOne_LocationID + " to: " + lc.LocationTwo_LocationID);
                Debug.Write("isLocked: " + lc.IsLocked);
                foreach (Thing t in currentInventory)
                {
                    if (t.KeyLevel.HasValue)
                    {
                        if (t.KeyLevel >= lc.RequiredKeyLevel)
                        {
                            return -1;
                        }
                    }
                }
            }
            return lc.RequiredKeyLevel;
        }

        // GET api/Location/MoveTo/5
        [HttpGet]
        public SimpleLocation MoveTo(int id)
        {
            Location currentLocation;
            var user = UserManager.FindById(User.Identity.GetUserId());
            int keyLevel = CheckAccess(id, user);

            // Hvis id != -1 kom kallet fra en knapp hos klienten
            // CheckAccess om døren er åpen/kan åpnes
            if (id != -1 && keyLevel <= 0)
            {
                UpdatePlayerLocation(id);
                currentLocation = repository.GetLocation(id);
            }
            else
            {   // Hvis ikke hentes lagret location fra databasen
                currentLocation = GetCurrentLocation(user);
            }

            // Lager et nytt SimpleLocation-objekt
            SimpleLocation simpleLocation = new SimpleLocation();
            simpleLocation.keyReturn = keyLevel;
            simpleLocation.LocationId = currentLocation.LocationID;
            simpleLocation.LocationName = currentLocation.LocationName;
            simpleLocation.LocationInfo = GetInfo(id);

            // Hvis lokasjonen har et bilde
            if (currentLocation.ImageID.HasValue)
                simpleLocation.ImageID = (int)currentLocation.ImageID;

            var connectedLocations = repository.GetConnectedLocations(currentLocation.LocationID);
            foreach (Location l in connectedLocations)
            {
                simpleLocation.AddLocation(new SimpleLocation { 
                    LocationId = l.LocationID, 
                    LocationName = l.LocationName,
                    LocationInfo = getToolTip(l)
                });
            }
            return simpleLocation;
        }

        // Henter lagret posissjon fra databasen
        public Location GetCurrentLocation(ApplicationUser user)
        {
            if (user != null && user.CurrentLocation != null)
                return repository.GetLocation(user.CurrentLocation.LocationID);
            else
                return repository.GetLocation("Glassgata");
        }

        // Genererer en string med info om valgt location
        private String GetInfo(int id)
        {
            if (id != -1)
            {
                var location = repository.GetLocation(id);
                string init = "<strong>" + location.LocationName + ": </strong>";
                if (location.ShortDescription != null && location.LongDescription != null)
                    return  init + "You are " + location.ShortDescription + " | " + location.LongDescription;

                if (location.ShortDescription == null)
                    return init + "You are " + location.LongDescription;

                return init + "You are " + location.ShortDescription;
            }
            // Fancy kul velkomstmelding
            var user = UserManager.FindById(User.Identity.GetUserId());
            String locationInfo = GetInfo(repository.GetLocation("Glassgata").LocationID);
            if (user != null && user.CurrentLocation != null)
                locationInfo = GetInfo(user.CurrentLocation.LocationID);
            return "Welcome to HiN. " + locationInfo;
        }

        private String getToolTip(Location location)
        {
            return location.ShortDescription == null ? 
                location.LongDescription : location.ShortDescription;
        }

    }
}
