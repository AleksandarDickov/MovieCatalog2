using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCatalog
{
    public class MovieContext : DbContext
    {
        public MovieContext() : base()
        {
            //Database.SetInitializer<MovieContext>(new DropCreateDatabaseIfModelChanges<MovieContext>());
        }

        public DbSet<MovieName> Movies { get; set; }
    }
}
