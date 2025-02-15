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
  ],
  templateUrl: './client-detail.component.html',
  styleUrl: './client-detail.component.scss'
})
export class ClientDetailComponent {

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
  private paramSubscription!: Subscription;

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

    this.querySubscription = this.graphql.getClient(this.userName).subscribe(result => {
      this.client = this.addPassword(result.data.client.client);
      this.allRoles = result.data.rolesList.roles.map((x: any) => x.roleName);
      this.allGroups = result.data.groupsList.groups.map((x: any) => x.groupName);
    });
  }

  private addPassword(client: any): Client {
    return {
      ...client,
      password: ''
    };
  }

  toggleUserState() {
    const enable: boolean = this.client.disabled ? true : false;
    this.graphql.setState(this.userName, enable);
  }

  ngOnDestroy() {
    this.querySubscription.unsubscribe();
    this.paramSubscription.unsubscribe();
  }
}
