using AutoMapper;
using DoctoLab.DTOs;
using DoctoLab.GTOs;
using DoctoLab.Models;

namespace DoctoLab.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Doctor, DoctorGetDto>();
            CreateMap<DoctorCreateDto, Doctor>();

            CreateMap<Patient, PatientGetDto>();
            CreateMap<PatientCreateDto, Patient>();

            CreateMap<Hospital, HospitalGetDto>();
            CreateMap<Field, FieldGetDto>();
        }
    }
}
