import { mapEnumToOptions } from '@abp/ng.core';

export enum DistrictType {
  thixa = 1,
  thanhPho = 2,
  quan = 3,
  huyen = 4,
}

export const districtTypeOptions = mapEnumToOptions(DistrictType);
