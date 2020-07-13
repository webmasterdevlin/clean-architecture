using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.interfaces;
using AutoMapper;
using MediatR;
using Web.Dtos;

namespace Web.Features.HeroFeature.Commands
{
    public class DeleteHeroByIdCommand : IRequest
    {
        public string HeroId { get; set; }
    }

    public class DeleteHeroByIdHandler : IRequestHandler<DeleteHeroByIdCommand>
    {
        private readonly IHeroRepository _heroRepository;

        public DeleteHeroByIdHandler(IHeroRepository heroRepository)
        {
            _heroRepository = heroRepository;
        }

        public async Task<Unit> Handle(DeleteHeroByIdCommand request, CancellationToken cancellationToken)
        {
            await _heroRepository.DeleteHeroAsync(request.HeroId);

            return Unit.Value;
        }
    }
}