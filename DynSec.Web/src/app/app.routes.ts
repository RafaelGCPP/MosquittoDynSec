import { Routes } from '@angular/router';
import { ClientsComponent } from './clients/clients.component';
import { ClientDetailComponent } from './clients/client-detail/client-detail.component';
import { RolesComponent } from './roles/roles.component';
import { RoleDetailComponent } from './roles/role-detail/role-detail.component';

export const routes: Routes = [
  {
    path: 'clients', component: ClientsComponent,
    children: [
      { path: ':userName', component: ClientDetailComponent, },
    ]
  },
  {
    path: 'roles', component: RolesComponent,
    children: [
      { path: ':roleName', component: RoleDetailComponent, },
    ]
  }
];
