using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WhiteApp.Api.Model;

namespace WhiteApp.Api.DataSeeding
{
    public static class ContextSeed
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            CreateCustomer(modelBuilder);
        }

        private static void CreateCustomer(ModelBuilder modelBuilder)
        {
            List<Customer> customers = DefaultCustomer.CustomerList();
            modelBuilder.Entity<Customer>().HasData(customers);
        }

    }
}
