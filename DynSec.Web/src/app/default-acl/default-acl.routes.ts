import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '', loadComponent: () => import('./default-acl.component').then(m => m.DefaultAclComponent),
    children: [
    ]
  },
];
