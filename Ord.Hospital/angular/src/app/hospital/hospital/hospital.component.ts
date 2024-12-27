import { Component, OnInit } from '@angular/core';
import { CreateUpdateProvinceDto, ProvinceDto } from '@proxy/provinces/dtos';
import { PagedAndSortedResultRequestDto } from '@abp/ng.core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommuneService, DistrictService, HospitalService, ProvinceService } from '@proxy/services';
import { ExcelImportService } from '@proxy/controllers';
import { CreateUpdateHospitalDto, HospitalDto } from '@proxy/hospital-ord/dtos';
import { NgForOf, NgIf } from '@angular/common';
import { NzButtonComponent } from 'ng-zorro-antd/button';
import { NzColDirective, NzRowDirective } from 'ng-zorro-antd/grid';
import { NzFormControlComponent, NzFormDirective, NzFormItemComponent, NzFormLabelComponent } from 'ng-zorro-antd/form';
import { NzModalComponent, NzModalContentDirective } from 'ng-zorro-antd/modal';
import { NzOptionComponent, NzSelectComponent } from 'ng-zorro-antd/select';
import {
  NzTableCellDirective,
  NzTableComponent,
  NzTbodyComponent,
  NzTheadComponent,
  NzThMeasureDirective, NzTrDirective
} from 'ng-zorro-antd/table';
import { NzInputDirective } from 'ng-zorro-antd/input';
import { DistrictDto } from '@proxy/districts/dtos';
import { CommuneDto } from '@proxy/communes/dtos';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-hospital',
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
  templateUrl: './hospital.component.html',
  styleUrl: './hospital.component.scss'
})
export class HospitalComponent implements OnInit{


  hospitals :HospitalDto[]
  createUpdateHospital: CreateUpdateHospitalDto;
  requestDto:PagedAndSortedResultRequestDto;
  requestFullDto:PagedAndSortedResultRequestDto;


  communeMap: { [code: string]: string } = {};
  districtMap: { [code: string]: string } = {};
  provinceMap: { [code: string]: string } = {};

  pageIndex = 1;
  pageSize = 6;
  total = 0;

  IsCreate = false;
  isVisible: any;
  hospitalForm: any;
  selectedHospital: HospitalDto | undefined;
  provinces: ProvinceDto[];
  districts: DistrictDto[];
  communes: CommuneDto[];
  districtsByProvince: DistrictDto[];
   communesByDistrict: CommuneDto[];
   provinceCodeSubscription: number;
  districtCodeSubscription: number;
  constructor(private hospitalService:HospitalService, private provinceService:ProvinceService,
              private districtService:DistrictService,private communeService:CommuneService, private fb: FormBuilder) {
    this.requestDto = { sorting: 'HospitalName', skipCount: 0, maxResultCount:this.pageSize};
    this.requestFullDto = {sorting: 'ProvinceCode', skipCount: 0, maxResultCount:1000};
  }

  ngOnInit(): void {
    this.hospitalForm = this.fb.group({
      hospitalName: [null, [Validators.required]],
      address:[null,[Validators.required]],
      provinceCode: [null, [Validators.required]],
      districtCode: [null, [Validators.required]],
      communeCode: [null, [Validators.required]],
    });
    this.loadList()
    this.loadListProvince();
    this.loadListDistrict();
    this.loadListCommune()
    this.provinceCodeSubscription = this.hospitalForm.get('provinceCode').valueChanges.subscribe(
      provinceCode => {
        if (provinceCode) {
          this.loadListDistrictByProvinceCode(provinceCode);
        }
      });
    this.districtCodeSubscription = this.hospitalForm.get('districtCode').valueChanges.subscribe(
      districtCode => {
        if (districtCode) {
          this.loadListCommuneByDistrictCode(districtCode);
        }
      });
  }
  loadList(){
    this.hospitalService.getList(this.requestDto).subscribe(
      res=>{
        console.log(res);
        this.hospitals=res.items;
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
  loadListCommuneByDistrictCode( districtCode:number){
    this.communeService.getListByDistrictCodeByDistrictCode(districtCode).subscribe(
      res=>{
        this.communesByDistrict = res;
      }
    )
  }
  loadListCommune(){
    this.communeService.getListPaging(this.requestFullDto).subscribe(
      res=>{
        this.communes=res.items;
        this.communes.forEach(commune => { this.communeMap[commune.communeCode] = commune.communeName; });
      })
  }
  createHospital() {
    this.hospitalForm.reset();
    this.IsCreate = true;
    this.isVisible = true;

  }
  editHospital(hospital:HospitalDto) {
    this.selectedHospital = hospital;
    this.hospitalForm.patchValue(this.selectedHospital);
    this.IsCreate = false;
    this.isVisible = true;

  }

  deleteHospital(id) {
    this.provinceService.delete(id).subscribe(
      res=>{
        console.log(res);
        alert("Xóa bệnh viện thành công" )
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
    this.createUpdateHospital=this.hospitalForm.value;
    if(this.IsCreate){
      this.hospitalService.create(this.createUpdateHospital).subscribe(
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
      this.hospitalService.update(this.selectedHospital.id,this.createUpdateHospital).subscribe(
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
