import { HttpClient } from '@angular/common/http';
import { Component, ViewChild } from '@angular/core';
import { MatTable, MatTableModule } from '@angular/material/table';
import { Apollo, gql } from 'apollo-angular';
import { catchError, map, of, retry, retryWhen, Subscription } from 'rxjs';
import { Client } from '../model/client';

import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';

const clientslistQuery =
gql`query {
  clientsList {
    totalCount
    clients {
      userName
      disabled
    }
  }
}`;

const enableUserMutation =
gql`mutation EnableClient($userName: String!) {
  enableClient(client:$userName)
}`;

const disableUserMutation =
gql`mutation DisableClient($userName: String!) {
  disableClient(client:$userName)
}`;

@Component({
  selector: 'dynsec-clients-list',
  imports: [MatTableModule, MatButtonModule, MatIconModule,MatSnackBarModule],
  templateUrl: './clients-list.component.html',
  styleUrl: './clients-list.component.scss'
})
export class ClientsListComponent {

  private querySubscription!: Subscription;
  loading: boolean = true;
  clients: Client[]=[];

  displayedColumns: string[] = ['userName','disableButton'];

  constructor(private readonly apollo: Apollo, private readonly http: HttpClient, private readonly snack:MatSnackBar) { }
  @ViewChild(MatTable)
    table!: MatTable<Client>;

  ngOnInit() {
    this.http.get('/health', { responseType: "text" })
      .pipe(
        retry({
          count: 5, delay: 1000
        }),
      )
      .subscribe(
        {
          next: (response: any) => {
            this.getClientList();
          },
          error: (error: any) => {
            console.error("Backend not available: " + error)
            this.snack.open("Backend not available.", "Close", {
              duration: 5000
            });
          },
          complete: () => {
            console.log("Backend healthy!");
          }
        }
      );
  }

  disableClient(username: string, disabled: boolean) {
    if (disabled) {
      this.apollo.mutate({
        mutation: disableUserMutation,
        variables: {
          userName: username
        },
        refetchQueries: [{
          query: clientslistQuery,
          fetchPolicy: 'network-only',
          variables: {},
        }]
      }).subscribe();
    } else {
      this.apollo.mutate({
        mutation: enableUserMutation,
        variables: {
          userName: username
        },
        refetchQueries: [{
          query: clientslistQuery,
          fetchPolicy: 'network-only',
          variables: {},
        }]
      }).subscribe();
    }

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
