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
  gql`query Role($roleName: String!) {
  role(role: $roleName) {
    role {
      roleName
      textDescription
      textName
      acLs {
        aclType
        allow
        priority
        topic
      }
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

  getRole(roleName: string) {
    return this.apollo
      .watchQuery<any>({
        query: roleQuery,
        variables: {
          roleName: roleName
        }
      })
      .valueChanges;
  }

  refresh() {
    this.apollo.client.resetStore();
  }
}
