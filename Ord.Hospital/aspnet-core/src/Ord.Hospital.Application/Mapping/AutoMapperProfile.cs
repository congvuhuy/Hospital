using AutoMapper;
using Ord.Hospital.Communes.Dtos;
using Ord.Hospital.Districts.Dtos;
using Ord.Hospital.Enities;
using Ord.Hospital.HospitalOrd.Dtos;
using Ord.Hospital.Patients.Dtos;
using Ord.Hospital.Provinces.Dtos;
using Ord.Hospital.UserHospitals.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ord.Hospital.Mapping
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            //province
            CreateMap<ProvinceDto, Province>();
            CreateMap<Province, ProvinceDto>();

            CreateMap<CreateUpdateProvinceDto, Province>();


            //District
            CreateMap<DistrictDto, District>();
            CreateMap<District, DistrictDto>();

            CreateMap<CreateUpdateDistrictDto, District>();


            //Commune
            CreateMap<CommuneDto, Commune>();
            CreateMap<Commune, CommuneDto>();
            CreateMap<CreateUpdateCommuneDto, Commune>();

            //Hospital
            CreateMap<CreateUpdateHospitalDto, Hospitals>();
            CreateMap<Hospitals, HospitalDto>();

            //Patient
            CreateMap<CreateUpdatePatientDto, Patient>();
            CreateMap<Patient, PatientDto>();

            //UserHospital
            CreateMap<CreateUpdateUserHospitalDto, UserHospital>();
            CreateMap<UserHospital, UserHospitalDto>();
        }
    }
}
