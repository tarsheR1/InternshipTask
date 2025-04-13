using Mapster;
using UserManagementService.BusinessLogicLayer.Interfaces.Infrastructure;

namespace UserManagementService.BusinessLogicLayer.Services.ExternalServices
{
    public class MapperService : IMapper
    {

        private readonly MapsterMapper.IMapper _mapster;

        public MapperService(MapsterMapper.IMapper mapster)
        {
            _mapster = mapster;
        }

        public TDestination Map<TDestination>(object source)
            => _mapster.Map<TDestination>(source);

        public TDestination Map<TSource, TDestination>(TSource source)
            => _mapster.Map<TSource, TDestination>(source);

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
            => _mapster.Map(source, destination);
    }
}
