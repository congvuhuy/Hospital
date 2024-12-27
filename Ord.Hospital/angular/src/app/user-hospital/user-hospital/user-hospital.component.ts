import { jwtDecode } from "jwt-decode";
import { Component, OnInit } from '@angular/core';
import {
  NzTableCellDirective,
  NzTableComponent,
  NzTbodyComponent,
  NzTheadComponent,
  NzThMeasureDirective, NzTrDirective
} from 'ng-zorro-antd/table';
import { HospitalService, UserHospitalService } from '@proxy/services';
import { UserService } from '../../core/services/user.service';
import { FormBuilder, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { NgForOf, NgIf } from '@angular/common';
import { NzButtonComponent } from 'ng-zorro-antd/button';
import { NzColDirective, NzRowDirective } from 'ng-zorro-antd/grid';
import { NzFormControlComponent, NzFormDirective, NzFormItemComponent, NzFormLabelComponent } from 'ng-zorro-antd/form';
import { NzModalComponent, NzModalContentDirective } from 'ng-zorro-antd/modal';
import { NzOptionComponent, NzSelectComponent } from 'ng-zorro-antd/select';
import { NzInputDirective, NzInputModule } from 'ng-zorro-antd/input';
import { HospitalDto } from '@proxy/hospital-ord/dtos';
import { PagedAndSortedResultRequestDto } from '@abp/ng.core';
import { CreateUpdateUserHospitalDto, UserHospitalDto } from '@proxy/user-hospitals/dtos';
import { PatientDto } from '@proxy/patients/dtos';

@Component({
  selector: 'app-user-hospital',
  standalone: true,
  imports: [
    FormsModule,
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
    NzModalContentDirective,
    NgForOf,
    NzInputModule
  ],
  templateUrl: './user-hospital.component.html',
  styleUrl: './user-hospital.component.scss'
})
export class UserHospitalComponent implements OnInit{
  usersHospitals:UserHospitalDto[];
  hospitals :HospitalDto[];
  user:[]=[];
  users: any;
  hospitalMap: { [code: string]: string } = {};
  usersMap: { [code: string]: string } = {};

  total: 10;
  pageSize: 10;
  pageIndex: 1;
  requestFullDto:PagedAndSortedResultRequestDto ;
  isVisible: boolean=false;
  createUpdateUserHospital: CreateUpdateUserHospitalDto;
  IsCreate=false;
  userHospitalForm: any;
   selectedUserHospital:UserHospitalDto|undefined;


  constructor(private userHospitalService: UserHospitalService, private hospitalService: HospitalService, private userService:UserService ,private fb: FormBuilder) {
  }
  ngOnInit(): void {
    this.userHospitalForm = this.fb.group({
      userID: [null, [Validators.required]],
      hospitalID:[null,[Validators.required]],
    });
    this.requestFullDto = {sorting: '', skipCount: 0, maxResultCount:1000};
    this.loadListUserHospital();
    this.loadListHospital();
    this.loadListUser();
  }
  loadListUserHospital(){
    this.userHospitalService.getList(this.requestFullDto).subscribe(
      res=>{
        this.usersHospitals=res.items
      }
    )
  }
  loadListHospital(){
    this.hospitalService.getList(this.requestFullDto).subscribe(
      res=>{
        this.hospitals=res.items;
        this.hospitals.forEach(hospital => { this.hospitalMap[hospital.id] = hospital.hospitalName; });
      }
    )
  }
  loadListUser(){
    this.userService.getUsers().subscribe(
      res=>{
        this.users=res.items
        this.users.forEach(user => { this.usersMap[user.id] = user.userName;});
      },
      err => {
        console.log(err);
      }
    )
  }
  handleCancel() {
    this.isVisible = false;
  }

  handleOk() {
    this.createUpdateUserHospital=this.userHospitalForm.value;
    if(this.IsCreate){
      this.userHospitalService.create(this.createUpdateUserHospital).subscribe(
        res=>{
          this.isVisible=false;
          this.loadListUserHospital();
        },
        err=>{
          console.error(err)
        }
      )
    }else{
      this.userHospitalService.update(this.selectedUserHospital.id,this.createUpdateUserHospital).subscribe(
        res=>{
          this.isVisible = false;
          this.loadListUserHospital();
        },
        err=>{
          console.error(err);
        }
      )
    }
  }
  editUserHospital(userHospital:UserHospitalDto) {
    this.selectedUserHospital = userHospital;
    this.userHospitalForm.patchValue(this.selectedUserHospital);
    this.IsCreate = false;
    this.isVisible = true;

  }
  onPageIndexChange($event: number) {

  }

  createUsersHospital() {

  }

  deleteUserHospital(id) {

  }
}
