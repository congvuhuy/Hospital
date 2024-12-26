import type { AuditedEntityDto } from '@abp/ng.core';

export interface CreateUpdateUserHospitalDto {
  userID?: string;
  hospitalID: number;
}

export interface UserHospitalDto extends AuditedEntityDto<number> {
  userID?: string;
  hospitalID: number;
}
