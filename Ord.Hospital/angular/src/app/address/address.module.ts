import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NzTableModule } from 'ng-zorro-antd/table';
import { NzModalModule } from 'ng-zorro-antd/modal';
import { NzFormModule } from 'ng-zorro-antd/form';
import { ReactiveFormsModule } from '@angular/forms';
import { AddressRoutingModule } from './address-routing.module';
import { ProvinceComponent } from './province/province.component';
@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    AddressRoutingModule,
    NzTableModule,
    NzModalModule,
    NzFormModule,
    ReactiveFormsModule,
    ProvinceComponent
  ],
  exports:[
    ProvinceComponent,
  ]
})
export class AddressModule { }
