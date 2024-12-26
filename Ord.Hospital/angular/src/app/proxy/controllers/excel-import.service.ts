  import { RestService, Rest } from '@abp/ng.core';
  import { Injectable } from '@angular/core';
  import type { IFormFile } from '../microsoft/asp-net-core/http/models';
  import type { ActionResult } from '../microsoft/asp-net-core/mvc/models';

  @Injectable({
    providedIn: 'root',
  })
  export class ExcelImportService {
    apiName = 'Default';


    importCommuneExcelByFile = (file: FormData, config?: Partial<Rest.Config>) =>
      this.restService.request<any, ActionResult>({
        method: 'POST',
        url: '/api/ExcelImport/importCommune',
        body: file,
      },
      { apiName: this.apiName,...config });


    importDistrictExcelByFile = (file: FormData, config?: Partial<Rest.Config>) =>
      this.restService.request<any, ActionResult>({
        method: 'POST',
        url: '/api/ExcelImport/importDistrict',
        body: file,
      },
      { apiName: this.apiName,...config });


    importProvinceExcelByFile = (file: FormData, config?: Partial<Rest.Config>) =>
      this.restService.request<any, ActionResult>({
        method: 'POST',
        url: '/api/ExcelImport/importProvince',
        body: file,
      },
      { apiName: this.apiName,...config });

    constructor(private restService: RestService) {}
  }
