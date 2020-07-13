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
    public class UpdateHeroCommand : IRequest
    {
        public HeroDto HeroDto { get; set; }
    }

    public class UpdateHeroHandler : IRequestHandler<UpdateHeroCommand>
    {
        private readonly IHeroRepository _heroRepository;
        private readonly IMapper _mapper;

        public UpdateHeroHandler(IHeroRepository heroRepository, IMapper mapper)
        {
            _heroRepository = heroRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateHeroCommand request, CancellationToken cancellationToken)
        {
            var hero = _mapper.Map<Hero>(request.HeroDto);
            await _heroRepository.UpdateHeroAsync(hero);

            return Unit.Value;
        }
    }
}