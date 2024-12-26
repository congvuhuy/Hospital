using FluentValidation;
using Ord.Hospital.Patients.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ord.Hospital.Patients
{
    public class CreateUpdatePatitentDtoValidator:AbstractValidator<CreateUpdatePatientDto>
    {
        public CreateUpdatePatitentDtoValidator() 
        {
            RuleFor(x => x.PatientName).NotEmpty().WithMessage("Tên không được để trống")
                                       .MaximumLength(50).WithMessage("Tên không quá 50 ký tự");

            RuleFor(x => x.CommuneCode).NotEmpty().WithMessage("Mã xã không được để trống")
                                            .GreaterThan(0).WithMessage("Mã xã không hợp lệ");
            
            RuleFor(x => x.DistrictCode).NotEmpty().WithMessage("Mã huyện không được để trống")
                                       .GreaterThan(0).WithMessage("Mã tỉnh không hợp lệ");

            RuleFor(x => x.ProvinceCode).NotEmpty().WithMessage("Mã tỉnh không được để trống")
                                      .GreaterThan(0).WithMessage("Mã tỉnh không hợp lệ");
            RuleFor(x => x.HospitalID).NotEmpty().WithMessage("Mã bệnh viện không được để trống")
                                      .GreaterThan(0).WithMessage("Mã bệnh viện không hợp lệ");
        }
    }
}
