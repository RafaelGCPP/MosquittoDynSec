import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { ActivatedRoute, Router } from '@angular/router';
import { NavBarService } from '../../navbar/navbar.service';
import { ClientsGraphqlService } from '../clients.graphql.service';

import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatSelectModule } from '@angular/material/select';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Subscription } from 'rxjs';
import { Client } from '../../model/client';
import { ItemPriority, PriorityListComponent } from '../../priority-list/priority-list.component';
import { ApolloError } from '@apollo/client/core';
import { GraphQLFormattedError } from 'graphql';

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
  originalClient: Client = {
    ...this.client
  };
  allGroups: string[] = [];
  allRoles: string[] = [];
  newRolePriority: number = 0;
  newGroupPriority: number = 0;
  rolesChanged: boolean = false;
  groupsChanged: boolean = false;

  private querySubscription!: Subscription;
  private rolesAndGroupsSubscription!: Subscription;
  private paramSubscription!: Subscription;

  selectedRoles: ItemPriority[] = [];
  selectedGroups: ItemPriority[] = [];

  constructor(
    private readonly route: ActivatedRoute,
    private readonly navBar: NavBarService,
    private readonly graphql: ClientsGraphqlService,
    private readonly snack: MatSnackBar,
    private readonly router: Router
  ) {
  }

  ngOnInit() {
    this.paramSubscription = this.route.paramMap.subscribe(params => {
      let userName = params.get('userName');
      this.updateView(userName);
      console.log("Loading client detail for " + userName);
    });

  }

  private updateView(userName: string | null) {

    if (userName) {
      this.userName = userName;
    }

    if (this.userName === '') {
      this.mode = 'new';
    } else {
      this.mode = 'edit';
      this.querySubscription = this.graphql.getClient(this.userName).subscribe(result => {
        this.client = this.addPassword(result.data.client.client);
        this.rolesChanged = false;
        this.groupsChanged = false;
        this.originalClient = {
          ...this.client
        };
      });
    }

    this.rolesAndGroupsSubscription = this.graphql.getRolesAndGroups().subscribe(result => {
      this.allRoles = result.data.rolesList.roles.map((x: any) => x.roleName);
      this.allGroups = result.data.groupsList.groups.map((x: any) => x.groupName);
      this.updateSelectedItems();
    });

    this.navBar.closeSidenav();
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

  private createChangeset() {
    const changeset: any = { userName: this.client.userName };
    if (this.client.textName !== this.originalClient.textName) {
      changeset.textName = this.client.textName;
    }
    if (this.client.textDescription !== this.originalClient.textDescription) {
      changeset.textDescription = this.client.textDescription;
    }
    if (this.client.password !== this.originalClient.password) {
      changeset.password = this.client.password;
    }
    if (this.rolesChanged) {
      changeset.roles = this.selectedRoles.map((item) => ({ roleName: item.name, priority: item.priority }));
    }
    if (this.groupsChanged) {
      changeset.groups = this.selectedGroups.map((item) => ({ groupName: item.name, priority: item.priority }));
    }

    return changeset;
  }

  saveClient() {
    console.log("changed!");
    const changeset = this.createChangeset();

    console.log(changeset);
    console.log(this.mode);

    if (this.mode === 'new') {
      if ((changeset.userName === '') || (!changeset.password) || (changeset.password === '')) {
        console.error("Invalid input: missing username and/or password");

        this.snack.open("Username and password must be supplied.", "Close", {
          duration: 15000
        });

        return;
      }
      const password: string = changeset.password;
      delete changeset.password;

      const action = {
        next: (data: any) => {
          console.log(data);
          this.router.navigate([`../${changeset.userName}`], { relativeTo: this.route });
        },
        error: (error: ApolloError) => {
          console.error(error);
          console.error('Mutation error: ', error.message);
          this.snack.open(`${error.message}`, "Close", {
            duration: 15000
          });
        }
      }

      this.graphql.createClient(changeset, password, action);

    } else {
      const action = {
        next: (data: any) => {
          console.log(data);          
        },
        error: (error: ApolloError) => {
          console.error(error);
          console.error('Mutation error: ', error.message);
          this.snack.open(`${error.message}`, "Close", {
            duration: 15000
          });
        }
      }
      this.graphql.updateClient(changeset, changeset.password, action);
    }
  }

  toggleUserState() {
    const enable: boolean = this.client.disabled ? true : false;
    const action = {
      next: (data: any) => {
        console.log(data);
      },
      error: (error: ApolloError) => {
        console.error(error);
        console.error('Mutation error: ', error.message);
        this.snack.open(`${error.message}`, "Close", {
          duration: 15000
        });
      }
    }
    this.graphql.setState(this.userName, enable, action);
  }

  ngOnDestroy() {
    if (this.mode != 'new') {
      this.querySubscription.unsubscribe();
    }
    this.paramSubscription.unsubscribe();
    this.rolesAndGroupsSubscription.unsubscribe();
  }
}
