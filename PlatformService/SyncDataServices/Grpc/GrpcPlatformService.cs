using AutoMapper;
using Grpc.Core;
using PlatformService.Data;
using static PlatformService.GrpcPlatform;

namespace PlatformService.SyncDataServices.Grpc
{
    public class GrpcPlatformService : GrpcPlatformBase
    {
        private readonly IPlatformRepo _repository;
        private readonly IMapper _mapper;

        public GrpcPlatformService(IPlatformRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public override Task<PlatformResponse> GetAllPlatforms(GetAllRequest request, ServerCallContext context)
        {
            var response = new PlatformResponse();
            var platforms = _repository.GetAllPlatforms();

            foreach (var plat in platforms)
            {
                Console.WriteLine($"--> Adding Platform Id: {plat.Id}");
                response.Platform.Add(_mapper.Map<GrpcPlatformsModel>(plat));
            }

            return Task.FromResult(response);
        }
    }
}