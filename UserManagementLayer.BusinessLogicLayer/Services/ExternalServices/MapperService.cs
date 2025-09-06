using UserManagementService.BusinessLogicLayer.Interfaces.Infrastructure;

namespace UserManagementService.BusinessLogicLayer.Services.ExternalServices
{
    public class MapperService : IMapper
    {
        private readonly IMapper _mapper;

        public MapperService(TypeAdapterConfig config)
        {
            _mapper = new MapperService(config);
        }

        public TDestination Map<TDestination>(object source)
            => _mapper.Map<TDestination>(source);

        public TDestination Map<TSource, TDestination>(TSource source)
            => _mapper.Map<TSource, TDestination>(source);

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
            => _mapper.Map(source, destination);

        public IQueryable<TDestination> ProjectTo<TDestination>(IQueryable source)
            => _mapper.ProjectTo<TDestination>(source);
    }
}
