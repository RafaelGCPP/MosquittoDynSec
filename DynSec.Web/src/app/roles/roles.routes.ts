import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '', loadComponent: () => import('./roles.component').then(m => m.RolesComponent),
    children: [
      {
        path: ':roleName', loadComponent: () => import('./role-detail/role-detail.component').then(m => m.RoleDetailComponent),
      },
    ]
  },
];
