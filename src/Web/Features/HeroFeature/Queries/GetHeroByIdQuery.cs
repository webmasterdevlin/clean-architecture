using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.interfaces;
using AutoMapper;
using MediatR;
using Web.Dtos;

namespace Web.Features.HeroFeature.Queries
{
    public class GetHeroByIdQuery : IRequest<HeroDto>
    {
        public string HeroId { get; set; }
    }

    public class GetHeroByIdHandler : IRequestHandler<GetHeroByIdQuery, HeroDto>
    {
        private readonly IHeroRepository _heroRepository;
        private readonly IMapper _mapper;

        public GetHeroByIdHandler(IHeroRepository heroRepository, IMapper mapper)
        {
            _heroRepository = heroRepository;
            _mapper = mapper;
        }

        public async Task<HeroDto> Handle(GetHeroByIdQuery request, CancellationToken cancellationToken)
        {
            var hero = await _heroRepository.GetHeroByIdAsync(request.HeroId);

            return _mapper.Map<HeroDto>(hero);
        }
    }
}