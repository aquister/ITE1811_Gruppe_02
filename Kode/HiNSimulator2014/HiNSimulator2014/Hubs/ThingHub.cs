﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Host.SystemWeb;
using Microsoft.AspNet.SignalR;

using HiNSimulator2014.Models;

namespace HiNSimulator2014.Hubs
{
    [Authorize]
    public class ThingHub : Hub
    {
        public Task JoinLocation(string LocationID)
        {
            return Groups.Add(Context.ConnectionId, LocationID);
        }

        public Task LeaveLocation(string LocationID)
        {
            return Groups.Remove(Context.ConnectionId, LocationID);
        }

        public Task RemoveLocationThing(string LocationID, string thingID)
        {
            return Clients.Group(LocationID).removeLocationThing(thingID);
        }

        public Task AddLocationThing(string LocationID, string thingID, string thingName)
        {
            return Clients.Group(LocationID).addLocationThing(thingID, thingName);
        }
    }
}