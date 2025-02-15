import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NavBarService } from '../../navbar/navbar.service';
import { ClientsGraphqlService } from '../clients.graphql.service';
import { FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';

import { Client } from '../../model/client';
import { Subscription } from 'rxjs';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { ItemPriority, PriorityListComponent } from '../../priority-list/priority-list.component';

@Component({
  selector: 'dynsec-client-detail',
  imports: [
    FormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatSlideToggleModule,
    MatSelectModule,
    MatButtonModule,
    MatIconModule,
    PriorityListComponent
  ],
  templateUrl: './client-detail.component.html',
  styleUrl: './client-detail.component.scss'
})
export class ClientDetailComponent {

  mode = '';
  userName = '';
  data: any;
  client: Client = {
    userName: '',
    password: '',
    textName: '',
    textDescription: '',
    roles: [],
    groups: []
  };
  allGroups: string[] = [];
  allRoles: string[] = [];
  newRolePriority: number = 0;
  newGroupPriority: number = 0;
  private querySubscription!: Subscription;
  private rolesAndGroupsSubscription!: Subscription;
  private paramSubscription!: Subscription;

  selectedRoles: ItemPriority[] = [];
  selectedGroups: ItemPriority[] = [];

  constructor(
    private readonly route: ActivatedRoute,
    private readonly navBar: NavBarService,
    private readonly graphql: ClientsGraphqlService
  ) {
  }

  ngOnInit() {
    this.paramSubscription = this.route.paramMap.subscribe(params => {
      let userName = params.get('userName');
      if (userName) {
        this.userName = userName;
        this.navBar.closeSidenav();
      }
    });

    if (this.userName === '') {
      this.mode = 'new';
    } else {
      this.mode = 'edit';
      this.querySubscription = this.graphql.getClient(this.userName).subscribe(result => {
        this.client = this.addPassword(result.data.client.client);
      });
    }

    this.rolesAndGroupsSubscription = this.graphql.getRolesAndGroups().subscribe(result => {
      this.allRoles = result.data.rolesList.roles.map((x: any) => x.roleName);
      this.allGroups = result.data.groupsList.groups.map((x: any) => x.groupName);
      this.updateSelectedItems();
    });
  }

  private updateSelectedItems() {
    this.selectedRoles = [];
    this.selectedGroups = [];

    if (this.client.roles) {
      this.selectedRoles = this.client.roles.map(
        (role) =>
          ({ name: role.roleName, priority: (role.priority) ? (role.priority) : 0 })
      );
    }
    if (this.client.groups) {
      this.selectedGroups = this.client.groups.map(
        (group) =>
          ({ name: group.groupName, priority: (group.priority) ? (group.priority) : 0 })
      );
    }
  }

  private addPassword(client: any): Client {
    return {
      ...client,
      password: ''
    };
  }

  saveClient() {
    console.log("changed!");
  }

  toggleUserState() {
    const enable: boolean = this.client.disabled ? true : false;
    this.graphql.setState(this.userName, enable);
  }

  ngOnDestroy() {
    this.querySubscription.unsubscribe();
    this.paramSubscription.unsubscribe();
    this.rolesAndGroupsSubscription.unsubscribe();
  }
}
