using Application.Features.ProgrammingLanguages.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.ProgrammingLanguages.Queries.GetListProgrammingLanguage
{
    public class GetListProgrammingLanguageQuery : IRequest<ProgrammingLanguageListModel>
    {

        public PageRequest PageRequest { get; set; }
        public class GetListProgrammingLanguageQueryHandler : IRequestHandler<GetListProgrammingLanguageQuery, ProgrammingLanguageListModel>
        {
            IProgrammingLanguageRepository _ProgrammingLanguageRepository;
            IMapper _mapper;

            public GetListProgrammingLanguageQueryHandler(IProgrammingLanguageRepository ProgrammingLanguageRepository, IMapper mapper)
            {
                _ProgrammingLanguageRepository = ProgrammingLanguageRepository;
                _mapper = mapper;
            }

            public async Task<ProgrammingLanguageListModel> Handle(GetListProgrammingLanguageQuery request, CancellationToken cancellationToken)
            {
                IPaginate<ProgrammingLanguage> ProgrammingLanguages = await _ProgrammingLanguageRepository.GetListAsync(index: request.PageRequest.Page, size: request.PageRequest.PageSize);

                ProgrammingLanguageListModel mappedProgrammingLanguageListModel = _mapper.Map<ProgrammingLanguageListModel>(ProgrammingLanguages);

                return mappedProgrammingLanguageListModel;
            }
        }
    }
}
