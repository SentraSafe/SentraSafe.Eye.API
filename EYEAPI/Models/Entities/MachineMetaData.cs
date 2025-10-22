using System.ComponentModel.DataAnnotations.Schema;
using AutoMapper;
using EYEAPI.Models.Dtos.MachineDtos;

namespace EYEAPI.Models.Entities;

[AutoMap(typeof(MachineMetaDataDto))]
public class MachineMetaData
{
    public int Id { get; set; }
    public int MachineId { get; set; }
    [ForeignKey(nameof(MachineId))]
    public Machine Machine { get; set; }
    public int? TotalStorage { get; set; }
    public int? TotalMemory { get; set; }
}