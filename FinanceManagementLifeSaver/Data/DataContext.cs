using FinanceManagementLifesaver.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceManagementLifesaver.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Transaction>();
        //}
        public DataContext() { }
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        //public DbSet<Contact> Contacts { get; set; }
        public DbSet<ToDo> ToDos { get; set; }
        public DbSet<Scope> Scopes { get; set; }
        public DbSet<Guest> Guests { get; set; }
    }
}
