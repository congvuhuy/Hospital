import { RoutesService, eLayoutType } from '@abp/ng.core';
import { APP_INITIALIZER } from '@angular/core';

export const APP_ROUTE_PROVIDER = [
  { provide: APP_INITIALIZER, useFactory: configureRoutes, deps: [RoutesService], multi: true },
];

function configureRoutes(routesService: RoutesService) {
  return () => {
    routesService.add([
      {
        path: '/',
        name: '::Menu:Home',
        iconClass: 'fas fa-home',
        order: 1,
        layout: eLayoutType.application,
      },
      {
        path: '/address',
        name: 'address',
        order: 2,
        iconClass: 'fas fa-home',
        layout: eLayoutType.application,
      },
      {
        path: '/address/province',

        name: 'province',
        parentName: 'address',
        iconClass: 'fas fa-home',
        layout: eLayoutType.application,
      },

      {
        path: '/address/district',
        name: 'district',
        parentName: 'address',
        iconClass: 'fas fa-home',
        layout: eLayoutType.application,
      },
      {
        path: '/address/commune',
        name: 'commune',
        parentName: 'address',
        iconClass: 'fas fa-home',
        layout: eLayoutType.application,
      },

      {
        path: '/hospital',
        name: 'hospital',
        iconClass: 'fas fa-book',
        layout: eLayoutType.application,
      },
      {
        path: '/patient',
        name: 'patient',
        iconClass: 'fas fa-phone',
        layout: eLayoutType.application,
      },
      {
        path: '/user-hospital',
        name: 'user-hospital',
        iconClass: 'fas fa-pen',
        layout: eLayoutType.application,
      },
    ]);
  };
}
