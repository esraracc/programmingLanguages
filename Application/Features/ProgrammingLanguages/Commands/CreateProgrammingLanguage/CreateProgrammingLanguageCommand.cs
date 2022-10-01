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

namespace Application.Features.ProgrammingLanguages.Commands.CreateProgrammingLanguage
{
    public partial class CreateProgrammingLanguageCommand : IRequest<CreatedProgrammingLanguageDto>
    {
        public string Name { get; set; }

        public class CreateProgrammingLanguageCommandHandler : IRequestHandler<CreateProgrammingLanguageCommand, CreatedProgrammingLanguageDto>
        {
            private readonly IProgrammingLanguageRepository _ProgrammingLanguageRepository;
            private readonly IMapper _mapper;
            private readonly ProgrammingLanguageBusinessRules _ProgrammingLanguageBusinessRules;

            public CreateProgrammingLanguageCommandHandler(IProgrammingLanguageRepository ProgrammingLanguageRepository, IMapper mapper, ProgrammingLanguageBusinessRules ProgrammingLanguageBusinessRules)
            {
                _ProgrammingLanguageRepository = ProgrammingLanguageRepository;
                _mapper = mapper;
                _ProgrammingLanguageBusinessRules = ProgrammingLanguageBusinessRules;
            }

            public async Task<CreatedProgrammingLanguageDto> Handle(CreateProgrammingLanguageCommand request, CancellationToken cancellationToken)
            {
                await _ProgrammingLanguageBusinessRules.ProgrammingLanguageNameCanNotBeDuplicatedWhenInserted(request.Name);
                await _ProgrammingLanguageBusinessRules.ProgrammingLanguageNameCanNotEmpty(request.Name);

                ProgrammingLanguage mappedProgrammingLanguage = _mapper.Map<ProgrammingLanguage>(request);
                ProgrammingLanguage createdProgrammingLanguage = await _ProgrammingLanguageRepository.AddAsync(mappedProgrammingLanguage);
                CreatedProgrammingLanguageDto createdProgrammingLanguageDto = _mapper.Map<CreatedProgrammingLanguageDto>(createdProgrammingLanguage);

                return createdProgrammingLanguageDto;

            }
        }
    }
}
