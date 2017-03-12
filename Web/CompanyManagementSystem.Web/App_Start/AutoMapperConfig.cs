namespace CompanyManagementSystem.Web
{
    using System.Reflection;

    using CompanyManagementSystem.Mappings;
    using CompanyManagementSystem.Web.Common;

    public static class AutoMapperConfig
    {
        public static void RegisterAllMappings()
        {
            var mapper = new AutoMapperConfigurations();
            mapper.Execute(Assembly.Load(Constants.DataTransferModelsAssemblyPath));
        }
    }
}