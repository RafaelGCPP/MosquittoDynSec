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

const roleUpdateMutation =
  gql`mutation modifyRole ($role: RoleACLInput!) {
  modifyRole(role: $role) 
}`;

const createRoleMutation =
  gql`mutation createRole ($role: RoleACLInput!) {
  createRole(newrole: $role) 
}`;

const deleteRoleMutation =
  gql`mutation deleteRole ($roleName: String!) {
  deleteRole(role: $roleName) 
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

  createRole(role: any, actions?: any) {
    return this.runMutation(createRoleMutation, role, actions);
  }

  updateRole(role: any, actions?: any) {
    return this.runMutation(roleUpdateMutation, role, actions);
  }

  runMutation(mutation: any, role: any, actions?: any) {
    return this.apollo
      .mutate({
        mutation: mutation,
        variables: {
          role: role
        },
        refetchQueries: [
          {
            query: roleslistQuery,
            fetchPolicy: 'network-only',
            variables: {},
          },
          {
            query: roleQuery,
            fetchPolicy: 'network-only',
            variables: {
              roleName: role.roleName
            }
          }
        ]

      }).subscribe(actions);
  }

  deleteRole(roleName: string, actions?: any) {
    return this.apollo
      .mutate({
        mutation: deleteRoleMutation,
        variables: {
          roleName: roleName
        },
        refetchQueries: [
          {
            query: roleslistQuery,
            fetchPolicy: 'network-only',
            variables: {},
          }
        ]
      }).subscribe(actions);
  }

  refresh() {
    this.apollo.client.resetStore();
  }
}
