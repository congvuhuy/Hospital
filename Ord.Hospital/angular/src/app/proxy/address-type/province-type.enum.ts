import { mapEnumToOptions } from '@abp/ng.core';

export enum ProvinceType {
  tinh = 1,
  thanhphotrunguong = 2,
}

export const provinceTypeOptions = mapEnumToOptions(ProvinceType);
