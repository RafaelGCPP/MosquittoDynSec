import { Component, ViewChild } from '@angular/core';
import { Apollo, gql } from 'apollo-angular';
import { catchError, map, of, Subscription } from 'rxjs';
import { Client } from '../model/client';
import { MatTable, MatTableModule } from '@angular/material/table';
import { HttpClient } from '@angular/common/http';

import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';



const clientslistQuery =
gql`query {
  clientsList {
    totalCount
    clients {
      ...clientdata
    }
  }
}

fragment clientdata on Client {
      textDescription
      textName
      userName
      roles {
        roleName
        priority
      }
      groups {
        groupName
        priority
      }
}`;

@Component({
  selector: 'dynsec-clients-list',
  imports: [MatTableModule, MatButtonModule, MatIconModule],
  templateUrl: './clients-list.component.html',
  styleUrl: './clients-list.component.scss'
})
export class ClientsListComponent {

  private querySubscription!: Subscription;
  loading: boolean = true;
  clients: Client[]=[];

  displayedColumns: string[] = ['userName', 'textName','textDescription', 'editButton'];

  constructor(private readonly apollo: Apollo, private readonly http:HttpClient) { }
  @ViewChild(MatTable)
    table!: MatTable<Client>;

  ngOnInit() {
    this.http.get('/health', { responseType: "text" }).pipe
      (
        map(
          (response: any) => {
            this.getClientList();
            return "";
          }
        ),
        catchError(
          error => {
            console.error("Error in health check: " + error);
            console.error("Backend is not ready!");
            return of("");
          }
        )
    ).subscribe();
    

  }

  getClientList() {
    this.querySubscription = this.apollo
      .watchQuery<any>({
        query: clientslistQuery,
      })
      .valueChanges.subscribe(({ data, loading }) => {
        this.loading = loading;
        this.clients = data.clientsList.clients;
        this.table.renderRows();
      });
  }

  ngOnDestroy() {
    this.querySubscription.unsubscribe();
  }

}
