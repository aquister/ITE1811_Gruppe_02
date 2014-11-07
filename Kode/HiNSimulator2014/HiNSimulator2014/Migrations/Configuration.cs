namespace HiNSimulator2014.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using HiNSimulator2014.Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity;
    using System.Collections.Generic;
    /// <summary>
    /// Skrevet av: Andreas Jansson og Pål Skogsrud
    /// 
    /// 
    /// </summary>
    internal sealed class Configuration : DbMigrationsConfiguration<HiNSimulator2014.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            
            
        }

        private UserManager<ApplicationUser> userManager;
        private UserStore<ApplicationUser> userStore;

        #region Opprett bruker metode
        /// <summary>
        /// Metode som lager en ny bruker
        /// </summary>
        /// <param name="_userName">Brukernavn</param>
        /// <param name="password">Passord</param>
        /// <param name="_playerName">Navn på spilleren</param>
        /// <param name="_accessLevel">Tilgangsrettigheter</param>
        /// <param name="_writePermission">Skriverettigheter</param>
        /// <param name="_currentLocation">Spillerens posisjon</param>
        /// <returns>Ny bruker</returns>
        private ApplicationUser createUser(string _userName, string password, string _playerName, string _accessLevel, bool _writePermission, Location _currentLocation)
        {
            var user = userManager.FindByName(_userName);
            

            if (user == null)
            {
                user = new ApplicationUser { UserName = _userName, PlayerName = _playerName, Email = _userName, AccessLevel = _accessLevel, WritePermission = _writePermission, CurrentLocation = _currentLocation };
                var result = userManager.Create(user, password);
            }
            else
            {
                string newPassword = password;
                string newHashPassword = userManager.PasswordHasher.HashPassword(newPassword);

                user.AccessLevel = _accessLevel;
                user.PlayerName = _playerName;
                user.UserName = _userName;
                user.WritePermission = _writePermission;
                user.CurrentLocation = _currentLocation;
                user.PasswordHash = newHashPassword;                

                var result = userManager.Update(user);
            }
            return user;
        }
        #endregion

        protected override void Seed(HiNSimulator2014.Models.ApplicationDbContext context)
        {

            userStore = new UserStore<ApplicationUser>(context);
            userManager = new UserManager<ApplicationUser>(userStore);
            //context.Database.Delete();            

            #region Steder
            var locations = new List<Location>{
                new Location{
                    LocationType = "Korridor", 
                    LocationName = "D3310", 
                    ShortDescription = "Gang", 
                    LongDescription = "Gang ved i tredje etasje"
                },
                 new Location{
                    LocationType = "Klasserom", 
                    LocationName = "D3320", 
                    ShortDescription = "Klasserom for elektronikkstudenter", 
                    LongDescription = "Klasserom for studenter som går Digital teknikk", 
                    AcessTypeRole = "EL"
                },
                 new Location{
                    LocationType = "Datarom", 
                    LocationName = "D3330", 
                    ShortDescription = "Linuxlabben", 
                    LongDescription = "Linuxlabben er det mest brukte rommet for studenter som går datateknikk ved høgskolen", 
                    AcessTypeRole = "DT"
                },
                 new Location{
                    LocationType = "Klasserom", 
                    LocationName = "D3340", 
                    ShortDescription = "Grunnlagslab", 
                    LongDescription = "Klasserom for elektronikkstudenter", 
                    AcessTypeRole = "EL"
                },
                 new Location{
                    LocationType = "Klasserom", 
                    LocationName = "D3350", 
                    ShortDescription = "Elektronikk produksjon", 
                    LongDescription = "Klasserom for elektronikkstudenter", 
                    AcessTypeRole = "EL"
                },
                new Location{
                    LocationType = "Klasserom", 
                    LocationName = "D3360", 
                    ShortDescription = "Verksted ELK", 
                    LongDescription = "Verksted for elektronikkstudenter", 
                    AcessTypeRole = "ELK"
                },
                new Location{
                    LocationType = "Korridor", 
                    LocationName = "C3020", 
                    ShortDescription = "Gang", 
                    LongDescription = "Gang ved siden av toalettene"
                },
                new Location{
                    LocationType = "Toalett", 
                    LocationName = "C3040", 
                    ShortDescription = "Herretoalett", 
                    LongDescription = "Her kan det gå strålende eller bare dritt :)"
                },
                new Location{
                    LocationType = "Toalett", 
                    LocationName ="C3050", 
                    ShortDescription = "Dametoalett", 
                    LongDescription = "Her kan det gå strålende eller bare dritt :)"
                },
                new Location{
                    LocationType = "Kontor", 
                    LocationName = "C2100", 
                    ShortDescription = "Einars kontor", 
                    LongDescription = "Einar er en hardtarbeidende student og har alltid tid til en prat"
                },
                new Location{
                    LocationType = "Gang", 
                    LocationName = "C2000", 
                    ShortDescription = "Gang", 
                    LongDescription = "Just an ordinary corridor"
                },
                new Location{
                    LocationType = "Gang", 
                    LocationName = "D2500-C", 
                    ShortDescription = "Galleri"
                },
                new Location{
                    LocationType = "Gang", 
                    LocationName = "Glassgata", 
                    ShortDescription = "Glassgata er en gate laget av glass"
                },
                new Location{
                    LocationType = "Gang", 
                    LocationName = "C1001", 
                    ShortDescription = "--"
                },
                new Location{
                    LocationType = "Kontor", 
                    LocationName = "C1070", 
                    ShortDescription = "HiN IL's kontor"
                },
                new Location{
                    LocationType = "Bru", 
                    LocationName = "BRU-2C", 
                    LongDescription = "Under broen bor det et troll"
                },
                new Location{
                    LocationType = "Bru", 
                    LocationName = "BRU-3C", 
                    LongDescription = "En fin bro"
                },
                new Location{
                    LocationType = "Gang", 
                    LocationName = "D2360", 
                    ShortDescription = "Dean & Project Area", 
                    LongDescription = "In this part is the Dean's office, but also offices to those that work with projects and simulations."
                },
                new Location{
                    LocationType = "Kontor", 
                    LocationName = "D2210", 
                    ShortDescription = "Hans Olofsen's office", 
                    LongDescription = "You are amazed by how tidy this office is."
                },
                new Location{
                    LocationType = "Kontor", 
                    LocationName = "D2280", 
                    ShortDescription = "The Dean's office", 
                    LongDescription = "What are you doing in the Dean's office without asking for permission!?!"
                },
                new Location{
                    LocationType = "Kontor", 
                    LocationName = "D2310", 
                    LongDescription = "Jostein er en veldig hyggelig stipendiat. Han hjelper gjerne."
                },
                new Location{
                    LocationType = "Gang", 
                    LocationName = "C3191", 
                    ShortDescription = "Engineering Design Area", 
                    LongDescription = ""
                },
                new Location{
                    LocationType = "Kontor", 
                    LocationName = "D3440", 
                    ShortDescription = "Guy's office", 
                    LongDescription = "Guy is a nice and funny guy."
                },
                new Location{
                    LocationType = "Kontor", 
                    LocationName = "D3400C", 
                    ShortDescription = "Prof. Per-Arne Sundsbø's office", 
                    LongDescription = "Prof. Sundsbø is a role-model professor"
                },
                new Location{
                    LocationType = "Kontor", 
                    LocationName = "D3480", 
                    ShortDescription = "Prof. Annette Meidell's office", 
                    LongDescription = "Prof. Meidell was the first female professor at Narvik Univeristy College"
                },
                new Location{
                    LocationType = "Gang", 
                    LocationName = "C4330", 
                    ShortDescription = "C4-area", 
                    LongDescription = "In the part of the building are the offices of the other computer engineering teachers. You decide not to disturb them."
                },
                new Location{
                    LocationType = "Gang",
                    LocationName = "C5460", 
                    ShortDescription = "C5-area",
                    LongDescription = "This floor belong to the Health Department. You have no business here."
                },
                new Location{
                    LocationType = "Gang", 
                    LocationName = "C6001", 
                    ShortDescription = "Parking area C", 
                    LongDescription = "Outside is parking for employees. You cannot open the doors."
                }
            };
            locations.ForEach(element => context.Locations.AddOrUpdate(locationName => locationName.LocationName, element));
            context.SaveChanges();
            
#endregion

            var location01 = context.Locations.Where(l => l.LocationID == 1).FirstOrDefault();
            var location02 = context.Locations.Where(l => l.LocationID == 2).FirstOrDefault();
            var location03 = context.Locations.Where(l => l.LocationID == 3).FirstOrDefault();
            var location04 = context.Locations.Where(l => l.LocationID == 4).FirstOrDefault();
            var location05 = context.Locations.Where(l => l.LocationID == 5).FirstOrDefault();
            var location06 = context.Locations.Where(l => l.LocationID == 6).FirstOrDefault();
            var location07 = context.Locations.Where(l => l.LocationID == 7).FirstOrDefault();
            var location08 = context.Locations.Where(l => l.LocationID == 8).FirstOrDefault();
            var location09 = context.Locations.Where(l => l.LocationID == 9).FirstOrDefault();
            var location10 = context.Locations.Where(l => l.LocationID == 10).FirstOrDefault();
            var location11 = context.Locations.Where(l => l.LocationID == 11).FirstOrDefault();
            var location12 = context.Locations.Where(l => l.LocationID == 12).FirstOrDefault();
            var location13 = context.Locations.Where(l => l.LocationID == 13).FirstOrDefault();
            var location14 = context.Locations.Where(l => l.LocationID == 14).FirstOrDefault();
            var location15 = context.Locations.Where(l => l.LocationID == 15).FirstOrDefault();
            var location16 = context.Locations.Where(l => l.LocationID == 16).FirstOrDefault();
            var location17 = context.Locations.Where(l => l.LocationID == 17).FirstOrDefault();
            var location18 = context.Locations.Where(l => l.LocationID == 18).FirstOrDefault();
            var location19 = context.Locations.Where(l => l.LocationID == 19).FirstOrDefault();
            var location20 = context.Locations.Where(l => l.LocationID == 20).FirstOrDefault();
            var location21 = context.Locations.Where(l => l.LocationID == 21).FirstOrDefault();
            var location22 = context.Locations.Where(l => l.LocationID == 22).FirstOrDefault();
            var location23 = context.Locations.Where(l => l.LocationID == 23).FirstOrDefault();
            var location24 = context.Locations.Where(l => l.LocationID == 24).FirstOrDefault();
            var location25 = context.Locations.Where(l => l.LocationID == 25).FirstOrDefault();
            var location26 = context.Locations.Where(l => l.LocationID == 26).FirstOrDefault();
            var location27 = context.Locations.Where(l => l.LocationID == 27).FirstOrDefault();
            var location28 = context.Locations.Where(l => l.LocationID == 28).FirstOrDefault();

            #region Ting
            var things = new List<Thing>{
                new Thing{
                    Name = "Cola-boks",
                    Description = "En cola-boks med cola inni evt oppi. Den er full.",
                    IsStationary = false,
                    WrittenText = "Inneholder koockain og herpes.",
                    LocationID = context.Locations.Where(l => l.LocationName == "D3360").FirstOrDefault().LocationID,
                    PlayerWritable = false
                },
                new Thing{
                    Name = "Tavle",
                    Description = "En kul gul tavle som henger på veggen.",
                    IsStationary = true,
                    WrittenText = "Whaaaaaaaat?",
                    LocationID = context.Locations.Where(l => l.LocationName == "D3330").FirstOrDefault().LocationID,
                    PlayerWritable = true
                },
                new Thing{
                    Name = "Bazooka",
                    Description = "En sovjetisk bazooka fra den kalde krigen.",
                    IsStationary = false,
                    WrittenText = "что-то смешное на русском.",
                    LocationID = context.Locations.Where(l => l.LocationName == "D2210").FirstOrDefault().LocationID,
                    PlayerWritable = false
                },
            };

            things.ForEach(element => context.Things.AddOrUpdate(u => u.Name, element));
            context.SaveChanges();
            #endregion

            #region AI
            var artificialPlayer = new List<ArtificialPlayer>{
                new ArtificialPlayer{
                    Name = "Hans Olofsen",
                    Description = "Hans er en trivelig dude som jobber på skolen",
                    IsStationary = false,
                    Type = "Førstelektor",
                    LocationID = context.Locations.Where(l => l.LocationName == "D2210").FirstOrDefault().LocationID,
                    AccessLevel = "Universal"
                },
                new ArtificialPlayer{
                    Name = "Knut Collin",
                    Description = "Høgskolelektor med avansert kunnskap innen forskjellige programmeringsspråk",
                    IsStationary = false,
                    Type = "Høgskolelektor",
                    LocationID = context.Locations.Where(l => l.LocationName == "D3330").FirstOrDefault().LocationID,
                    AccessLevel = "Universal"
                },
                new ArtificialPlayer{
                    Name = "Arvid Urke",
                    Description = "Snasn kis med mye på hjertet",
                    IsStationary = false,
                    Type = "Rådgiver",
                    LocationID = context.Locations.Where(l => l.LocationName == "Glassgata").FirstOrDefault().LocationID,
                    AccessLevel = "Universal"
                },
                new ArtificialPlayer{
                    Name = "Dracula",
                    Description = "Skummel kar som biter",
                    IsStationary = false,
                    Type = "Vampyr",
                    LocationID = context.Locations.Where(l => l.LocationName == "C3050").FirstOrDefault().LocationID,
                    AccessLevel = "Universal"
                }

            };

            artificialPlayer.ForEach(element => context.ArtificialPlayers.AddOrUpdate(u => u.Name, element));
            context.SaveChanges();
            #endregion

            #region Brukere
            var userPaal = createUser("pskogsru88@hotmail.com", "appelsinFarge5", "Gerrard", "Datateknikk", false, location03);
            var userTina = createUser("tinahotty64@hotmail.com", "appelsinFarge5", "Tina", "Datateknikk", false, location03);
            var userKristina = createUser("kristinamyrligundersen@gmail.com", "appelsinFarge5", "Kristina", "Datateknikk", false, location03);
            var userAndreas = createUser("drknert@gmail.com", "appelsinFarge5", "Andreas", "Datateknikk", false, location03);
            var userAlexander = createUser("alec90@gmail.com", "appelsinFarge5", "Alexander", "Datateknikk", false, location03);
            var userMarius = createUser("skaterase@gmail.com", "appelsinFarge5", "Marius", "Datateknikk", false, location03);
            #endregion

            #region Kommandoer
            var commands = new List<Command>
            {
                new Command {
                    Name = "Take",
                    Description = "Plukker opp valgt gjenstand"
                },
                new Command {
                    Name = "Open",
                    Description = "Åpner dør, skap o.l"
                },
                new Command {
                    Name = "Use",
                    Description = "Aktiverer funksjon på objekt"
                },
                new Command {
                    Name = "Drop",
                    Description = "Legg fra deg valgt gjenstand"
                },
                new Command {
                    Name = "Turn on",
                    Description = "Slår på objekt"
                },
                new Command {
                    Name = "Turn off",
                    Description = "Slår av objekt"
                },
                new Command {
                    Name = "Talk",
                    Description = "Starter samtale i valgt rom"
                },
                new Command {
                    Name = "Kick",
                    Description = "Sparker et objekt"
                },
                new Command {
                    Name = "Close",
                    Description = "Lukker dør, skap ol."
                },
                new Command {
                    Name = "Enter",
                    Description = "Går inn dør, rom"
                },
                new Command {
                    Name = "Write on",
                    Description = "Åpner en editor for å endre tekst på et objekt"
                },
                new Command {
                    Name = "Look at",
                    Description = "Viser detaljert informasjon om objekt"
                },
                new Command {
                    Name = "Punch",
                    Description = "Slår et objekt"
                },
            };
            commands.ForEach(element => context.Commands.AddOrUpdate(u => u.Name, element));
            context.SaveChanges();
            #endregion

            #region Kunstige spillere
            var artificialPlayerResponses = new List<ArtificialPlayerResponse>
            {
            #region Hans 
                new ArtificialPlayerResponse 
                {
                    ArtificialPlayerID = context.ArtificialPlayers.Where(l => l.Name == "Hans Olofsen").FirstOrDefault().ArtificialPlayerID,
                    ResponseText = "God dag!"
                },
                new ArtificialPlayerResponse 
                {
                    ArtificialPlayerID = context.ArtificialPlayers.Where(l => l.Name == "Hans Olofsen").FirstOrDefault().ArtificialPlayerID,
                    ResponseText = "Jeg har et veldig ryddig kontor"
                },
                new ArtificialPlayerResponse 
                {
                    ArtificialPlayerID = context.ArtificialPlayers.Where(l => l.Name == "Hans Olofsen").FirstOrDefault().ArtificialPlayerID,
                    ResponseText = "Fint vær i dag"
                },
                new ArtificialPlayerResponse 
                {
                    ArtificialPlayerID = context.ArtificialPlayers.Where(l => l.Name == "Hans Olofsen").FirstOrDefault().ArtificialPlayerID,
                    ResponseText = "Jeg lærer meg asp.NET"
                },
                new ArtificialPlayerResponse 
                {
                    ArtificialPlayerID = context.ArtificialPlayers.Where(l => l.Name == "Hans Olofsen").FirstOrDefault().ArtificialPlayerID,
                    ResponseText = "Skolen er full av hemmeligheter!"
                },
            #endregion
            #region kc
                new ArtificialPlayerResponse 
                {
                    ArtificialPlayerID = context.ArtificialPlayers.Where(l => l.Name == "Knut Collin").FirstOrDefault().ArtificialPlayerID,
                    ResponseText = "..."
                },
                new ArtificialPlayerResponse 
                {
                    ArtificialPlayerID = context.ArtificialPlayers.Where(l => l.Name == "Knut Collin").FirstOrDefault().ArtificialPlayerID,
                    ResponseText = "Jeg har masse å gjøre"
                },
                new ArtificialPlayerResponse 
                {
                    ArtificialPlayerID = context.ArtificialPlayers.Where(l => l.Name == "Knut Collin").FirstOrDefault().ArtificialPlayerID,
                    ResponseText = "Jeg har rettet oblig'en din, det ser meget bra ut"
                },
                new ArtificialPlayerResponse 
                {
                    ArtificialPlayerID = context.ArtificialPlayers.Where(l => l.Name == "Knut Collin").FirstOrDefault().ArtificialPlayerID,
                    ResponseText = "Jeg skal holde et foredrag om java i morgen"
                },
            #endregion
            #region urke
                new ArtificialPlayerResponse 
                {
                    ArtificialPlayerID = context.ArtificialPlayers.Where(l => l.Name == "Arvid Urke").FirstOrDefault().ArtificialPlayerID,
                    ResponseText = "Stå på! Tenk på dopaminet!"
                },
                new ArtificialPlayerResponse 
                {
                    ArtificialPlayerID = context.ArtificialPlayers.Where(l => l.Name == "Arvid Urke").FirstOrDefault().ArtificialPlayerID,
                    ResponseText = "Du kan klare alt! Fortsett sånn."
                },
                new ArtificialPlayerResponse 
                {
                    ArtificialPlayerID = context.ArtificialPlayers.Where(l => l.Name == "Arvid Urke").FirstOrDefault().ArtificialPlayerID,
                    ResponseText = "Jeg skal personlig sørge for at du får A+ på eksamen"
                },
                new ArtificialPlayerResponse 
                {
                    ArtificialPlayerID = context.ArtificialPlayers.Where(l => l.Name == "Arvid Urke").FirstOrDefault().ArtificialPlayerID,
                    ResponseText = "Høgskolen i Narvik er den beste i Norge, nei, hele verden!"
                },
                new ArtificialPlayerResponse 
                {
                    ArtificialPlayerID = context.ArtificialPlayers.Where(l => l.Name == "Arvid Urke").FirstOrDefault().ArtificialPlayerID,
                    ResponseText = "Alle husker dagen Urke mistet buksene"
                },
            #endregion
            #region dracula
                new ArtificialPlayerResponse 
                {
                    ArtificialPlayerID = context.ArtificialPlayers.Where(l => l.Name == "Dracula").FirstOrDefault().ArtificialPlayerID,
                    ResponseText = "Ha, ha! Jeg er Dracula!"
                },
                new ArtificialPlayerResponse 
                {
                    ArtificialPlayerID = context.ArtificialPlayers.Where(l => l.Name == "Dracula").FirstOrDefault().ArtificialPlayerID,
                    ResponseText = "I natt skal det skje."
                },
                new ArtificialPlayerResponse 
                {
                    ArtificialPlayerID = context.ArtificialPlayers.Where(l => l.Name == "Dracula").FirstOrDefault().ArtificialPlayerID,
                    ResponseText = "Du kan ikke rømme fra Dracula!"
                },
                new ArtificialPlayerResponse 
                {
                    ArtificialPlayerID = context.ArtificialPlayers.Where(l => l.Name == "Dracula").FirstOrDefault().ArtificialPlayerID,
                    ResponseText = "Hvem våger å forstyrre Dracula?"
                },
                new ArtificialPlayerResponse 
                {
                    ArtificialPlayerID = context.ArtificialPlayers.Where(l => l.Name == "Dracula").FirstOrDefault().ArtificialPlayerID,
                    ResponseText = "It's idnight and the oon is up"
                }
            #endregion  

            };

            artificialPlayerResponses.ForEach(element => context.ArtificialPlayerResponses.AddOrUpdate(u => u.ResponseText, element));
            context.SaveChanges();
            #endregion

            #region Stedsforbindelse
            var locationConnections = new List<LocationConnection>
            {
#region D3310
                new LocationConnection {
                    LocationOne_LocationID = context.Locations.Where(l => l.LocationName == "D3310").FirstOrDefault().LocationID,
                    LocationTwo_LocationID = context.Locations.Where(l => l.LocationName == "D3320").FirstOrDefault().LocationID,

                },
                new LocationConnection {
                    LocationOne_LocationID = context.Locations.Where(l => l.LocationName == "D3310").FirstOrDefault().LocationID,
                    LocationTwo_LocationID = context.Locations.Where(l => l.LocationName == "D3330").FirstOrDefault().LocationID,

                },
                new LocationConnection {
                    LocationOne_LocationID = context.Locations.Where(l => l.LocationName == "D3310").FirstOrDefault().LocationID,
                    LocationTwo_LocationID = context.Locations.Where(l => l.LocationName == "D3340").FirstOrDefault().LocationID,
                },
                new LocationConnection {
                    LocationOne_LocationID = context.Locations.Where(l => l.LocationName == "D3310").FirstOrDefault().LocationID,
                    LocationTwo_LocationID = context.Locations.Where(l => l.LocationName == "D3350").FirstOrDefault().LocationID,

                },
                new LocationConnection {
                    LocationOne_LocationID = context.Locations.Where(l => l.LocationName == "D3310").FirstOrDefault().LocationID,
                    LocationTwo_LocationID = context.Locations.Where(l => l.LocationName == "D3360").FirstOrDefault().LocationID,

                },
                new LocationConnection {
                    LocationOne_LocationID = context.Locations.Where(l => l.LocationName == "D3310").FirstOrDefault().LocationID,
                    LocationTwo_LocationID = context.Locations.Where(l => l.LocationName == "C3020").FirstOrDefault().LocationID,

                },
#endregion
#region C3020
                new LocationConnection {
                    LocationOne_LocationID = context.Locations.Where(l => l.LocationName == "C3020").FirstOrDefault().LocationID,
                    LocationTwo_LocationID = context.Locations.Where(l => l.LocationName == "C3191").FirstOrDefault().LocationID,

                },
                new LocationConnection {
                    LocationOne_LocationID = context.Locations.Where(l => l.LocationName == "C3020").FirstOrDefault().LocationID,
                    LocationTwo_LocationID = context.Locations.Where(l => l.LocationName == "BRU-3C").FirstOrDefault().LocationID,

                },
                new LocationConnection {
                    LocationOne_LocationID = context.Locations.Where(l => l.LocationName == "C3020").FirstOrDefault().LocationID,
                    LocationTwo_LocationID = context.Locations.Where(l => l.LocationName == "C4330").FirstOrDefault().LocationID,

                },
                new LocationConnection {
                    LocationOne_LocationID = context.Locations.Where(l => l.LocationName == "C3020").FirstOrDefault().LocationID,
                    LocationTwo_LocationID = context.Locations.Where(l => l.LocationName == "C5460").FirstOrDefault().LocationID,

                },
                new LocationConnection {
                    LocationOne_LocationID = context.Locations.Where(l => l.LocationName == "C3020").FirstOrDefault().LocationID,
                    LocationTwo_LocationID = context.Locations.Where(l => l.LocationName == "C2000").FirstOrDefault().LocationID,

                },
                new LocationConnection {
                    LocationOne_LocationID = context.Locations.Where(l => l.LocationName == "C3020").FirstOrDefault().LocationID,
                    LocationTwo_LocationID = context.Locations.Where(l => l.LocationName == "D2500-C").FirstOrDefault().LocationID,

                },
                new LocationConnection {
                    LocationOne_LocationID = context.Locations.Where(l => l.LocationName == "C3020").FirstOrDefault().LocationID,
                    LocationTwo_LocationID = context.Locations.Where(l => l.LocationName == "C3040").FirstOrDefault().LocationID,

                },
                new LocationConnection {
                    LocationOne_LocationID = context.Locations.Where(l => l.LocationName == "C3020").FirstOrDefault().LocationID,
                    LocationTwo_LocationID = context.Locations.Where(l => l.LocationName == "D3350").FirstOrDefault().LocationID,

                },

#endregion

#region C2000
                new LocationConnection {
                    LocationOne_LocationID = context.Locations.Where(l => l.LocationName == "C2000").FirstOrDefault().LocationID,
                    LocationTwo_LocationID = context.Locations.Where(l => l.LocationName == "C2100").FirstOrDefault().LocationID,

                },
                new LocationConnection {
                    LocationOne_LocationID = context.Locations.Where(l => l.LocationName == "C2000").FirstOrDefault().LocationID,
                    LocationTwo_LocationID = context.Locations.Where(l => l.LocationName == "D2500-C").FirstOrDefault().LocationID,

                },
                new LocationConnection {
                    LocationOne_LocationID = context.Locations.Where(l => l.LocationName == "C2000").FirstOrDefault().LocationID,
                    LocationTwo_LocationID = context.Locations.Where(l => l.LocationName == "C1001").FirstOrDefault().LocationID,

                },
#endregion
#region D2500
                new LocationConnection {
                    LocationOne_LocationID = context.Locations.Where(l => l.LocationName == "D2500-C").FirstOrDefault().LocationID,
                    LocationTwo_LocationID = context.Locations.Where(l => l.LocationName == "D2360").FirstOrDefault().LocationID,

                },
                new LocationConnection {
                    LocationOne_LocationID = context.Locations.Where(l => l.LocationName == "D2500-C").FirstOrDefault().LocationID,
                    LocationTwo_LocationID = context.Locations.Where(l => l.LocationName == "BRU-2C").FirstOrDefault().LocationID,

                },
                new LocationConnection {
                    LocationOne_LocationID = context.Locations.Where(l => l.LocationName == "D2500-C").FirstOrDefault().LocationID,
                    LocationTwo_LocationID = context.Locations.Where(l => l.LocationName == "Glassgata").FirstOrDefault().LocationID,

                },
#endregion
#region Glassgata
                new LocationConnection {
                    LocationOne_LocationID = context.Locations.Where(l => l.LocationName == "Glassgata").FirstOrDefault().LocationID,
                    LocationTwo_LocationID = context.Locations.Where(l => l.LocationName == "C1001").FirstOrDefault().LocationID,

                },
#endregion
#region C1001
                new LocationConnection {
                    LocationOne_LocationID = context.Locations.Where(l => l.LocationName == "C1001").FirstOrDefault().LocationID,
                    LocationTwo_LocationID = context.Locations.Where(l => l.LocationName == "C1070").FirstOrDefault().LocationID,

                },
#endregion
#region D2360
                new LocationConnection {
                    LocationOne_LocationID = context.Locations.Where(l => l.LocationName == "D2360").FirstOrDefault().LocationID,
                    LocationTwo_LocationID = context.Locations.Where(l => l.LocationName == "D2280").FirstOrDefault().LocationID,

                },
                new LocationConnection {
                    LocationOne_LocationID = context.Locations.Where(l => l.LocationName == "D2360").FirstOrDefault().LocationID,
                    LocationTwo_LocationID = context.Locations.Where(l => l.LocationName == "D2310").FirstOrDefault().LocationID,

                },
                new LocationConnection {
                    LocationOne_LocationID = context.Locations.Where(l => l.LocationName == "D2360").FirstOrDefault().LocationID,
                    LocationTwo_LocationID = context.Locations.Where(l => l.LocationName == "D2210").FirstOrDefault().LocationID,

                },
#endregion
#region C3191
                new LocationConnection {
                    LocationOne_LocationID = context.Locations.Where(l => l.LocationName == "C3191").FirstOrDefault().LocationID,
                    LocationTwo_LocationID = context.Locations.Where(l => l.LocationName == "D3440").FirstOrDefault().LocationID,

                },
                new LocationConnection {
                    LocationOne_LocationID = context.Locations.Where(l => l.LocationName == "C3191").FirstOrDefault().LocationID,
                    LocationTwo_LocationID = context.Locations.Where(l => l.LocationName == "D3400C").FirstOrDefault().LocationID,

                },
                new LocationConnection {
                    LocationOne_LocationID = context.Locations.Where(l => l.LocationName == "C3191").FirstOrDefault().LocationID,
                    LocationTwo_LocationID = context.Locations.Where(l => l.LocationName == "D3480").FirstOrDefault().LocationID,

                },
#endregion
#region C4330
                new LocationConnection {
                    LocationOne_LocationID = context.Locations.Where(l => l.LocationName == "C4330").FirstOrDefault().LocationID,
                    LocationTwo_LocationID = context.Locations.Where(l => l.LocationName == "C5460").FirstOrDefault().LocationID,

                },
#endregion
#region C5460
                new LocationConnection {
                    LocationOne_LocationID = context.Locations.Where(l => l.LocationName == "C5460").FirstOrDefault().LocationID,
                    LocationTwo_LocationID = context.Locations.Where(l => l.LocationName == "C6001").FirstOrDefault().LocationID,

                }
#endregion

            };
            locationConnections.ForEach(element => context.LocationConnections.AddOrUpdate(l => l.LocationOne_LocationID, element));
            context.SaveChanges();
            #endregion

            #region Gyldige kommandoer for AI
            var validCommandForAi = new List<ValidCommandsForArtificialPlayers>{
#region Hans Olofsen    
                new ValidCommandsForArtificialPlayers{
                    ArtificialPlayerID = context.ArtificialPlayers.Where(u => u.Name == "Hans Olofsen").FirstOrDefault().ArtificialPlayerID,
                    CommandID = context.Commands.Where(u => u.Name == "Talk").FirstOrDefault().CommandID,
                },
                new ValidCommandsForArtificialPlayers{
                    ArtificialPlayerID = context.ArtificialPlayers.Where(u => u.Name == "Hans Olofsen").FirstOrDefault().ArtificialPlayerID,
                    CommandID = context.Commands.Where(u => u.Name == "Look at").FirstOrDefault().CommandID,
                },
                new ValidCommandsForArtificialPlayers{
                    ArtificialPlayerID = context.ArtificialPlayers.Where(u => u.Name == "Hans Olofsen").FirstOrDefault().ArtificialPlayerID,
                    CommandID = context.Commands.Where(u => u.Name == "Punch").FirstOrDefault().CommandID,
                },
#endregion
#region Knut Collin    
                new ValidCommandsForArtificialPlayers{
                    ArtificialPlayerID = context.ArtificialPlayers.Where(u => u.Name == "Knut Collin").FirstOrDefault().ArtificialPlayerID,
                    CommandID = context.Commands.Where(u => u.Name == "Talk").FirstOrDefault().CommandID,
                },
                new ValidCommandsForArtificialPlayers{
                    ArtificialPlayerID = context.ArtificialPlayers.Where(u => u.Name == "Knut Collin").FirstOrDefault().ArtificialPlayerID,
                    CommandID = context.Commands.Where(u => u.Name == "Look at").FirstOrDefault().CommandID,
                },
                new ValidCommandsForArtificialPlayers{
                    ArtificialPlayerID = context.ArtificialPlayers.Where(u => u.Name == "Knut Collin").FirstOrDefault().ArtificialPlayerID,
                    CommandID = context.Commands.Where(u => u.Name == "Punch").FirstOrDefault().CommandID,
                },
#endregion
#region Arvid Urke   
                new ValidCommandsForArtificialPlayers{
                    ArtificialPlayerID = context.ArtificialPlayers.Where(u => u.Name == "Arvid Urke").FirstOrDefault().ArtificialPlayerID,
                    CommandID = context.Commands.Where(u => u.Name == "Talk").FirstOrDefault().CommandID,
                },
                new ValidCommandsForArtificialPlayers{
                    ArtificialPlayerID = context.ArtificialPlayers.Where(u => u.Name == "Arvid Urke").FirstOrDefault().ArtificialPlayerID,
                    CommandID = context.Commands.Where(u => u.Name == "Look at").FirstOrDefault().CommandID,
                },
                new ValidCommandsForArtificialPlayers{
                    ArtificialPlayerID = context.ArtificialPlayers.Where(u => u.Name == "Arvid Urke").FirstOrDefault().ArtificialPlayerID,
                    CommandID = context.Commands.Where(u => u.Name == "Punch").FirstOrDefault().CommandID,
                },
                new ValidCommandsForArtificialPlayers{
                    ArtificialPlayerID = context.ArtificialPlayers.Where(u => u.Name == "Arvid Urke").FirstOrDefault().ArtificialPlayerID,
                    CommandID = context.Commands.Where(u => u.Name == "Turn off").FirstOrDefault().CommandID,
                },
#endregion
#region Dracula   
                new ValidCommandsForArtificialPlayers{
                    ArtificialPlayerID = context.ArtificialPlayers.Where(u => u.Name == "Dracula").FirstOrDefault().ArtificialPlayerID,
                    CommandID = context.Commands.Where(u => u.Name == "Talk").FirstOrDefault().CommandID,
                },
                new ValidCommandsForArtificialPlayers{
                    ArtificialPlayerID = context.ArtificialPlayers.Where(u => u.Name == "Dracula").FirstOrDefault().ArtificialPlayerID,
                    CommandID = context.Commands.Where(u => u.Name == "Look at").FirstOrDefault().CommandID,
                },
                new ValidCommandsForArtificialPlayers{
                    ArtificialPlayerID = context.ArtificialPlayers.Where(u => u.Name == "Dracula").FirstOrDefault().ArtificialPlayerID,
                    CommandID = context.Commands.Where(u => u.Name == "Punch").FirstOrDefault().CommandID,
                },
                new ValidCommandsForArtificialPlayers{
                    ArtificialPlayerID = context.ArtificialPlayers.Where(u => u.Name == "Dracula").FirstOrDefault().ArtificialPlayerID,
                    CommandID = context.Commands.Where(u => u.Name == "Kick").FirstOrDefault().CommandID,
                }
#endregion
            };
            validCommandForAi.ForEach(element => context.ValidCommandsForArtificialPlayers.AddOrUpdate(l => l.ArtificialPlayerID, element));
            context.SaveChanges();
            #endregion

            #region Gyldige kommandoer for ting
            var validCommandForThings = new List<ValidCommandsForThings>{
#region Cola-boks   
                new ValidCommandsForThings{
                    ThingID = context.Things.Where(u => u.Name == "Cola-boks").FirstOrDefault().ThingID,
                    CommandID = context.Commands.Where(u => u.Name == "Take").FirstOrDefault().CommandID,
                },
                new ValidCommandsForThings{
                    ThingID = context.Things.Where(u => u.Name == "Cola-boks").FirstOrDefault().ThingID,
                    CommandID = context.Commands.Where(u => u.Name == "Use").FirstOrDefault().CommandID,
                },
                new ValidCommandsForThings{
                    ThingID = context.Things.Where(u => u.Name == "Cola-boks").FirstOrDefault().ThingID,
                    CommandID = context.Commands.Where(u => u.Name == "Drop").FirstOrDefault().CommandID,
                },
                new ValidCommandsForThings{
                    ThingID = context.Things.Where(u => u.Name == "Cola-boks").FirstOrDefault().ThingID,
                    CommandID = context.Commands.Where(u => u.Name == "Look at").FirstOrDefault().CommandID,
                },
                new ValidCommandsForThings{
                    ThingID = context.Things.Where(u => u.Name == "Cola-boks").FirstOrDefault().ThingID,
                    CommandID = context.Commands.Where(u => u.Name == "Punch").FirstOrDefault().CommandID,
                },

#endregion
#region Tavle  
                new ValidCommandsForThings{
                    ThingID = context.Things.Where(u => u.Name == "Tavle").FirstOrDefault().ThingID,
                    CommandID = context.Commands.Where(u => u.Name == "Use").FirstOrDefault().CommandID,
                },
                new ValidCommandsForThings{
                    ThingID = context.Things.Where(u => u.Name == "Tavle").FirstOrDefault().ThingID,
                    CommandID = context.Commands.Where(u => u.Name == "Write on").FirstOrDefault().CommandID,
                },
                new ValidCommandsForThings{
                    ThingID = context.Things.Where(u => u.Name == "Tavle").FirstOrDefault().ThingID,
                    CommandID = context.Commands.Where(u => u.Name == "Look at").FirstOrDefault().CommandID,
                },
#endregion
#region Bazooka
                new ValidCommandsForThings{
                    ThingID = context.Things.Where(u => u.Name == "Bazooka").FirstOrDefault().ThingID,
                    CommandID = context.Commands.Where(u => u.Name == "Take").FirstOrDefault().CommandID,
                },
                new ValidCommandsForThings{
                    ThingID = context.Things.Where(u => u.Name == "Bazooka").FirstOrDefault().ThingID,
                    CommandID = context.Commands.Where(u => u.Name == "Use").FirstOrDefault().CommandID,
                },
                new ValidCommandsForThings{
                    ThingID = context.Things.Where(u => u.Name == "Bazooka").FirstOrDefault().ThingID,
                    CommandID = context.Commands.Where(u => u.Name == "Drop").FirstOrDefault().CommandID,
                },
                new ValidCommandsForThings{
                   ThingID = context.Things.Where(u => u.Name == "Bazooka").FirstOrDefault().ThingID,
                    CommandID = context.Commands.Where(u => u.Name == "Look at").FirstOrDefault().CommandID,
                }
#endregion

            };
            validCommandForThings.ForEach(element => context.ValidCommandsForThings.AddOrUpdate(l => l.ThingID, element));
            context.SaveChanges();
            #endregion
        }

        
    }
}
