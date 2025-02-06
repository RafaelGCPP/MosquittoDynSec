import { Role } from "./role";
import { Group } from "./group"; 

export  interface Client {
  textDescription: string;
  textName: string;
  userName: string;
  roles: Role[];
  groups: Group[];
};
