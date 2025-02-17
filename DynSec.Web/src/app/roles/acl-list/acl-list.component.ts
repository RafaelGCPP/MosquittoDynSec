import { Component, model } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatTable, MatTableModule } from '@angular/material/table';
import { Acl } from '../../model/acl';

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
  ],
  templateUrl: './acl-list.component.html',
  styleUrl: './acl-list.component.scss'
})
export class AclListComponent {
  displayedColumns = ['permission','topic', 'allow', 'priority', 'buttons'];
  selectedValues = model<Acl[]>([]);

}
