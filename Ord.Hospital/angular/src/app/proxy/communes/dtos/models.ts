import type { CommuneType } from '../../address-type/commune-type.enum';

export interface CommuneDto {
  id: number;
  communeName?: string;
  communeCode: number;
  communeType: CommuneType;
  isDeleted: boolean;
  districtCode: number;
  provinceCode: number;
}

export interface CreateUpdateCommuneDto {
  communeName?: string;
  communeCode: number;
  communeType: CommuneType;
  provinceCode: number;
  districtCode: number;
}
