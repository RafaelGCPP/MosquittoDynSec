import { Injectable } from "@angular/core";
import { gql } from "@apollo/client/core";
import { Apollo } from "apollo-angular";

const roleslistQuery =
  gql`query {
  rolesList {
    roles {
      roleName
    }
  }
}`;

const roleQuery =
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

@Injectable({ providedIn: 'root', })
export class RolesGraphqlService {
  constructor(private readonly apollo: Apollo) { }
  getRolesList() {
    return this.apollo
      .watchQuery<any>({
        query: roleslistQuery,
      })
      .valueChanges;
  }

  getRole(userName: string) {
    return this.apollo
      .watchQuery<any>({
        query: roleQuery,
        variables: {
          userName: userName
        }
      })
      .valueChanges;
  }

  
}
