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

namespace Application.Features.ProgrammingLanguages.Commands.DeleteProgrammingLanguage
{
    public partial class DeleteProgrammingLanguageCommand : IRequest<DeletedProgrammingLanguageDto>
    {
        public int Id { get; set; }

        public class DeleteProgrammingLanguageCommandHandler : IRequestHandler<DeleteProgrammingLanguageCommand, DeletedProgrammingLanguageDto>
        {
            private readonly IProgrammingLanguageRepository _programmingLanguageRepository;
            private readonly IMapper _mapper;
            private readonly ProgrammingLanguageBusinessRules _ProgrammingLanguageBusinessRules;

            public DeleteProgrammingLanguageCommandHandler(IProgrammingLanguageRepository ProgrammingLanguageRepository, IMapper mapper, ProgrammingLanguageBusinessRules ProgrammingLanguageBusinessRules)
            {
                _programmingLanguageRepository = ProgrammingLanguageRepository;
                _mapper = mapper;
                _ProgrammingLanguageBusinessRules = ProgrammingLanguageBusinessRules;
            }

            public async Task<DeletedProgrammingLanguageDto> Handle(DeleteProgrammingLanguageCommand request, CancellationToken cancellationToken)
            {
                await _ProgrammingLanguageBusinessRules.ProgrammingLanguageIdCanNotEmpty(request.Id);
                await _ProgrammingLanguageBusinessRules.ProgrammingLanguageIdIsExists(request.Id);

                ProgrammingLanguage ProgrammingLanguage = await _programmingLanguageRepository.GetAsync(x => x.Id == request.Id);//_mapper.Map<ProgrammingLanguage>(request);
                Console.WriteLine(" id  : " + ProgrammingLanguage.Id + "  name :  " + ProgrammingLanguage.Name);
                ProgrammingLanguage deleteProgrammingLanguage = await _programmingLanguageRepository.DeleteAsync(ProgrammingLanguage);
                DeletedProgrammingLanguageDto ProgrammingLanguageGetByIdDto = _mapper.Map<DeletedProgrammingLanguageDto>(deleteProgrammingLanguage);
                return ProgrammingLanguageGetByIdDto;
            }
        }
    }
}
