import { Environment } from '@abp/ng.core';

// const baseUrl = 'http://localhost:4200';
const baseUrl = 'https://192.168.116.131:5001';

export const environment = {
  production: true,
  application: {
    baseUrl,
    name: 'Hospital',
    logoUrl: '',
  },
  oAuthConfig: {
    // issuer: 'https://localhost:44334/',
    issuer: 'https://192.168.116.131:5000/',
    redirectUri: baseUrl,
    clientId: 'Hospital_App',
    responseType: 'code',
    scope: 'offline_access Hospital',
    requireHttps: true
  },
  apis: {
    default: {
      // url: 'https://localhost:44334',
      url: 'https://192.168.116.131:5000',
      rootNamespace: 'Ord.Hospital',

    },
  },
} as Environment;
