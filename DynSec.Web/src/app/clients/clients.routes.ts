import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '', loadComponent: () => import('./clients.component').then(m => m.ClientsComponent),
    children: [
      { path: ':userName', loadComponent: () => import('./client-detail/client-detail.component').then(m => m.ClientDetailComponent), },
    ]
  },
];
