import { Acl } from "./acl";

export interface Role {
  roleName: string;
  textName?: string;
  textDescription?: string;
  priority?: number;
  acLs?: Acl[];
}
