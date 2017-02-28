namespace CompanyManagementSystem.Web
{
    using System.Reflection;

    using CompanyManagementSysytem.Web.Mappings;

    public static class AutoMapperConfig
    {
        public static void RegisterAllMappings()
        {
            var mapper = new AutoMapperConfigurations();
            mapper.Execute(Assembly.GetExecutingAssembly());
        }
    }
}