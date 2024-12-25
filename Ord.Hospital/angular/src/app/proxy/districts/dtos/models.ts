import type { DistrictType } from '../../address-type/district-type.enum';

export interface CreateUpdateDistrictDto {
  districtName?: string;
  districtCode: number;
  districtType: DistrictType;
  provinceCode: number;
}

export interface DistrictDto {
  id: number;
  districtName?: string;
  districtCode: number;
  districtType: DistrictType;
  deleterId?: number;
  isDeleted: boolean;
  provinceCode: number;
}
