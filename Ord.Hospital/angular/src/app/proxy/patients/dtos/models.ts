
export interface CreateUpdatePatientDto {
  patientName?: string;
  provinceCode: number;
  districtCode: number;
  communeCode: number;
  address?: string;
  hospitalID: number;
}

export interface PatientDto {
  id: number;
  patientName?: string;
  provinceCode: number;
  districtCode: number;
  communeCode: number;
  address?: string;
  hospitalID: number;
}
