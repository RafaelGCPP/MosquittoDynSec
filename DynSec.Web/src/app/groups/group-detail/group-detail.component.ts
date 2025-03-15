import { Component, inject } from '@angular/core';
import { GroupsGraphqlService } from '../groups.graphql.service';
import { ActivatedRoute, Router } from '@angular/router';
import { NavBarService } from '../../navbar/navbar.service';
import { FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { Group } from '../../model/group';
import { Subscription } from 'rxjs';
import { ItemPriority, PriorityListComponent } from '../../priority-list/priority-list.component';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ApolloError } from '@apollo/client/errors';
import { DeleteDialog } from '../../delete-dialog/delete-dialog';

@Component({
  selector: 'dynsec-group-detail',
  imports: [
    FormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatSlideToggleModule,
    MatButtonModule,
    MatIconModule,
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
    clients: []
  };
  originalGroup: Group = {
    ...this.group
  };
  mode = 'new';
  rolesChanged: boolean = false;
  clientsChanged: boolean = false;
  allRoles: string[] = [];
  allClients: string[] = [];
  selectedRoles: ItemPriority[] = [];
  selectedClients: ItemPriority[] = [];

  private paramSubscription!: Subscription;
  private rolesAndGroupsSubscription!: Subscription;
  private querySubscription!: Subscription;

  private readonly snack = inject(MatSnackBar);
  private readonly router = inject(Router);
  private readonly dialog = inject(MatDialog);

  private readonly route = inject(ActivatedRoute);
  private readonly navBar = inject(NavBarService);
  private readonly graphql = inject(GroupsGraphqlService);

  defaultAction = {
    next: console.log,
    error: this.displayError
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
      this.allClients = result.data.clientsList.clients.map((x: any) => x.userName);
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
    this.selectedClients = [];

    if (this.group.roles) {
      this.selectedRoles = this.group.roles.map(
        (role) =>
          ({ name: role.roleName, priority: (role.priority) ? (role.priority) : 0 })
      );
    }
    if (this.group.clients) {
      this.selectedClients = this.group.clients.map(
        (client) =>
          ({ name: client.userName, priority: 1 })
      );
    }
  }

  private createChangeset() {
    const changeset: any = { groupName: this.group.groupName };
    if (this.group.textName !== this.originalGroup.textName) {
      changeset.textName = this.group.textName;
    }
    if (this.group.textDescription !== this.originalGroup.textDescription) {
      changeset.textDescription = this.group.textDescription;
    }
    if (this.rolesChanged) {
      changeset.roles = this.selectedRoles.map((item) => ({ roleName: item.name, priority: item.priority }));
    }
    if (this.clientsChanged) {
      changeset.clients = this.selectedClients.map((item) => (item.name));
    }

    return changeset;
  }

  saveGroup() {
    const changeset = this.createChangeset();
    if (this.mode === 'new') {
      if (changeset.groupName === '') {
        console.error("Invalid input: missing group name");

        this.snack.open("Group name must be supplied.", "Close", {
          duration: 15000
        });
        return;
      }

      const action = {
        next: (data: any) => {
          console.log(data);
          this.router.navigate([`../${changeset.groupName}`], { relativeTo: this.route });
        },
        error: (error: ApolloError) => {
          console.error(error);
          console.error('Mutation error: ', error.message);
          this.snack.open(`${error.message}`, "Close", {
            duration: 15000
          });
        }
      }

      this.graphql.createGroup(changeset, action);

    } else {
      this.graphql.updateGroup(changeset, this.defaultAction);
    }
  }

  deleteGroup() {
    const dialogRef = this.dialog.open(DeleteDialog, {
      data: { name: this.groupName, itemType: 'group' },
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result === 'confirm') {
        const action = {
          next: (data: any) => {
            console.log(data);
            this.router.navigateByUrl('/groups');
            this.graphql.refresh();
            this.navBar.openSidenav();
          },
          error: this.displayError
        }
        this.graphql.deleteGroup(this.groupName, action);
      }
    });
  }

  displayError(error: ApolloError) {
    console.error(error);
    console.error('Mutation error: ', error.message);
    this.snack.open(`${error.message}`, "Close", {
      duration: 15000
    });
  }

  ngOnDestroy() {
    if (this.mode != 'new') {
      this.querySubscription.unsubscribe();
    }
    this.rolesAndGroupsSubscription.unsubscribe();
    this.paramSubscription.unsubscribe();
  }
}
