namespace CompanyManagementSystem.Web
{
    using System.Reflection;

    using CompanyManagementSystem.Web.Mappings;

    public static class AutoMapperConfig
    {
        public static void RegisterAllMappings()
        {
            var mapper = new AutoMapperConfigurations();
            mapper.Execute(Assembly.GetExecutingAssembly());
        }
    }
}