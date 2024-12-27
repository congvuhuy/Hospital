import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    loadChildren: () => import('./home/home.module').then(m => m.HomeModule),
  },
  {
    path: 'account',
    loadChildren: () => import('@abp/ng.account').then(m => m.AccountModule.forLazy()),
  },
  {
    path: 'identity',
    loadChildren: () => import('@abp/ng.identity').then(m => m.IdentityModule.forLazy()),
  },
  {
    path: 'tenant-management',
    loadChildren: () =>
      import('@abp/ng.tenant-management').then(m => m.TenantManagementModule.forLazy()),
  },
  {
    path: 'setting-management',
    loadChildren: () =>
      import('@abp/ng.setting-management').then(m => m.SettingManagementModule.forLazy()),
  },
  {
    path:'address',
    loadChildren:()=>
      import('./address/address.module').then(m=>m.AddressModule)
  },
  {
    path:'patient',
    loadChildren:()=>
      import('./patient/patient.module').then(m=>m.PatientModule)
  },
  {
    path:'hospital',
    loadChildren:()=>
      import('./hospital/hospital.module').then(m=>m.HospitalModule)
  },
  {
    path:'user-hospital',
    loadChildren:()=>
      import('./user-hospital/user-hospital.module').then(m=>m.UserHospitalModule)
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {})],
  exports: [RouterModule],
})
export class AppRoutingModule {}
