import { Component, model, output } from '@angular/core';
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
  modified = output<void>();
  
  aclTypes = Object.values(ACLtype);
  newValue = {
    aclType: ACLtype.subscribe,
    topic: '#',
    allow: 'true',
    priority: 0
  };

  addItem() {
    const acl: Acl = {
      aclType: this.newValue.aclType,
      topic: this.newValue.topic,
      allow: this.newValue.allow === 'true',
      priority: this.newValue.priority
    };
    this.selectedValues.update((acls) => [...acls, acl]);
  }

  removeItem(acl: Acl) {
    this.newValue = {
      aclType: acl.aclType,
      topic: acl.topic,
      allow: acl.allow ? 'true' : 'false',
      priority: acl.priority
    }
    
    this.selectedValues.update((acls)=> acls.filter(a => a !== acl));
   
  }
}
