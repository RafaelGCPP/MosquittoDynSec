import { Injectable } from "@angular/core";
import { gql } from "@apollo/client/core";
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

  setState(userName: string, enabled: boolean) {
    if (enabled) {
      this.enableClient(userName).subscribe();
    } else {
      this.disableClient(userName).subscribe();
    }
  }

  enableClient(userName: string) {
    return this.apollo.mutate({
      mutation: enableUserMutation,
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
    });
  }

  disableClient(userName: string) {
    return this.apollo.mutate({
      mutation: disableUserMutation,
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
    });
  }
}
