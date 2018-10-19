namespace Project1.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Kiosco : DbContext
    {
        public string SchemaName { get; private set; }
        public DbSet<Fabrica> Fabricas { get; set; }
        public DbSet<Golosina> Golosinas { get; set; }
        public static Kiosco Create(/*string databaseServer, string databaseName, string databaseUserName, string databasePassword,*/ string tenantId)
        {
            var connectionStringBuilder = new System.Data.SqlClient.SqlConnectionStringBuilder();
            connectionStringBuilder.DataSource = "LAPTOP-ND2LJQD5";
            connectionStringBuilder.InitialCatalog = "PruebaMultiTenantDB";
         //   connectionStringBuilder.UserID = "emimartins";
         //   connectionStringBuilder.Password = databasePassword;

            string connectionString = connectionStringBuilder.ToString();
            return new Kiosco("data source = LAPTOP-ND2LJQD5; initial catalog = PruebaMultiTenantDB; integrated security = True; MultipleActiveResultSets = True; App = EntityFramework", tenantId);
        }
        public Kiosco()
        {
            Database.SetInitializer<Kiosco>(null);
        }

        internal Kiosco(string connectionString, string tenantId)
            : base(connectionString)
        {
            Database.SetInitializer<Kiosco>(null);
            this.SchemaName = tenantId /*tenantId.ToString("D")*/;
        }
 
        /*public Kiosco()
            : base("name=KioscoConnection")
        {
           // this.Configuration.LazyLoadingEnabled = false;

        }*/


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            if (this.SchemaName != null)
            {
                modelBuilder.HasDefaultSchema(this.SchemaName);
            }

            base.OnModelCreating(modelBuilder);
        }
        public string CacheKey
        {
            get { return this.SchemaName; }
        }
    }
}
