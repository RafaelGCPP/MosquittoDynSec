import { Injectable } from "@angular/core";
import { gql } from "@apollo/client/core";
import { Apollo } from "apollo-angular";

const groupslistQuery =
  gql`query {
  groupsList {
    groups {
      groupName
    }
  }
}`;

const groupQuery =
  gql`query Group($groupName: String!) {
  group(group: $groupName) {
    group {
      groupName
      textDescription
      textName
      clients {
        userName
      }
      roles {
        roleName
        priority
      }
    }
  }
}`;

const rolesAndClientsQuery =
  gql`query {
  rolesList {
    roles {
      roleName
    }
  }
  clientsList {
    clients {
      userName
      disabled
    }
  }
}`;


//const roleUpdateMutation =
//  gql`mutation modifyRole ($role: RoleACLInput!) {
//  modifyRole(role: $role) 
//}`;

//const createRoleMutation =
//  gql`mutation createRole ($role: RoleACLInput!) {
//  createRole(newrole: $role) 
//}`;

//const deleteRoleMutation =
//  gql`mutation deleteRole ($roleName: String!) {
//  deleteRole(role: $roleName) 
//}`;

@Injectable({ providedIn: 'root', })
export class GroupsGraphqlService {
  constructor(private readonly apollo: Apollo) { }
  getGroupsList() {
    return this.apollo
      .watchQuery<any>({
        query: groupslistQuery,
      })
      .valueChanges;
  }

  getGroup(groupName: string) {
    return this.apollo
      .watchQuery<any>({
        query: groupQuery,
        variables: {
          groupName: groupName
        }
      })
      .valueChanges;
  }

  getRolesAndClients() {
    return this.apollo
      .watchQuery<any>({
        query: rolesAndClientsQuery,
      })
      .valueChanges;
  }


  createGroup(group: any, actions?: any) {
    //return this.runMutation(createRoleMutation, role, actions);
  }

  updateGroup(group: any, actions?: any) {
    //return this.runMutation(roleUpdateMutation, role, actions);
  }

  runMutation(mutation: any, role: any, actions?: any) {
    //return this.apollo
    //  .mutate({
    //    mutation: mutation,
    //    variables: {
    //      role: role
    //    },
    //    refetchQueries: [
    //      {
    //        query: roleslistQuery,
    //        fetchPolicy: 'network-only',
    //        variables: {},
    //      },
    //      {
    //        query: roleQuery,
    //        fetchPolicy: 'network-only',
    //        variables: {
    //          roleName: role.roleName
    //        }
    //      }
    //    ]

    //  }).subscribe(actions);
  }

  deleteGroup(groupName: string, actions?: any) {
    //return this.apollo
    //  .mutate({
    //    mutation: deleteRoleMutation,
    //    variables: {
    //      roleName: roleName
    //    },
    //    refetchQueries: [
    //      {
    //        query: roleslistQuery,
    //        fetchPolicy: 'network-only',
    //        variables: {},
    //      }
    //    ]
    //  }).subscribe(actions);
  }

  refresh() {
    this.apollo.client.resetStore();
  }
}
