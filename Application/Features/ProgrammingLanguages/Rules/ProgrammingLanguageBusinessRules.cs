using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgrammingLanguages.Rules
{
    public class ProgrammingLanguageBusinessRules
    {
        private readonly IProgrammingLanguageRepository _programmingLanguageRepository;

        public ProgrammingLanguageBusinessRules(IProgrammingLanguageRepository programmingLanguageRepository)
        {
            _programmingLanguageRepository = programmingLanguageRepository;
        }

        public async Task ProgrammingLanguageNameCanNotBeDuplicatedWhenInserted(string name)
        {
            IPaginate<ProgrammingLanguage> result = await _programmingLanguageRepository.GetListAsync(x => x.Name == name);
            if (result.Items.Any()) throw new BusinessException("ProgrammingLanguage name exists.");
        }

        public void ProgrammingLanguageShouldExistWhenRequested(ProgrammingLanguage ProgrammingLanguage)
        {
            if (ProgrammingLanguage == null) throw new BusinessException("Requested ProgrammingLanguage does not exist");
        }

        public async Task ProgrammingLanguageNameCanNotEmpty(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new BusinessException("ProgrammingLanguage name can not empty.");
        }

        public async Task ProgrammingLanguageIdCanNotEmpty(int id)
        {
            if (id == 0 || id == null) throw new BusinessException("ProgrammingLanguage id can not empty.");
        }

        public async Task ProgrammingLanguageIdIsExists(int id)
        {
            ProgrammingLanguage result = await _programmingLanguageRepository.GetAsync(x => x.Id == id);
            if (result == null) throw new BusinessException($"ProgrammingLanguage {id} id data not found!");
        }
    }
}
