using Project1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using TenantDatabaseManager;
using System.Data.Entity.Migrations.History;

namespace Api.Controllers
{
    public class KioscoController : ApiController
    {
        [HttpGet]
        [Route("api/fabrica")]
        public ICollection <Fabrica> getFabricas()
        {
            using(var context = new Kiosco())
            {
                var res= context.Fabricas.Include(x => x.golosinas).ToList();
                return res;
            }
        }
        [HttpPost]
        [Route("api/fabrica")]
        public void addFabrica(Fabrica f)
        {
            using(var context =new Kiosco())
            {
                context.Fabricas.Add(f);
                context.SaveChanges();
            }
        }
        [HttpGet]
        [Route("api/golosina")]
        public ICollection<Golosina> getGolosinas()
        {
            using (var context = new Kiosco())
            {
                return context.Golosinas.ToList();
            }
        }
        [HttpPost]
        [Route("api/golosina")]
        public void addGolosinas(Golosina f)
        {
            using (var context = Kiosco.Create("nuevoSchemas"))
            {
               // context.Connection.Open();
                context.Golosinas.Add(f);
                context.SaveChanges();
            }
        }
        [HttpGet]
        [Route("api/tenant")]
        public Boolean crearTenant()
        {
            var tenantDataMigrationsConfiguration = new DbMigrationsConfiguration<Kiosco>();
            tenantDataMigrationsConfiguration.AutomaticMigrationsEnabled = false;
            tenantDataMigrationsConfiguration.SetSqlGenerator("System.Data.SqlClient", new SqlServerSchemaAwareMigrationSqlGenerator("nuevoSchemas"));
            tenantDataMigrationsConfiguration.SetHistoryContextFactory("System.Data.SqlClient", (existingConnection, defaultSchema) => new HistoryContext(existingConnection, "nuevoSchemas"));
            tenantDataMigrationsConfiguration.TargetDatabase = new System.Data.Entity.Infrastructure.DbConnectionInfo("data source=LAPTOP-ND2LJQD5;initial catalog=PruebaMultiTenantDB;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework", "System.Data.SqlClient");
            tenantDataMigrationsConfiguration.MigrationsAssembly = typeof(Kiosco).Assembly;
            tenantDataMigrationsConfiguration.MigrationsNamespace = "AccesoADatos.Migrations.TenantData";

            DbMigrator tenantDataCtxMigrator = new DbMigrator(tenantDataMigrationsConfiguration);
            tenantDataCtxMigrator.Update();
            return true;
        }
    }
}
