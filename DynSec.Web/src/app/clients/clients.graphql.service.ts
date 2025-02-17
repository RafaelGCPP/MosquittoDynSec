import { Injectable } from "@angular/core";
import { MatSnackBar } from "@angular/material/snack-bar";
import { ApolloError, gql } from "@apollo/client/core";
import { Apollo } from "apollo-angular";

const clientslistQuery =
  gql`query {
  clientsList {
    clients {
      userName
      disabled
    }
  }
}`;

const clientQuery =
  gql`query Client($userName: String!) {
  client(client: $userName) {
    client {
      disabled
      textDescription
      textName
      userName
      groups {
        groupName
        priority
      }
      roles {
        priority
        roleName
      }
    }
  }
}`;

const rolesAndGroupsQuery =
  gql`query {
  rolesList {
    roles {
      roleName
    }
  }
  groupsList {
    groups {
      groupName
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

const updateClientMutation =
  gql`mutation UpdateClient($client: ClientInput!, $password: String) {
  modifyClient(client: $client, password: $password)
}`;

const createClientMutation =
  gql`mutation NewClient($client: ClientInput!, $password: String!) {
  createClient(newclient: $client, password: $password) 
}`
const deleteClientMutation =
  gql`mutation DeleteClient($userName: String!) {
  deleteClient(client: $userName)
}`

@Injectable({ providedIn: 'root', })
export class ClientsGraphqlService {
  constructor(
    private readonly apollo: Apollo,
    private readonly snack: MatSnackBar
  ) { }


  getClientList() {
    return this.apollo
      .watchQuery<any>({
        query: clientslistQuery,
      })
      .valueChanges;
  }

  getClient(userName: string) {
    return this.apollo
      .watchQuery<any>({
        query: clientQuery,
        variables: {
          userName: userName
        }
      })
      .valueChanges;
  }

  getRolesAndGroups() {
    return this.apollo
      .watchQuery<any>({
        query: rolesAndGroupsQuery,
      })
      .valueChanges;
  }

  setState(userName: string, enabled: boolean, actions?: any) {

    const mutation = (enabled) ? enableUserMutation : disableUserMutation;

    return this.apollo.mutate({
      mutation: mutation,
      variables: {
        userName: userName
      },
      refetchQueries: [
        {
          query: clientslistQuery,
          fetchPolicy: 'network-only',
          variables: {},
        },
        {
          query: clientQuery,
          fetchPolicy: 'network-only',
          variables: {
            userName: userName
          }
        }
      ]
    }).subscribe(actions);
  }


  updateClient(client: any, password: string, actions?: any) {
    return this.runMutation(updateClientMutation, client, password, actions);
  }

  createClient(client: any, password: string, actions?: any) {
    return this.runMutation(createClientMutation, client, password, actions);
  }

  private runMutation(mutation: any, client: any, password: string, actions?: any) {
    return this.apollo.mutate({
      mutation: mutation,
      variables: {
        client: client,
        password: password
      },
      refetchQueries: [
        {
          query: clientslistQuery,
          fetchPolicy: 'network-only',
          variables: {},
        },
        {
          query: clientQuery,
          fetchPolicy: 'network-only',
          variables: {
            userName: client.userName
          }
        }
      ]
    }).subscribe(actions);
  }

  deleteClient(userName: string, actions?: any) {
    return this.apollo.mutate({
      mutation: deleteClientMutation,
      variables: {
        userName: userName
      },
      refetchQueries: [
        {
          query: clientslistQuery,
          fetchPolicy: 'network-only',
          variables: {},
        },
      ]
    }).subscribe(actions);
  }

  refresh() {
    this.apollo.client.resetStore();
  }
}

