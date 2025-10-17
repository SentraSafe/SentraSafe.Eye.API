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
        public async Task<List<Sublocation>> GetSublocationByLocationAsync(int locationId) => 
            mapper.Map<List<Sublocation>>(await eyeRepository.GetSublocationsByLocationAsync(locationId));

        public async Task<Sublocation> AddSublocationAsync(CreateSublocationDto createSublocation)
        {
            Sublocation newSublocation = await eyeRepository.AddSublocationAsync(mapper.Map<Sublocation>(createSublocation));
            return mapper.Map<Sublocation>(newSublocation);
        }

        public async Task DeleteSublocationByIdAsync(int sublocationId) => await eyeRepository.DeleteSublocationByIdAsync(sublocationId);
        public async Task<Sublocation> UpdateSublocationAsync(SublocationDto sublocation) => await eyeRepository.UpdateSublocationAsync(mapper.Map<Sublocation>(sublocation));
    }
}
