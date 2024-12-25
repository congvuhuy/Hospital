import type { ProvinceType } from '../../address-type/province-type.enum';

export interface CreateUpdateProvinceDto {
  provinceName?: string;
  provinceCode: number;
  provinceType: ProvinceType;
}

export interface ProvinceDto {
  id: number;
  provinceName?: string;
  provinceCode: number;
  provinceType: ProvinceType;
  deleterId?: number;
  isDeleted: boolean;
}
