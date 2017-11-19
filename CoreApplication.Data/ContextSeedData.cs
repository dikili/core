using CoreApplication.Data.Models;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CoreApplication.Data.DataEntities;
using MongoDB.Bson.Serialization.Serializers;
using Microsoft.AspNetCore.Identity;

namespace CoreApplication.Data
{
    public class ContextSeedData
    {
        private readonly CoreContext _context;
        private readonly IHostingEnvironment _env;
        private readonly UserManager<AdUser> _userManager;

        public ContextSeedData(CoreContext context, IHostingEnvironment env, UserManager<AdUser> userManager)
        {
            _context = context;
            _env = env;
            _userManager = userManager;
        }

        public async Task Seed()
        {
            // Ensure that database is created ..
            _context.Database.EnsureCreated();

            //check if initial user is created or not

            var user = await _userManager.FindByEmailAsync("dikili@dikili.com");

            if (user == null)
            {
                //var users = new List<AdUser>
                //{
                //    new AdUser(){FirstName = "Hot" ,LastName = "Girl" ,Email ="dikili@dikili.com" },
                //    new AdUser(){FirstName = "Mature" ,LastName = "Girl"},
                //    new AdUser(){FirstName = "Math" ,LastName = "Genius"},
                //    new AdUser(){FirstName = "Another" ,LastName = "Girl"},
                //    new AdUser(){FirstName = "Jumper" ,LastName = "Jumper"},
                //    new AdUser(){FirstName = "Bulb" ,LastName = "Sale"},
                //};

                user = new AdUser
                {
                    UserName = "Dikili10",
                    FirstName = "Crazy",
                    LastName = "Guy",
                    Email = "dikili@dikili.com"
                };

                var result = await _userManager.CreateAsync(user, "P@ssw0rd!");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("User not created");
                }
            }

            // Need to read from a file for the sample data

            //var filePath = Path.Combine(Directory.GetParent(_env.ContentRootPath).ToString(), @"CoreApplication.Data/Stop.json");
            //var json = File.ReadAllText(filePath);
            //var ads = JsonConvert.DeserializeObject<IEnumerable<Ad>>(json);

            if (!_context.Categories.Any())
            {
                var categories = new List<Category>
                {
                    new Category() {IsValid = 1, Name = "cate1", Order = 1},
                    new Category() {IsValid = 1, Name = "cate2", Order = 2},
                    new Category() {IsValid = 1, Name = "cate3", Order = 3},
                    new Category() {IsValid = 1, Name = "cate4", Order = 4},
                    new Category() {IsValid = 1, Name = "cate5", Order = 5},
                    new Category() {IsValid = 1, Name = "cate6", Order = 6},
                    new Category() {IsValid = 1, Name = "cate7", Order = 7},
                    new Category() {IsValid = 1, Name = "cate8", Order = 8}

                };



                _context.Categories.AddRange(categories);
                _context.SaveChanges();
            }

            if (!_context.Ads.Any())
            {

                var ads = new List<Ad>
                {
                    new Ad
                    {
                        Title = "Hot Girl",
                        UserId = "1",
                        Reason = "Meet Man",
                        Details = "I am a passionate girl with alot of appetite for man",
                        AdPostDate = DateTime.Today,
                        ExpiryDate = DateTime.Today.AddDays(5),
                        CategoryId = _context.Categories.FirstOrDefault().Id,
                        Rating = 9,
                        IsActive = true,
                        User = user
                    },
                    new Ad
                    {
                        Title = "Mature Girl",
                        UserId = "2",
                        Reason = "Meet Man",
                        Details = "Mature n",
                        AdPostDate = DateTime.Today,
                        ExpiryDate = DateTime.Today.AddDays(5),
                        CategoryId = _context.Categories.FirstOrDefault().Id,
                        Rating = 9,
                        IsActive = true,
                        User = user
                    },
                    new Ad
                    {
                        Title = "Another Girl",
                        UserId = "1",
                        Reason = "Meet Man",
                        Details = "Another appetite for man",
                        AdPostDate = DateTime.Today,
                        ExpiryDate = DateTime.Today.AddDays(5),
                        CategoryId = _context.Categories.FirstOrDefault().Id,
                        Rating = 9,
                        IsActive = true,
                        User = user
                    },
                    new Ad
                    {
                        Title = "Light bulb needed",
                        UserId = "1",
                        Reason = "Meet Man",
                        Details = "Light bulb r man",
                        AdPostDate = DateTime.Today,
                        ExpiryDate = DateTime.Today.AddDays(5),
                        CategoryId = _context.Categories.FirstOrDefault().Id,
                        Rating = 9,
                        IsActive = true,
                        User = user
                    },
                    new Ad
                    {
                        Title = "Hello from africa",
                        UserId = "1",
                        Reason = "Meet Man",
                        Details = "South africa",
                        AdPostDate = DateTime.Today,
                        ExpiryDate = DateTime.Today.AddDays(5),
                        CategoryId = _context.Categories.FirstOrDefault().Id,
                        Rating = 9,
                        IsActive = true,
                        User = user
                    }
                };

                _context.Ads.AddRange(ads);


                _context.SaveChanges();
            }
           


          
        }


        //public async Task EnsureDataSeed()
        //{


        //if(!_context.Trips.Any())
        //{
        //    var usTrip = new Trip()
        //    {
        //        DateCreated = DateTime.Now,
        //        Name = "US Trip",
        //        UserName = "",
        //        Stops = new List<Category>()
        //        {
        //             new Category() {  Name = "Atlanta, GA", Arrival = new DateTime(2014, 6, 4), Latitude = 33.748995, Longtitude = -84.387982, Order = 0 },
        //            new Category() {  Name = "New York, NY", Arrival = new DateTime(2014, 6, 9), Latitude = 40.712784, Longtitude = -74.005941, Order = 1 },
        //            new Category() {  Name = "Boston, MA", Arrival = new DateTime(2014, 7, 1), Latitude = 42.360082, Longtitude = -71.058880, Order = 2 },
        //            new Category() {  Name = "Chicago, IL", Arrival = new DateTime(2014, 7, 10), Latitude = 41.878114, Longtitude = -87.629798, Order = 3 },
        //            new Category() {  Name = "Seattle, WA", Arrival = new DateTime(2014, 8, 13), Latitude = 47.606209, Longtitude = -122.332071, Order = 4 },
        //            new Category() {  Name = "Atlanta, GA", Arrival = new DateTime(2014, 8, 23), Latitude = 33.748995, Longtitude = -84.387982, Order = 5 },
        //        }
        //    };
        //    _context.Trips.Add(usTrip);

        //    _context.Stops.AddRange(usTrip.Stops);

        //    var worldTrip= new Trip()
        //    {

        //        DateCreated = DateTime.UtcNow,
        //        Name = "World Trip",
        //        UserName = "",
        //        Stops = new List<Category>()
        //        {
        //                new Category() { Order = 0, Latitude =  33.748995, Longtitude =  -84.387982, Name = "Atlanta, Georgia", Arrival = DateTime.Parse("Jun 3, 2014") },
        //                new Category() { Order = 1, Latitude =  48.856614, Longtitude =  2.352222, Name = "Paris, France", Arrival = DateTime.Parse("Jun 4, 2014") },
        //                new Category() { Order = 2, Latitude =  50.850000, Longtitude =  4.350000, Name = "Brussels, Belgium", Arrival = DateTime.Parse("Jun 25, 2014") },
        //                new Category() { Order = 3, Latitude =  51.209348, Longtitude =  3.224700, Name = "Bruges, Belgium", Arrival = DateTime.Parse("Jun 28, 2014") },
        //                new Category() { Order = 4, Latitude =  48.856614, Longtitude =  2.352222, Name = "Paris, France", Arrival = DateTime.Parse("Jun 30, 2014") },
        //                new Category() { Order = 5, Latitude =  51.508515, Longtitude =  -0.125487, Name = "London, UK", Arrival = DateTime.Parse("Jul 8, 2014") },
        //                new Category() { Order = 6, Latitude =  51.454513, Longtitude =  -2.587910, Name = "Bristol, UK", Arrival = DateTime.Parse("Jul 24, 2014") },
        //                new Category() { Order = 7, Latitude =  52.078000, Longtitude =  -2.783000, Name = "Stretton Sugwas, UK", Arrival = DateTime.Parse("Jul 29, 2014") },
        //                new Category() { Order = 8, Latitude =  51.864211, Longtitude =  -2.238034, Name = "Gloucestershire, UK", Arrival = DateTime.Parse("Jul 30, 2014") },
        //                new Category() { Order = 9, Latitude =  52.954783, Longtitude =  -1.158109, Name = "Nottingham, UK", Arrival = DateTime.Parse("Jul 31, 2014") },
        //                new Category() { Order = 10, Latitude =  51.508515, Longtitude =  -0.125487, Name = "London, UK", Arrival = DateTime.Parse("Aug 1, 2014") },
        //                new Category() { Order = 11, Latitude =  55.953252, Longtitude =  -3.188267, Name = "Edinburgh, UK", Arrival = DateTime.Parse("Aug 5, 2014") },
        //                new Category() { Order = 12, Latitude =  55.864237, Longtitude =  -4.251806, Name = "Glasgow, UK", Arrival = DateTime.Parse("Aug 6, 2014") },
        //                new Category() { Order = 13, Latitude =  57.149717, Longtitude =  -2.094278, Name = "Aberdeen, UK", Arrival = DateTime.Parse("Aug 7, 2014") },
        //                new Category() { Order = 14, Latitude =  55.953252, Longtitude =  -3.188267, Name = "Edinburgh, UK", Arrival = DateTime.Parse("Aug 8, 2014") },
        //                new Category() { Order = 15, Latitude =  51.508515, Longtitude =  -0.125487, Name = "London, UK", Arrival = DateTime.Parse("Aug 10, 2014") },
        //                new Category() { Order = 16, Latitude =  52.370216, Longtitude =  4.895168, Name = "Amsterdam, Netherlands", Arrival = DateTime.Parse("Aug 14, 2014") },
        //                new Category() { Order = 17, Latitude =  48.583148, Longtitude =  7.747882, Name = "Strasbourg, France", Arrival = DateTime.Parse("Aug 17, 2014") },
        //                new Category() { Order = 18, Latitude =  46.519962, Longtitude =  6.633597, Name = "Lausanne, Switzerland", Arrival = DateTime.Parse("Aug 19, 2014") },
        //                new Category() { Order = 19, Latitude =  46.021073, Longtitude =  7.747937, Name = "Zermatt, Switzerland", Arrival = DateTime.Parse("Aug 27, 2014") },
        //                new Category() { Order = 20, Latitude =  46.519962, Longtitude =  6.633597, Name = "Lausanne, Switzerland", Arrival = DateTime.Parse("Aug 29, 2014") },
        //                new Category() { Order = 21, Latitude =  53.349805, Longtitude =  -6.260310, Name = "Dublin, Ireland", Arrival = DateTime.Parse("Sep 2, 2014") },
        //                new Category() { Order = 22, Latitude =  54.597285, Longtitude =  -5.930120, Name = "Belfast, Northern Ireland", Arrival = DateTime.Parse("Sep 7, 2014") },
        //                new Category() { Order = 23, Latitude =  53.349805, Longtitude =  -6.260310, Name = "Dublin, Ireland", Arrival = DateTime.Parse("Sep 9, 2014") },
        //                new Category() { Order = 24, Latitude =  47.368650, Longtitude =  8.539183, Name = "Zurich, Switzerland", Arrival = DateTime.Parse("Sep 16, 2014") },
        //                new Category() { Order = 25, Latitude =  48.135125, Longtitude =  11.581981, Name = "Munich, Germany", Arrival = DateTime.Parse("Sep 19, 2014") },
        //                new Category() { Order = 26, Latitude =  50.075538, Longtitude =  14.437800, Name = "Prague, Czech Republic", Arrival = DateTime.Parse("Sep 21, 2014") },
        //                new Category() { Order = 27, Latitude =  51.050409, Longtitude =  13.737262, Name = "Dresden, Germany", Arrival = DateTime.Parse("Oct 1, 2014") },
        //                new Category() { Order = 28, Latitude =  50.075538, Longtitude =  14.437800, Name = "Prague, Czech Republic", Arrival = DateTime.Parse("Oct 4, 2014") },
        //                new Category() { Order = 29, Latitude =  42.650661, Longtitude =  18.094424, Name = "Dubrovnik, Croatia", Arrival = DateTime.Parse("Oct 10, 2014") },
        //                new Category() { Order = 30, Latitude =  42.697708, Longtitude =  23.321868, Name = "Sofia, Bulgaria", Arrival = DateTime.Parse("Oct 16, 2014") },
        //                new Category() { Order = 31, Latitude =  45.658928, Longtitude =  25.539608, Name = "Brosov, Romania", Arrival = DateTime.Parse("Oct 20, 2014") },
        //                new Category() { Order = 32, Latitude =  41.005270, Longtitude =  28.976960, Name = "Istanbul, Turkey", Arrival = DateTime.Parse("Nov 1, 2014") },
        //                new Category() { Order = 33, Latitude =  45.815011, Longtitude =  15.981919, Name = "Zagreb, Croatia", Arrival = DateTime.Parse("Nov 11, 2014") },
        //                new Category() { Order = 34, Latitude =  41.005270, Longtitude =  28.976960, Name = "Istanbul, Turkey", Arrival = DateTime.Parse("Nov 15, 2014") },
        //                new Category() { Order = 35, Latitude =  50.850000, Longtitude =  4.350000, Name = "Brussels, Belgium", Arrival = DateTime.Parse("Nov 25, 2014") },
        //                new Category() { Order = 36, Latitude =  50.937531, Longtitude =  6.960279, Name = "Cologne, Germany", Arrival = DateTime.Parse("Nov 30, 2014") },
        //                new Category() { Order = 37, Latitude =  48.208174, Longtitude =  16.373819, Name = "Vienna, Austria", Arrival = DateTime.Parse("Dec 4, 2014") },
        //                new Category() { Order = 38, Latitude =  47.497912, Longtitude =  19.040235, Name = "Budapest, Hungary", Arrival = DateTime.Parse("Dec 28,2014") },
        //                new Category() { Order = 39, Latitude =  37.983716, Longtitude =  23.729310, Name = "Athens, Greece", Arrival = DateTime.Parse("Jan 2, 2015") },
        //                new Category() { Order = 40, Latitude =  -25.746111, Longtitude =  28.188056, Name = "Pretoria, South Africa", Arrival = DateTime.Parse("Jan 19, 2015") },
        //                new Category() { Order = 41, Latitude =  43.771033, Longtitude =  11.248001, Name = "Florence, Italy", Arrival = DateTime.Parse("Feb 1, 2015") },
        //                new Category() { Order = 42, Latitude =  45.440847, Longtitude =  12.315515, Name = "Venice, Italy", Arrival = DateTime.Parse("Feb 9, 2015") },
        //                new Category() { Order = 43, Latitude =  43.771033, Longtitude =  11.248001, Name = "Florence, Italy", Arrival = DateTime.Parse("Feb 13, 2015") },
        //                new Category() { Order = 44, Latitude =  41.872389, Longtitude =  12.480180, Name = "Rome, Italy", Arrival = DateTime.Parse("Feb 17, 2015") },
        //                new Category() { Order = 45, Latitude =  28.632244, Longtitude =  77.220724, Name = "New Delhi, India", Arrival = DateTime.Parse("Mar 4, 2015") },
        //                new Category() { Order = 46, Latitude =  27.700000, Longtitude =  85.333333, Name = "Kathmandu, Nepal", Arrival = DateTime.Parse("Mar 10, 2015") },
        //                new Category() { Order = 47, Latitude =  28.632244, Longtitude =  77.220724, Name = "New Delhi, India", Arrival = DateTime.Parse("Mar 11, 2015") },
        //                new Category() { Order = 48, Latitude =  22.1667, Longtitude =  113.5500, Name = "Macau", Arrival = DateTime.Parse("Mar 21, 2015") },
        //                new Category() { Order = 49, Latitude =  22.396428, Longtitude =  114.109497, Name = "Hong Kong", Arrival = DateTime.Parse("Mar 24, 2015") },
        //                new Category() { Order = 50, Latitude =  39.904030, Longtitude =  116.407526, Name = "Beijing, China", Arrival = DateTime.Parse("Apr 19, 2015") },
        //                new Category() { Order = 51, Latitude =  22.396428, Longtitude =  114.109497, Name = "Hong Kong", Arrival = DateTime.Parse("Apr 24, 2015") },
        //                new Category() { Order = 52, Latitude =  1.352083, Longtitude =  103.819836, Name = "Singapore", Arrival = DateTime.Parse("Apr 30, 2015") },
        //                new Category() { Order = 53, Latitude =  3.139003, Longtitude =  101.686855, Name = "Kuala Lumpor, Malaysia", Arrival = DateTime.Parse("May 7, 2015") },
        //                new Category() { Order = 54, Latitude =  13.727896, Longtitude =  100.524123, Name = "Bangkok, Thailand", Arrival = DateTime.Parse("May 24, 2015") },
        //                new Category() { Order = 55, Latitude =  33.748995, Longtitude =  -84.387982, Name = "Atlanta, Georgia", Arrival = DateTime.Parse("Jun 17, 2015") },

        //        }
        //    };


        //    _context.Trips.Add(worldTrip);

        //    _context.Stops.AddRange(worldTrip.Stops);

        //    await _context.SaveChangesAsync();

        //}
    }
}

