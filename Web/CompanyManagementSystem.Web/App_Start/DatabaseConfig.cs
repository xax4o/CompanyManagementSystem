namespace CompanyManagementSystem.Web
{
    using System.Data.Entity;

    using CompanyManagementSystem.Data;
    using CompanyManagementSystem.Data.Migrations;

    public static class DatabaseConfig
    {
        public static void RegisterMigrations()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<CompanyManagementDbContext, Configuration>());
        }
    }
}