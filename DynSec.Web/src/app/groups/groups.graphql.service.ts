import { Injectable } from "@angular/core";
import { gql } from "@apollo/client/core";
import { Apollo } from "apollo-angular";

const groupslistQuery =
  gql`query {
  groupsList {
    groups {
      groupsName
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
    }
  }
}`;

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

  
}
