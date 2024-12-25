import { Component, OnInit } from '@angular/core';
import { ProvinceService } from '@proxy/services';
import { CreateUpdateProvinceDto, ProvinceDto } from '@proxy/provinces/dtos';
import { PagedAndSortedResultRequestDto } from '@abp/ng.core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NzTableComponent, NzTableModule } from 'ng-zorro-antd/table';
import { NzModalModule } from 'ng-zorro-antd/modal';
import { NzFormModule } from 'ng-zorro-antd/form';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { NzDropdownMenuComponent } from 'ng-zorro-antd/dropdown';

@Component({
  selector: 'app-province',
  standalone: true,
  imports: [
    CommonModule,
    NzTableModule,
    NzModalModule,
    NzFormModule,
    ReactiveFormsModule,
    NzDropdownMenuComponent
  ],
  templateUrl: './province.component.html',
  styleUrl: './province.component.scss'
})
export class ProvinceComponent implements OnInit{
    provinces :ProvinceDto[]
    sorting:string=""
    requestDto:PagedAndSortedResultRequestDto

    pageIndex = 1;
    pageSize = 8;
    total = 10111;

    isVisible: any;
    provinceForm: any;
    selectedProvince: ProvinceDto | undefined;
    constructor(private provinceService:ProvinceService,private fb: FormBuilder) {
      this.requestDto = { sorting: 'ProvinceCode', skipCount: 0, maxResultCount: 1000 };
      this.provinceForm = this.fb.group({
        provinceName: [null, [Validators.required]],
        provinceCode: [null, [Validators.required]]

      });
    }

    ngOnInit(): void {
        this.provinceService.getList(this.requestDto).subscribe(
          res=>{
            console.log(res);
            this.provinces=res.items;
          }
        )
    }

  editProvince(province:ProvinceDto) {
    this.selectedProvince = province;
    this.provinceForm.patchValue(province);
    this.isVisible = true;
    console.log(this.selectedProvince)
  }

  deleteProvince(id) {

  }

  handleCancel() {
    this.isVisible = false;

  }

  handleOk() {
    this.isVisible = false;
    console.log(this.provinceForm);
  }
}
