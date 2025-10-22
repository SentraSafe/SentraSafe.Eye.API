using AutoMapper;
using EYEAPI.Models.Dtos.LocationDtos;
using EYEAPI.Models.Dtos.SublocationDtos;
using EYEAPI.Models.Entities;
using EYEAPI.Repositories;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace EYEAPI.Services.SublocationService
{
    public class SublocationService(IEyeRepository eyeRepository, IMapper mapper) : ISublocationService
    {
        public async Task<List<SublocationDto>> GetSublocationByLocationAsync(int locationId) =>
            mapper.Map<List<SublocationDto>>(await eyeRepository.GetSublocationsByLocationAsync(locationId));

        public async Task<Sublocation> AddSublocationAsync(CreateSublocationDto createSublocation)
        {
            Sublocation sublocation = mapper.Map<Sublocation>(createSublocation);
            await eyeRepository.AddSublocationAsync(sublocation);
            return mapper.Map<Sublocation>(await eyeRepository.GetSublocationByIdAsync(sublocation.Id));
        }
        
        public async Task AddSublocationsAsync(List<CreateSublocationDto> createSublocations)
        {
            List<Sublocation> sublocations = mapper.Map<List<Sublocation>>(createSublocations);
            await eyeRepository.AddSublocationsAsync(sublocations);
        }

        public async Task DeleteSublocationByIdAsync(int sublocationId) => await eyeRepository.DeleteSublocationByIdAsync(sublocationId);
        public async Task<Sublocation> UpdateSublocationAsync(SublocationDto sublocation)
        {
            Sublocation mappedSublocation = mapper.Map<Sublocation>(sublocation);
            await eyeRepository.UpdateSublocationAsync(mappedSublocation);
            return await eyeRepository.GetSublocationByIdAsync(mappedSublocation.Id);
        }
    }
}
