import { Routes } from '@angular/router';
import { HomePageComponent } from './home-page/home-page.component';
import { IsAuthenticatedGuard } from './security/auth.guard';

export const routes: Routes = [
  { path: 'clients', loadChildren: () => import('./clients/clients.routes').then(m => m.routes), canActivate: [IsAuthenticatedGuard] },
  { path: 'roles', loadChildren: () => import('./roles/roles.routes').then(m => m.routes), canActivate: [IsAuthenticatedGuard] },
  { path: 'groups', loadChildren: () => import('./groups/groups.routes').then(m => m.routes), canActivate: [IsAuthenticatedGuard] },
  { path: 'default-acl', loadChildren: () => import('./default-acl/default-acl.routes').then(m => m.routes), canActivate: [IsAuthenticatedGuard] },
  { path: '**', component: HomePageComponent }
];
