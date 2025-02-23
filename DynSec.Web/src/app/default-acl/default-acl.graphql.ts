import { inject, Injectable } from "@angular/core";
import { gql } from "@apollo/client/core";
import { Apollo } from "apollo-angular";


const getDefaultAclQuery =
  gql`query defacl {
  defaultACL {
    acLs {
      aclType
      allow
    }
  }
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
}
