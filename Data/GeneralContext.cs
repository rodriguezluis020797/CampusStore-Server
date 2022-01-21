using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace Server.Data {
    public class GeneralContext : DbContext{
        public DbSet<UserModel> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        { 
                options.UseSqlServer(@"Server=tcp:campus-store.database.windows.net,1433;Initial Catalog=campus-store-general;Persist Security Info=False;User ID=luis-admin;Password=tuVvuwfejda2tyvcah;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

        }

    }
}

