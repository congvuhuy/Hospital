
export interface CreateUpdateHospitalDto {
  hospitalName?: string;
  provinceCode: number;
  districtCode: number;
  communeCode: number;
  address?: string;
}

export interface HospitalDto {
  id: number;
  hospitalName?: string;
  provinceCode: number;
  districtCode: number;
  communeCode: number;
  address?: string;
}
