using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.interfaces;
using AutoMapper;
using MediatR;
using Web.Dtos;

namespace Web.Features.HeroFeature.Queries
{
    /// <summary>
    /// Yes. This is only it. =P
    /// </summary>
    public class GetAllHeroesQuery : IRequest<IEnumerable<HeroDto>> { }

    public class GetAllHeroesHandler : IRequestHandler<GetAllHeroesQuery, IEnumerable<HeroDto>>
    {
        private readonly IHeroRepository _heroRepository;
        private readonly IMapper _mapper;

        public GetAllHeroesHandler(IHeroRepository heroRepository, IMapper mapper)
        {
            _heroRepository = heroRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<HeroDto>> Handle(GetAllHeroesQuery request, CancellationToken cancellationToken)
        {
            var heroes = await _heroRepository.GetAllHeroesAsync();

            return _mapper.Map<IEnumerable<HeroDto>>(heroes);
        }
    }
}