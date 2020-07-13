using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.interfaces;
using AutoMapper;
using MediatR;
using Web.Dtos;

namespace Web.Features.HeroFeature.Commands
{
    public class CreateHeroCommand : IRequest<HeroDto>
    {
        public HeroDto NewHero { get; set; }
    }

    public class CreateHeroHandler : IRequestHandler<CreateHeroCommand, HeroDto>
    {
        private readonly IHeroRepository _heroRepository;
        private readonly IMapper _mapper;

        public CreateHeroHandler(IHeroRepository heroRepository, IMapper mapper)
        {
            _heroRepository = heroRepository;
            _mapper = mapper;
        }

        public async Task<HeroDto> Handle(CreateHeroCommand request, CancellationToken cancellationToken)
        {
            var hero = _mapper.Map<Hero>(request.NewHero);
            var createdHero = await _heroRepository.CreateHeroAsync(hero);

            return _mapper.Map<HeroDto>(createdHero);
        }
    }
}