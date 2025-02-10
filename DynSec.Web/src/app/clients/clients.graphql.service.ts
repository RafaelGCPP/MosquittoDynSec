import { Injectable } from "@angular/core";
import { gql } from "@apollo/client/core";
import { Apollo } from "apollo-angular";

export const clientslistQuery =
  gql`query {
  clientsList {
    totalCount
    clients {
      userName
      disabled
    }
  }
}`;

export const enableUserMutation =
  gql`mutation EnableClient($userName: String!) {
  enableClient(client:$userName)
}`;

export const disableUserMutation =
  gql`mutation DisableClient($userName: String!) {
  disableClient(client:$userName)
}`;

@Injectable({ providedIn: 'root', })
export class ClientsGraphqlService {
  constructor(private readonly apollo: Apollo) { }
  getClientList() {
    return this.apollo
      .watchQuery<any>({
        query: clientslistQuery,
      })
      .valueChanges;
  }
  enableClient(userName: string) {
    return this.apollo.mutate({
      mutation: enableUserMutation,
      variables: {
        userName: userName
      },
      refetchQueries: [{
        query: clientslistQuery,
        fetchPolicy: 'network-only',
        variables: {},
      }]
    });
  }
  disableClient(userName: string) {
    return this.apollo.mutate({
      mutation: disableUserMutation,
      variables: {
        userName: userName
      },
      refetchQueries: [{
        query: clientslistQuery,
        fetchPolicy: 'network-only',
        variables: {},
      }]
    });
  }
}
