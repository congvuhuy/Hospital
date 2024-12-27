import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserHospitalComponent } from './user-hospital/user-hospital.component';
import { AuthGuard } from '../core/auth/auth.guard';
const routes: Routes = [
  { path: '', component: UserHospitalComponent,canActivate: [AuthGuard]},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserHospitalRoutingModule { }
