namespace CompanyManagementSystem.Web.Mappings.Contracts
{
    using AutoMapper;

    public interface IHaveCustomMappings
    {
        void CreateMappings(IMapperConfigurationExpression configuration);
    }
}
