import { Component, model } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatTable, MatTableModule } from '@angular/material/table';
import { Acl, ACLtype } from '../../model/acl';
import { MatCheckboxModule } from '@angular/material/checkbox';

@Component({
  selector: 'dynsec-acl-list',
  imports: [
    FormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule,
    MatIconModule,
    MatTable,
    MatTableModule,
    MatCheckboxModule,
  ],
  templateUrl: './acl-list.component.html',
  styleUrl: './acl-list.component.scss'
})
export class AclListComponent {
  displayedColumns = ['permission','topic', 'allow', 'priority', 'buttons'];
  selectedValues = model<Acl[]>([]);
  aclTypes = Object.values(ACLtype);

  addItem() {
  }
}
