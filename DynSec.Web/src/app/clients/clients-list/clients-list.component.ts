import { Component, ViewChild } from '@angular/core';
import { MatTable, MatTableModule } from '@angular/material/table';
import { Subscription } from 'rxjs';
import { Client } from '../../model/client';

import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { RouterLink } from '@angular/router';
import { AppHealthCheckService } from '../../app.health.service';
import { ClientsGraphqlService } from '../clients.graphql.service';

@Component({
  selector: 'dynsec-clients-list',
  imports: [
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    RouterLink
  ],
  templateUrl: './clients-list.component.html',
  styleUrl: './clients-list.component.scss'
})
export class ClientsListComponent {

  private querySubscription!: Subscription;
  loading: boolean = true;
  clients: Client[]=[];

  displayedColumns: string[] = ['userName','disableButton'];

  constructor(
    private readonly graphql: ClientsGraphqlService,
    private readonly healthCheck: AppHealthCheckService
  ) { }


  @ViewChild(MatTable)
    table!: MatTable<Client>;

  ngOnInit() {
    this.healthCheck.checkBackend(
      (data: string) => {
        this.getClientList();
      }
    );
  }

  disableClient(username: string, disabled: boolean) {
    this.graphql.setState(username, !disabled);
  }

  getClientList() {
    this.querySubscription = this.graphql.getClientList().
      subscribe(({ data, loading }) => {
        this.loading = loading;
        this.clients = data.clientsList.clients;
        this.table.renderRows();
      });
  }

  ngOnDestroy() {
    this.querySubscription.unsubscribe();
  }

}
