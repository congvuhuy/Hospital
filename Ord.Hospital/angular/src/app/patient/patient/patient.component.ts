import { Component } from '@angular/core';
import { CreateUpdateHospitalDto, HospitalDto } from '@proxy/hospital-ord/dtos';
import { PagedAndSortedResultRequestDto } from '@abp/ng.core';
import { ProvinceDto } from '@proxy/provinces/dtos';
import { DistrictDto } from '@proxy/districts/dtos';
import { CommuneDto } from '@proxy/communes/dtos';
import { CommuneService, DistrictService, HospitalService, PatientService, ProvinceService } from '@proxy/services';
import { FormBuilder, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
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
import { CreateUpdatePatientDto, PatientDto } from '@proxy/patients/dtos';
interface JwtPayload { sub: string;  }
@Component({
  selector: 'app-patient',
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
  templateUrl: './patient.component.html',
  styleUrl: './patient.component.scss'
})

export class PatientComponent {
  patients :PatientDto[]
  createUpdatepPatient: CreateUpdatePatientDto;
  requestDto:PagedAndSortedResultRequestDto;
  requestFullDto:PagedAndSortedResultRequestDto;


  communeMap: { [code: string]: string } = {};
  districtMap: { [code: string]: string } = {};
  provinceMap: { [code: string]: string } = {};
  hospitalMap: { [code: string]: string } = {};

  pageIndex = 1;
  pageSize = 6;
  total = 0;

  IsCreate = false;
  isVisible: any;
  patientForm: any;
  selectedPatient: PatientDto | undefined;
  provinces: ProvinceDto[];
  districts: DistrictDto[];
  communes: CommuneDto[];
  hospitals:HospitalDto[];
  districtsByProvince: DistrictDto[];
  communesByDistrict: CommuneDto[];
  provinceCodeSubscription: number;
  districtCodeSubscription: number;
  UserInfo:JwtPayload;
  UserId:string;
  constructor(private patientService:PatientService, private hospitalService:HospitalService, private provinceService:ProvinceService,
              private districtService:DistrictService,private communeService:CommuneService, private fb: FormBuilder) {
              this.requestDto = { sorting: 'PatientName', skipCount: 0, maxResultCount:this.pageSize};
              this.requestFullDto = {sorting: 'ProvinceCode', skipCount: 0, maxResultCount:1000};
  }

  ngOnInit(): void {
    this.patientForm = this.fb.group({
      patientName: [null, [Validators.required]],
      address:[null,[Validators.required]],
      provinceCode: [null, [Validators.required]],
      districtCode: [null, [Validators.required]],
      communeCode: [null, [Validators.required]],
      hospitalID:[null,[Validators.required]]
    });
    this.UserInfo=this.parseJwt(localStorage.getItem("access_token"));
    this.UserId=this.UserInfo.sub;

    this.loadList()
    this.loadListProvince();
    this.loadListDistrict();
    this.loadListCommune();
    this.loadListHospital();
    this.provinceCodeSubscription = this.patientForm.get('provinceCode').valueChanges.subscribe(
      provinceCode => {
        if (provinceCode) {
          this.loadListDistrictByProvinceCode(provinceCode);
        }
      });
    this.districtCodeSubscription = this.patientForm.get('districtCode').valueChanges.subscribe(
      districtCode => {
        if (districtCode) {
          this.loadListCommuneByDistrictCode(districtCode);
        }
      });
  }
  parseJwt(token) {
    // Tách các phần của JWT
    var base64Url = token.split('.')[1];
    var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    var jsonPayload = decodeURIComponent(atob(base64).split('').map(function(c) {
      return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
    }).join(''));

    return JSON.parse(jsonPayload);
  }
  loadList(){
    console.log("userid",this.UserId);
    this.patientService.getListPaging(this.requestDto, this.UserId).subscribe(
      res=>{
        console.log("list benh nhan",res);
        this.patients=res.items;
        this.total=res.totalCount;
      })
  }
  loadListProvince(){
    this.provinceService.getListPaging(this.requestFullDto).subscribe(
      res=>{
        this.provinces=res.items;
        this.provinces.forEach(province => { this.provinceMap[province.provinceCode] = province.provinceName; });
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
  loadListHospital(){
    this.hospitalService.getList(this.requestFullDto).subscribe(
      res=>{
        this.hospitals=res.items;
        this.hospitals.forEach(hospital => { this.hospitalMap[hospital.id] = hospital.hospitalName; });
      }
    )
  }
  createPatient() {
    this.patientForm.reset();
    this.IsCreate = true;
    this.isVisible = true;

  }
  editPatient(patient:PatientDto) {
    this.selectedPatient = patient;
    this.patientForm.patchValue(this.selectedPatient);
    console.log(patient);
    this.IsCreate = false;
    this.isVisible = true;
  }

  deletePatient(id) {
    this.patientService.delete(id).subscribe(
      res=>{
        alert("Xóa bệnh nhân thành công" )
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
    this.createUpdatepPatient=this.patientForm.value;
    if(this.IsCreate){
      this.patientService.create(this.createUpdatepPatient).subscribe(
        res=>{
          this.isVisible=false;
          this.loadList();
        },
        err=>{
          console.error(err)
        }
      )
    }else{
      this.patientService.update(this.selectedPatient.id,this.createUpdatepPatient).subscribe(
        res=>{
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
  }
}
