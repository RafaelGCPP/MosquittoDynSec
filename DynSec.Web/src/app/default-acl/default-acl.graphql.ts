import { inject, Injectable } from "@angular/core";
import { gql } from "@apollo/client/core";
import { Apollo } from "apollo-angular";


const getDefaultAclQuery =
  gql`query getDefaultAcl {
  defaultACL {
    acLs {
      aclType
      allow
    }
  }
}`;

const setDefaultAclMutation = gql`mutation setDefaultAcl($defaultAcls:[DefaultACLInput!]!) {
  setDefaultACLs(data: $defaultAcls)
}`;


@Injectable({ providedIn: 'root', })
export class DefaultAclsGraphqlService {
  private readonly apollo = inject(Apollo);

  getDefaultAcl() {
    return this.apollo
      .watchQuery<any>({
        query: getDefaultAclQuery,
      })
      .valueChanges;
  }

  setDefaultAcl(acls: any, actions?: any) {
    this.apollo
      .mutate({
        mutation: setDefaultAclMutation,
        variables:
        {
          defaultAcls: acls
        },
        refetchQueries: [
          {
            query: getDefaultAclQuery,
            fetchPolicy: 'network-only',
            variables: {}
          }
        ]
      }).subscribe(actions);
  }
}
