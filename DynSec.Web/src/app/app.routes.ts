import { Routes } from '@angular/router';
import { HomePageComponent } from './home-page/home-page.component';

export const routes: Routes = [
  { path: 'clients', loadChildren: () => import('./clients/clients.routes').then(m => m.routes) },
  { path: 'roles', loadChildren: () => import('./roles/roles.routes').then(m => m.routes) },
  { path: 'groups', loadChildren: () => import('./groups/groups.routes').then(m => m.routes) },
  { path: 'default-acl', loadChildren: () => import('./default-acl/default-acl.routes').then(m => m.routes) },
  { path: '**', component: HomePageComponent }
];
