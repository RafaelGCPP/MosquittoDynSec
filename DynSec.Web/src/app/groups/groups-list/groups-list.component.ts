import { Component, ViewChild } from '@angular/core';
import { Group } from '../../model/group';
import { GroupsGraphqlService } from '../groups.graphql.service';
import { AppHealthCheckService } from '../../app.health.service';
import { MatTable, MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { RouterLink } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'dynsec-groups-list',
  imports: [
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    RouterLink
  ],
  templateUrl: './groups-list.component.html',
  styleUrl: './groups-list.component.scss'
})
export class GroupsListComponent {

  groups: Group[] = [];
  private querySubscription!: Subscription;
  displayedColumns: string[] = ['groupName'];
  loading: boolean = true;

  constructor(
    private readonly graphql: GroupsGraphqlService,
    private readonly healthCheck: AppHealthCheckService
  ) { }


  @ViewChild(MatTable)
  table!: MatTable<Group>;

  ngOnInit() {
    this.healthCheck.checkBackend(
      (data: string) => {
        this.getGroupsList();
      }
    );
  }

  getGroupsList() {
    this.querySubscription = this.graphql.getGroupsList().
      subscribe(({ data, loading }) => {
        this.loading = loading;
        this.groups = data.groupsList.groups;
        this.table.renderRows();
      });
  }

  ngOnDestroy() {
    this.querySubscription.unsubscribe();
  }
}
