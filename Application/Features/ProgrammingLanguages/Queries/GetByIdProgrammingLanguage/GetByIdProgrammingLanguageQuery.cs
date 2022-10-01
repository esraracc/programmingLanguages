using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.ProgrammingLanguages.Queries.GetByIdProgrammingLanguage
{
    public class GetByIdProgrammingLanguageQuery : IRequest<ProgrammingLanguageGetByIdDto>
    {
        public int Id { get; set; }
        public class GetByIdProgrammingLanguageQueryHandler : IRequestHandler<GetByIdProgrammingLanguageQuery, ProgrammingLanguageGetByIdDto>
        {
            private readonly IProgrammingLanguageRepository _ProgrammingLanguageRepository;
            private readonly IMapper _mapper;
            private readonly ProgrammingLanguageBusinessRules _ProgrammingLanguageBusinessRules;

            public GetByIdProgrammingLanguageQueryHandler(IProgrammingLanguageRepository ProgrammingLanguageRepository, IMapper mapper, ProgrammingLanguageBusinessRules ProgrammingLanguageBusinessRules)
            {
                _ProgrammingLanguageRepository = ProgrammingLanguageRepository;
                _mapper = mapper;
                _ProgrammingLanguageBusinessRules = ProgrammingLanguageBusinessRules;
            }

            public async Task<ProgrammingLanguageGetByIdDto> Handle(GetByIdProgrammingLanguageQuery request, CancellationToken cancellationToken)
            {
                ProgrammingLanguage ProgrammingLanguage = await _ProgrammingLanguageRepository.GetAsync(x => x.Id == request.Id);

                _ProgrammingLanguageBusinessRules.ProgrammingLanguageShouldExistWhenRequested(ProgrammingLanguage);

                ProgrammingLanguageGetByIdDto ProgrammingLanguageGetByIdDto = _mapper.Map<ProgrammingLanguageGetByIdDto>(ProgrammingLanguage);
                return ProgrammingLanguageGetByIdDto;
            }
        }
    }
}
