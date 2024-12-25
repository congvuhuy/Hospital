import { RestService, Rest } from '@abp/ng.core';
import type { PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { CreateUpdateProvinceDto, ProvinceDto } from '../provinces/dtos/models';

@Injectable({
  providedIn: 'root',
})
export class ProvinceService {
  apiName = 'Default';
  

  create = (input: CreateUpdateProvinceDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ProvinceDto>({
      method: 'POST',
      url: '/api/app/province',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  createMultiple = (input: CreateUpdateProvinceDto[], config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: '/api/app/province/multiple',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: number, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/province/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: number, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ProvinceDto>({
      method: 'GET',
      url: `/api/app/province/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getByCodeByCode = (code: number, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ProvinceDto>({
      method: 'GET',
      url: '/api/app/province/by-code',
      params: { code },
    },
    { apiName: this.apiName,...config });
  

  getFilterAllByPageNumberAndPageSize = (pageNumber: number, pageSize: number, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ProvinceDto[]>({
      method: 'GET',
      url: '/api/app/province/filter-all',
      params: { pageNumber, pageSize },
    },
    { apiName: this.apiName,...config });
  

  getList = (input: PagedAndSortedResultRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<ProvinceDto>>({
      method: 'GET',
      url: '/api/app/province',
      params: { sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  update = (id: number, input: CreateUpdateProvinceDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ProvinceDto>({
      method: 'PUT',
      url: `/api/app/province/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
