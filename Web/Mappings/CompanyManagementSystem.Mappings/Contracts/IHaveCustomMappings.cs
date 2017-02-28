namespace CompanyManagementSysytem.Web.Mappings.Contracts
{
    using AutoMapper;

    public interface IHaveCustomMappings
    {
        void CreateMappings(IMapperConfigurationExpression configuration);
    }
}
