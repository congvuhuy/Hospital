import { RestService, Rest } from '@abp/ng.core';
import type { PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { CommuneDto, CreateUpdateCommuneDto } from '../communes/dtos/models';

@Injectable({
  providedIn: 'root',
})
export class CommuneService {
  apiName = 'Default';
  

  create = (input: CreateUpdateCommuneDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, CommuneDto>({
      method: 'POST',
      url: '/api/app/commune',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  createMultiple = (communeList: CreateUpdateCommuneDto[], config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: '/api/app/commune/multiple',
      body: communeList,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: number, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/commune/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: number, config?: Partial<Rest.Config>) =>
    this.restService.request<any, CommuneDto>({
      method: 'GET',
      url: `/api/app/commune/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: PagedAndSortedResultRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<CommuneDto>>({
      method: 'GET',
      url: '/api/app/commune',
      params: { sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getListPaging = (input: PagedAndSortedResultRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<CommuneDto>>({
      method: 'GET',
      url: '/api/app/commune/paging',
      params: { sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  update = (id: number, input: CreateUpdateCommuneDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, CommuneDto>({
      method: 'PUT',
      url: `/api/app/commune/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });
  

  getListByDistrictCodeByDistrictCode = (districtCode: number, config?: Partial<Rest.Config>) =>
    this.restService.request<any, CommuneDto[]>({
      method: 'GET',
      url: '/api/app/commune/get-list-by-district-code',
      params: { districtCode },
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
