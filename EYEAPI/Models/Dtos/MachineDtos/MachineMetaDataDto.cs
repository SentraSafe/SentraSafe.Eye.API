using AutoMapper;
using EYEAPI.Models.Entities;

namespace EYEAPI.Models.Dtos.MachineDtos;

[AutoMap(typeof(MachineMetaData))]
public class MachineMetaDataDto
{
    public int? TotalStorage { get; set; }
    public int? TotalMemory { get; set; }
}