import { mapEnumToOptions } from '@abp/ng.core';

export enum CommuneType {
  phuong = 1,
  xa = 2,
  thitran = 3,
}

export const communeTypeOptions = mapEnumToOptions(CommuneType);
