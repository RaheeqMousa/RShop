using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

using Microsoft.EntityFrameworkCore;
using RShop.DAL.Data;
using RShop.DAL.DTO.Requests;
using RShop.DAL.Models;

namespace RShop.DAL.Utils
{
    public class SeedData:ISeedData
    {
        private readonly ApplicationDBContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public SeedData(ApplicationDBContext context,
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager
            )
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task DataSeedingAsync()
        {
            var pendingMigrations = await _context.Database.GetPendingMigrationsAsync();
            if (pendingMigrations.Any())
            {
                await _context.Database.MigrateAsync();
            }

            if (!await _context.Categories.AnyAsync())
            {
                await _context.Categories.AddRangeAsync(
                    new Models.Category { Name = "Shirts" },
                    new Models.Category { Name = "Mobiles" }
                );
            }

            if (!await _context.Brands.AnyAsync())
            {
                await _context.Brands.AddRangeAsync(
                    new Models.Brand { Name = "Samsung" },
                    new Models.Brand { Name = "Apple" },
                    new Models.Brand { Name = "Lacoste" }
                );
            }

            await _context.SaveChangesAsync();
        }

        public async Task IdentityDataSeeding()
        {
            if (!await _roleManager.Roles.AnyAsync()) {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
                await _roleManager.CreateAsync(new IdentityRole("Manager"));
                await _roleManager.CreateAsync(new IdentityRole("Customer"));
            }

            if (!await _context.Users.AnyAsync()) {
                var user1=new ApplicationUser() { 
                    Email="raheeqmousa99@gmail.com",
                    UserName = "Raheeq",
                    FullName ="RaheeqMousa",
                    PhoneNumber="972598411518",
                    EmailConfirmed = true
                };
                var user2 = new ApplicationUser()
                {
                    Email = "adnanmousa99@gmail.com",
                    UserName = "Adnan",
                    FullName = "AdnanMousa",
                    PhoneNumber = "123456789",
                    EmailConfirmed = true
                };

                var user3 = new ApplicationUser()
                {
                    Email = "mohammedmousa99@gmail.com",
                    UserName = "MohammedMousa",
                    FullName = "MohammedMousa",
                    PhoneNumber = "123456789",
                    EmailConfirmed = true
                };

                await _userManager.CreateAsync(user1, "Raheeq@1");
                await _userManager.CreateAsync(user2, "Adnan@123");
                await _userManager.CreateAsync(user3, "Mohammed@123");

                await _userManager.AddToRoleAsync(user2, "Admin");
                await _userManager.AddToRoleAsync(user3, "Manager");
                await _userManager.AddToRoleAsync(user1, "Customer");
            }

            await _context.SaveChangesAsync();
        }
    }
}