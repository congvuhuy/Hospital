import { Component, OnInit } from '@angular/core';
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
import { PagedAndSortedResultRequestDto } from '@abp/ng.core';
import { DistrictService, ProvinceService } from '@proxy/services';
import { CreateUpdateDistrictDto, DistrictDto } from '@proxy/districts/dtos';
import { NzInputDirective } from 'ng-zorro-antd/input';
import { ProvinceDto } from '@proxy/provinces/dtos';
import { ExcelImportService } from '@proxy/controllers';

@Component({
  selector: 'app-district',
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
  templateUrl: './district.component.html',
  styleUrl: './district.component.scss'
})
export class DistrictComponent implements OnInit {

  districtTypes = [
    { label: 'Thị xã', value: 1 },
    { label: 'Thành phố', value: 2 },
    { label: 'Quận', value: 3 },
    { label: 'Huyện', value: 4 }
  ]
  //map tu ma tinh sang ten tinh
  provinceMap: { [code: string]: string } = {};
  //khai bao danh sach tinh huyen
  districts :DistrictDto[]
  provinces:ProvinceDto[]
  //khai bao huyen de truyen vao api
  createUpdateDistrict: CreateUpdateDistrictDto;
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
  districtForm: any;
  // huyen duoc chon de sua
  selectedDistrict: DistrictDto | undefined;
  uploadForm: any;
  selectedFile:  File | null = null;
  constructor(private districtService:DistrictService,private provinceService:ProvinceService,private excelImportService: ExcelImportService,private fb: FormBuilder) {
    this.requestDto = { sorting: 'ProvinceCode', skipCount: 0, maxResultCount:this.pageSize};
    this.requestFullDto = {sorting: '', skipCount: 0, maxResultCount:1000};
  }

  ngOnInit(): void {
    this.districtForm = this.fb.group({
      districtName: [null, [Validators.required]],
      districtCode: [null, [Validators.required]],
      districtType: [null, [Validators.required]],
      provinceCode: [null, [Validators.required]]
    });
    this.loadList()
    this.loadListProvince()
    this.uploadForm = this.fb.group({ districtFile: [null, [Validators.required]] });
  }
  loadList(){
    this.districtService.getListPaging(this.requestDto).subscribe(
      res=>{
        console.log(res);
        this.districts=res.items;
        this.total=res.totalCount;
      })
  }
  loadListProvince(){
    this.provinceService.getListPaging(this.requestFullDto).subscribe(
      res=>{
        this.provinces=res.items;
        this.provinces.forEach(province => { this.provinceMap[province.provinceCode] = province.provinceName; });
        console.log(this.provinces);
      })
  }
  createDistrict() {
    this.loadListProvince();

    this.districtForm.reset();
    this.IsCreate = true;
    this.isVisible = true;

  }
  editDistrict(district:DistrictDto) {
    this.loadListProvince();
    this.selectedDistrict =district;
    this.districtForm.patchValue(this.selectedDistrict);
    console.log(this.districtForm);
    this.IsCreate = false;
    this.isVisible = true;

  }

  deleteDistrict(id) {
    this.districtService.delete(id).subscribe(
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
    this.createUpdateDistrict=this.districtForm.value;
    if(this.IsCreate){
      this.districtService.create(this.createUpdateDistrict).subscribe(
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
      this.districtService.update(this.selectedDistrict.id,this.createUpdateDistrict).subscribe(
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

  onSubmit() {

  }

  onFileChange(event) {
    if (event.target.files.length > 0) {
      this.selectedFile = event.target.files[0];
      this.uploadForm.patchValue({ provinceFile: this.selectedFile });
    }
  }

  uploadDistrictFile() {
    if (this.uploadForm.valid && this.selectedFile) {
      const formData: FormData = new FormData ();
      formData.append('file', this.selectedFile);
      this.excelImportService.importDistrictExcelByFile(formData).subscribe(
        res => {
          alert('Tải lên tệp Excel của tỉnh thành công');
          this.loadList();
        },
        err => {
          alert('Lỗi khi tải lên tệp Excel của tỉnh');
          console.error(err);
        }
      );
    }
  }
}
