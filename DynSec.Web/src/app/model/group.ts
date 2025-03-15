import { Role } from "./role";

export interface Group {
  groupName: string;
  priority?: number;
  textName?: string;
  textDescription?: string;
  roles?: Role[];
  clients?: string[];
}
