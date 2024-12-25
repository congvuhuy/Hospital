import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: false,
  application: {
    baseUrl,
    name: 'Hospital',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44334/',
    redirectUri: baseUrl,
    clientId: 'Hospital_App',
    responseType: 'code',
    scope: 'offline_access Hospital',
    requireHttps: true,
  },
  apis: {
    default: {
      url: 'https://localhost:44334',
      rootNamespace: 'Ord.Hospital',
    },
  },
} as Environment;
