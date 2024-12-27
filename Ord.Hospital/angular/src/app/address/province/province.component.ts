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
import { NzOptionComponent, NzSelectComponent } from 'ng-zorro-antd/select';
import { NzButtonComponent } from 'ng-zorro-antd/button';
import { NzInputDirective } from 'ng-zorro-antd/input';
import { ExcelImportService } from '@proxy/controllers';
import { IFormFile } from '@proxy/microsoft/asp-net-core/http';

@Component({
  selector: 'app-province',
  standalone: true,
  imports: [
    CommonModule,
    NzTableModule,
    NzModalModule,
    NzFormModule,
    ReactiveFormsModule,
    NzSelectComponent,
    NzOptionComponent,
    NzButtonComponent,
    NzInputDirective
  ],
  templateUrl: './province.component.html',
  styleUrl: './province.component.scss'
})
export class ProvinceComponent implements OnInit{
    provinceTypes = [
      { label: 'Tỉnh', value: 1 },
      { label: 'Thành phố trung ương', value: 2 }]
    provinces :ProvinceDto[]
    createUpdateProvince: CreateUpdateProvinceDto;
    requestDto:PagedAndSortedResultRequestDto

    pageIndex = 1;
    pageSize = 6;
    total = 0;

    IsCreate = false;
    isVisible: any;
    provinceForm: FormGroup;
    selectedProvince: ProvinceDto | undefined;
    uploadForm: FormGroup;
    selectedFile: File | null = null;
    constructor(private provinceService:ProvinceService, private  excelImportService:ExcelImportService, private fb: FormBuilder) {
      this.requestDto = { sorting: 'ProvinceCode', skipCount: 0, maxResultCount:this.pageSize};

    }

    ngOnInit(): void {
      this.provinceForm = this.fb.group({
        provinceName: [null, [Validators.required]],
        provinceCode: [null, [Validators.required]],
        provinceType: [null, [Validators.required]]
      });
      this.loadList()
      this.uploadForm = this.fb.group({ provinceFile: [null, [Validators.required]] });
    }
  loadList(){
    this.provinceService.getListPaging(this.requestDto).subscribe(
      res=>{
        console.log(res);
        this.provinces=res.items;
        this.total=res.totalCount;
      })
  }
  createProvince() {
    this.provinceForm.reset();
    this.IsCreate = true;
    this.isVisible = true;

  }
  editProvince(province:ProvinceDto) {
    this.selectedProvince = province;
    this.provinceForm.patchValue(this.selectedProvince);
    this.IsCreate = false;
    this.isVisible = true;

  }

  deleteProvince(id) {
      this.provinceService.delete(id).subscribe(
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
      this.createUpdateProvince=this.provinceForm.value;
      if(this.IsCreate){
        this.provinceService.create(this.createUpdateProvince).subscribe(
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
        this.provinceService.update(this.selectedProvince.id,this.createUpdateProvince).subscribe(
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

  onFileChange(event) {
    if (event.target.files.length > 0) {
      this.selectedFile = event.target.files[0];
      this.uploadForm.patchValue({ provinceFile: this.selectedFile });
    }
  }

  uploadProvinceFile() {
      if (this.uploadForm.valid && this.selectedFile) {
      const formData: FormData = new FormData ();
      formData.append('file', this.selectedFile);
      this.excelImportService.importProvinceExcelByFile(formData).subscribe(
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

  onSubmit() {

  }
}
