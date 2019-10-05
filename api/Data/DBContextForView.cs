using Microsoft.EntityFrameworkCore;

namespace Sample.Data
{
    public partial class DBContextForView : DbContext
    {

        public DBContextForView()
        {
        }

        public DBContextForView(DbContextOptions<DBContextForView> options)
            : base(options)
        {
        }

        // public virtual DbQuery<view_node_relationship> view_node_relationship { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");
            // modelBuilder.Query<view_node_relationship>().ToView("view_node_relationship");
        }
    }
}
