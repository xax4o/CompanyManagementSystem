namespace CompanyManagementSystem.Services
{
    using AutoMapper;

    using CompanyManagementSystem.Services.Contracts;
    using CompanyManagementSystem.Web.Mappings;

    public class MappingService : IMappingService
    {
        private readonly IMapper mapper;

        public MappingService()
        {
            this.mapper = AutoMapperConfigurations.Configuration.CreateMapper();
        }

        public T Map<T>(object source)
        {
            return mapper.Map<T>(source);
        }

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            return mapper.Map<TSource, TDestination>(source, destination);
        }
    }
}
