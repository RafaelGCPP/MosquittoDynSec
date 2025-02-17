import { Component, ViewChild } from '@angular/core';
import { Role } from '../../model/role';
import { RolesGraphqlService } from '../roles.graphql.service';
import { AppHealthCheckService } from '../../app.health.service';
import { MatTable, MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { RouterLink } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'dynsec-roles-list',
  imports: [
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    RouterLink
  ],
  templateUrl: './roles-list.component.html',
  styleUrl: './roles-list.component.scss'
})
export class RolesListComponent {

  roles: Role[] = [];
  private querySubscription!: Subscription;
  displayedColumns: string[] = ['roleName', 'buttons'];
  loading: boolean = true;

  constructor(
    private readonly graphql: RolesGraphqlService,
    private readonly healthCheck: AppHealthCheckService
  ) { }


  @ViewChild(MatTable)
  table!: MatTable<Role>;

  ngOnInit() {
    this.healthCheck.checkBackend(
      (data: string) => {
        this.getRolesList();
      }
    );
  }

  getRolesList() {
    this.querySubscription = this.graphql.getRolesList().
      subscribe(({ data, loading }) => {
        this.loading = loading;
        this.roles = data.rolesList.roles;
        this.table.renderRows();
      });
  }

  ngOnDestroy() {
    this.querySubscription.unsubscribe();
  }
}
