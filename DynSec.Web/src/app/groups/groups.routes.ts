import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '', loadComponent: () => import('./groups.component').then(m => m.GroupsComponent),
    children: [
      {
        path: ':groupName', loadComponent: () => import('./group-detail/group-detail.component').then(m => m.GroupDetailComponent),
      },
    ]
  },
];
