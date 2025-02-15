import { Role } from "./role";
import { Group } from "./group"; 

export  interface Client {
  textDescription?: string;
  textName?: string;
  userName: string;
  disabled?: boolean;
  roles?: Role[];
  groups?: Group[];
  password?: string;
};
