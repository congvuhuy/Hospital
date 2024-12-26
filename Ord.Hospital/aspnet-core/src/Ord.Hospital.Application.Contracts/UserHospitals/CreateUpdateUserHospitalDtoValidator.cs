using FluentValidation;
using Ord.Hospital.UserHospitals.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ord.Hospital.UserHospitals
{
    public class CreateUpdateUserHospitalDtoValidator:AbstractValidator<CreateUpdateUserHospitalDto>
    {
        public CreateUpdateUserHospitalDtoValidator()
        {
            RuleFor(x => x.UserID).NotEmpty().WithMessage("Mã tỉnh không được để trống");
            RuleFor(x => x.HospitalID).NotEmpty().WithMessage("Mã tỉnh không được để trống")
                                      .GreaterThan(0).WithMessage("Mã bệnh viện không hợp lệ");

        }
    }
}
