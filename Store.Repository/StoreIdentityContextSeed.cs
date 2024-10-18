using Microsoft.AspNetCore.Identity;
using Store.Data.Entities.IdentityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository
{
    public class StoreIdentityContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "Ahmed Emad",
                    Email = "ahmedemad10@gmail.com",
                    UserName = "ahmedEmad",
                    Address = new Address
                    {
                        FirstName = "Ahmed",
                        LastName = "Emad",
                        City = "Bab El Sharia",
                        State = "Cairo",
                        Street = "12",
                        PostalCode = "12345"
                    }
                };

                await userManager.CreateAsync(user, "YourSecurePasswordHere");
            }
        }

    }
}
