import { Component, computed, input, model, ViewChild } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatTable, MatTableModule } from '@angular/material/table';


export interface ItemPriority {
  name: string;
  priority: number;
}



@Component({
  selector: 'dynsec-priority-list',
  imports: [
    FormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule,
    MatIconModule,
    MatTable,
    MatTableModule
  ],
  templateUrl: './priority-list.component.html',
  styleUrl: './priority-list.component.scss'
})
export class PriorityListComponent {
  selectedValues = model<ItemPriority[]>([]);
  allValues = input.required<string[]>();
  name = input.required<string>();
  newPriority = 0;
  newItem = '';

  @ViewChild(MatTable)
  table!: MatTable<ItemPriority>;
  displayedColumns = ['itemName', 'priority', 'removeButton'];

  availableOptions() {
    const selectedValues = this.selectedValues();
    if (selectedValues) {
      return this.allValues().filter(
        (value) => !selectedValues.map((item) => item.name).includes(value)
      );
    }
    else {
      return [];
    }
  }
      
  addItem() {
    if (this.newItem === '') return;

    this.selectedValues.update(
      (list) => {
        if (!list) {
          return [{ name: this.newItem, priority: this.newPriority }]
        }
        list.push({ name: this.newItem, priority: this.newPriority });
        list.sort((a, b) => a.priority - b.priority);
        return list;
      }
    );
    this.newItem = '';
    this.newPriority = 0;
    this.table.renderRows();
  }

  removeItem(item: string) {

    this.selectedValues.update(
      (list) => {
        if (!list) return [];
        const index = list.findIndex((value) => value.name === item);
        if (index !== -1) {
          this.newItem = item;
          this.newPriority = list[index].priority;
          list.splice(index, 1);
        }
        return list;
      }
    );
    
    this.table.renderRows();

  }
}
