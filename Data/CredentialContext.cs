using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace Server.Data
{

    public class CredentialContext : DbContext
    {
        public DbSet<UserPasswordHash> UserPasswordHashes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        { 
                options.UseSqlServer(@"Server=tcp:campus-store-credentials.database.windows.net,1433;Initial Catalog=campus-store-credentials;Persist Security Info=False;User ID=luis-admin;Password=vedmyrwykru1ziCjaz;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

        }
    }
}