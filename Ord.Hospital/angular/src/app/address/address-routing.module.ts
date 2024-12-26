import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProvinceComponent } from './province/province.component';
import { DistrictComponent } from './district/district.component';
import { CommuneComponent } from './commune/commune.component';

const routes: Routes = [
  { path: 'province', component: ProvinceComponent },
  { path: 'district', component:DistrictComponent },
  { path: 'commune', component: CommuneComponent },

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AddressRoutingModule { }
