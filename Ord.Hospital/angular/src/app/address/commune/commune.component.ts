import { Component, OnInit } from '@angular/core';
import { CreateUpdateDistrictDto, DistrictDto } from '@proxy/districts/dtos';
import { ProvinceDto } from '@proxy/provinces/dtos';
import { PagedAndSortedResultRequestDto, RootCoreModule } from '@abp/ng.core';
import { CommuneService, DistrictService, ProvinceService } from '@proxy/services';
import { FormBuilder, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import {
  NzTableCellDirective,
  NzTableComponent,
  NzTbodyComponent,
  NzTheadComponent,
  NzThMeasureDirective, NzTrDirective
} from 'ng-zorro-antd/table';
import { NzButtonComponent } from 'ng-zorro-antd/button';
import { NgForOf, NgIf } from '@angular/common';
import { NzModalComponent, NzModalContentDirective } from 'ng-zorro-antd/modal';
import { NzFormControlComponent, NzFormDirective, NzFormItemComponent, NzFormLabelComponent } from 'ng-zorro-antd/form';
import { NzColDirective, NzRowDirective } from 'ng-zorro-antd/grid';
import { NzInputDirective } from 'ng-zorro-antd/input';
import { NzOptionComponent, NzSelectComponent } from 'ng-zorro-antd/select';
import { CommuneDto, CreateUpdateCommuneDto } from '@proxy/communes/dtos';

@Component({
  selector: 'app-commune',
  standalone: true,
  imports: [
    FormsModule,
    NgForOf,
    NgIf,
    NzButtonComponent,
    NzColDirective,
    NzFormControlComponent,
    NzFormDirective,
    NzFormItemComponent,
    NzFormLabelComponent,
    NzModalComponent,
    NzOptionComponent,
    NzRowDirective,
    NzSelectComponent,
    NzTableCellDirective,
    NzTableComponent,
    NzTbodyComponent,
    NzThMeasureDirective,
    NzTheadComponent,
    NzTrDirective,
    ReactiveFormsModule,
    NzInputDirective,
    NzModalContentDirective
  ],
  templateUrl: './commune.component.html',
  styleUrl: './commune.component.scss'
})
export class CommuneComponent implements OnInit {
  communeTypes = [
    { label: 'Phường', value: 1 },
    { label: 'Xã', value: 2 },
    { label: 'Thị trấn', value: 3 },
  ]
  //map tu ma tinh sang ten tinh
  districtMap: { [code: string]: string } = {};
  provinceMap: { [code: string]: string } = {};
  //khai bao danh sach tinh huyen
  communes:CommuneDto[] ;
  districts :DistrictDto[]
  districtsByProvince:DistrictDto[]
  provinces:ProvinceDto[]

  //khai bao huyen de truyen vao api
  createUpdateCommune: CreateUpdateCommuneDto;
  //gia tri truyen vao khi get list
  requestDto:PagedAndSortedResultRequestDto;
  requestFullDto:PagedAndSortedResultRequestDto;
  // giá trị ban đầu cho nz table
  pageIndex = 1;
  pageSize = 6;
  total = 0;
  // trang thai form them va sua
  IsCreate = false;
  isVisible: any;
  // khai bao reactivce form
  communeForm: any;
  // huyen duoc chon de sua
  selectedCommune: CommuneDto | undefined;
  provinceCodeSubscription: number;
  constructor(private communeService:CommuneService, private districtService:DistrictService,
              private provinceService:ProvinceService,private fb: FormBuilder) {
    this.requestDto = { sorting: 'ProvinceCode', skipCount: 0, maxResultCount:this.pageSize};
    this.requestFullDto = {sorting: 'ProvinceCode', skipCount: 0, maxResultCount:1000};
  }

  ngOnInit(): void {
    this.communeForm = this.fb.group({
      communeName: [null, [Validators.required]],
      communeCode: [null, [Validators.required]],
      communeType: [null, [Validators.required]],
      provinceCode: [null, [Validators.required]],
      districtCode: [null, [Validators.required]],

    });
    this.loadList()
    this.loadListProvince()
    this.loadListDistrict()
    this.provinceCodeSubscription = this.communeForm.get('provinceCode').valueChanges.subscribe(
      provinceCode => {
        if (provinceCode) {
          this.loadListDistrictByProvinceCode(provinceCode);
        }
      });
  }
  loadList(){
    this.communeService.getListPaging(this.requestDto).subscribe(
      res=>{
        console.log(res);
        this.communes=res.items;
        this.total=res.totalCount;
      })
  }
  loadListProvince(){
    this.provinceService.getListPaging(this.requestFullDto).subscribe(
      res=>{
        this.provinces=res.items;
        console.log("danh sach tinh:",this.provinces);
        this.provinces.forEach(province => { this.provinceMap[province.provinceCode] = province.provinceName; });
        console.log(this.provinces);
      })
  }
  loadListDistrictByProvinceCode( provinceCode:number){
    this.districtService.getListByProvinceCodeByProvinceCode(provinceCode).subscribe(
      res=>{
        this.districtsByProvince = res;
      }
    )
  }
  loadListDistrict(){
    this.districtService.getListPaging(this.requestFullDto).subscribe(
      res=>{
        this.districts=res.items;
        this.districts.forEach(district => { this.districtMap[district.districtCode] = district.districtName; });
      })
  }
  createCommune() {
    this.loadListProvince();

    this.communeForm.reset();
    this.IsCreate = true;
    this.isVisible = true;

  }
  editCommune(commune:CommuneDto) {
    this.loadListProvince();
    this.selectedCommune =commune;
    this.communeForm.patchValue(this.selectedCommune);
    console.log(this.communeForm);
    this.IsCreate = false;
    this.isVisible = true;

  }

  deleteCommune(id) {
    this.communeService.delete(id).subscribe(
      res=>{
        console.log(res);
        alert("Xóa tỉnh thành công" )
        this.loadList()
      },err=>{
        console.error(err)
      }
    )

  }

  handleCancel() {
    this.isVisible = false;
  }

  handleOk() {
    this.createUpdateCommune=this.communeForm.value;
    if(this.IsCreate){
      this.communeService.create(this.createUpdateCommune).subscribe(
        res=>{
          console.log(res);
          this.isVisible=false;
          this.loadList();
        },
        err=>{
          console.error(err)
        }
      )
    }else{
      this.communeService.update(this.selectedCommune.id,this.createUpdateCommune).subscribe(
        res=>{
          console.log(res);
          this.isVisible = false;
          this.loadList();
        },
        err=>{
          console.error(err);
        }
      )
    }

  }

  onPageIndexChange(newPageIndex: number)  {
    this.pageIndex = newPageIndex;
    this.requestDto.skipCount = (newPageIndex - 1) * this.pageSize;
    this.loadList();
    console.log('PageIndex:', this.pageIndex, 'PageSize:', this.pageSize, 'Total:', this.total);
  }
}
