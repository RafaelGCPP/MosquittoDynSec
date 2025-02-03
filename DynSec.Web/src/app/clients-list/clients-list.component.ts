import { Component, ViewChild } from '@angular/core';
import { Apollo, gql } from 'apollo-angular';
import { Subscription } from 'rxjs';
import { Client } from '../model/client';
import { MatTable, MatTableModule } from '@angular/material/table';




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
  imports: [ MatTableModule ],
  templateUrl: './clients-list.component.html',
  styleUrl: './clients-list.component.scss'
})
export class ClientsListComponent {

  private querySubscription!: Subscription;
  loading: boolean = true;
  clients: Client[]=[];

  displayedColumns: string[] = ['userName'];

  constructor(private readonly apollo: Apollo) { }
  @ViewChild(MatTable)
    table!: MatTable<Client>;

  ngOnInit() {
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
