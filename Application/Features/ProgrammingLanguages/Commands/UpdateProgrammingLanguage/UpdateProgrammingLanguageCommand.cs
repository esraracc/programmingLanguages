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

namespace Application.Features.ProgrammingLanguages.Commands.UpdateProgrammingLanguage
{
    public partial class UpdateProgrammingLanguageCommand : IRequest<UpdatedProgrammingLanguageDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public class UpdateProgrammingLanguageCommandHandler : IRequestHandler<UpdateProgrammingLanguageCommand, UpdatedProgrammingLanguageDto>
        {
            private readonly IProgrammingLanguageRepository _ProgrammingLanguageRepository;
            private readonly IMapper _mapper;
            private readonly ProgrammingLanguageBusinessRules _ProgrammingLanguageBusinessRules;

            public UpdateProgrammingLanguageCommandHandler(IProgrammingLanguageRepository ProgrammingLanguageRepository, IMapper mapper, ProgrammingLanguageBusinessRules ProgrammingLanguageBusinessRules)
            {
                _ProgrammingLanguageRepository = ProgrammingLanguageRepository;
                _mapper = mapper;
                _ProgrammingLanguageBusinessRules = ProgrammingLanguageBusinessRules;
            }

            public async Task<UpdatedProgrammingLanguageDto> Handle(UpdateProgrammingLanguageCommand request, CancellationToken cancellationToken)
            {
                await _ProgrammingLanguageBusinessRules.ProgrammingLanguageNameCanNotBeDuplicatedWhenInserted(request.Name);
                await _ProgrammingLanguageBusinessRules.ProgrammingLanguageNameCanNotEmpty(request.Name);

                ProgrammingLanguage mappedProgrammingLanguage = _mapper.Map<ProgrammingLanguage>(request);
                ProgrammingLanguage UpdatedProgrammingLanguage = await _ProgrammingLanguageRepository.UpdateAsync(mappedProgrammingLanguage);
                UpdatedProgrammingLanguageDto UpdatedProgrammingLanguageDto = _mapper.Map<UpdatedProgrammingLanguageDto>(UpdatedProgrammingLanguage);

                return UpdatedProgrammingLanguageDto;

            }
        }
    }
}
