import { Component } from '@angular/core';
import { GroupsGraphqlService } from '../groups.graphql.service';
import { ActivatedRoute } from '@angular/router';
import { NavBarService } from '../../navbar/navbar.service';
import { FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { Group } from '../../model/group';
import { Subscription } from 'rxjs';
import { ItemPriority, PriorityListComponent } from '../../priority-list/priority-list.component';

@Component({
  selector: 'dynsec-group-detail',
  imports: [
    FormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatSlideToggleModule,
    PriorityListComponent
  ],
  templateUrl: './group-detail.component.html',
  styleUrl: './group-detail.component.scss'
})
export class GroupDetailComponent {
  groupName = '';
  group: Group = {
    groupName: '',
    textName: '',
    textDescription: '',
    roles: [],
    clients:[]
  }
  mode = 'new';
  rolesChanged: boolean = false;
  clientsChanged: boolean = false;
  allRoles: string[] = [];
  allClients: string[] = [];
  selectedRoles: ItemPriority[] = [];

  private paramSubscription!: Subscription;
  private rolesAndGroupsSubscription!: Subscription;
  private querySubscription!: Subscription;

  constructor(
    private readonly route: ActivatedRoute,
    private readonly navBar: NavBarService,
    private readonly graphql: GroupsGraphqlService
  ) {
  }

  ngOnInit() {
    this.paramSubscription = this.route.paramMap.subscribe(params => {
      let groupName = params.get('groupName');

      this.updateView(groupName);
      console.log("Loading group detail for " + groupName);

    });


  }


  private updateView(groupName: string | null) {
    if (groupName) {
      this.groupName = groupName;
    }

    if (this.groupName === '') {
      this.mode = 'new';
    } else {
      this.mode = 'edit';
      this.querySubscription = this.graphql.getGroup(this.groupName).subscribe(result => {

        this.group = this.normalizeGroup(result.data.group.group);
        this.clientsChanged = false;
        this.rolesChanged = false;
        this.updateSelectedItems();

        console.log(this.group);

      });
    }

    this.rolesAndGroupsSubscription = this.graphql.getRolesAndClients().subscribe(result => {
      this.allRoles = result.data.rolesList.roles.map((x: any) => x.roleName);
      this.allClients = result.data.clientsList.groups.map((x: any) => x.groupName);
    });

    this.navBar.closeSidenav();

  }

  private normalizeGroup(group: any): Group {
    return {
      ...group,
    };
  }


  private updateSelectedItems() {
    this.selectedRoles = [];
    //this.selectedGroups = [];

    if (this.group.roles) {
      this.selectedRoles = this.group.roles.map(
        (role) =>
          ({ name: role.roleName, priority: (role.priority) ? (role.priority) : 0 })
      );
    }
    //if (this.client.groups) {
    //  this.selectedGroups = this.client.groups.map(
    //    (group) =>
    //      ({ name: group.groupName, priority: (group.priority) ? (group.priority) : 0 })
    //  );
    //}
  }


  ngOnDestroy() {
    if (this.mode != 'new') {
      this.querySubscription.unsubscribe();
    }
    this.rolesAndGroupsSubscription.unsubscribe();
    this.paramSubscription.unsubscribe();
  }
}
