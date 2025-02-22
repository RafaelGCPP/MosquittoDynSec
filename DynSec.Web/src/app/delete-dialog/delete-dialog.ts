import { Component, inject } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { MatButtonModule } from "@angular/material/button";
import { MatDialogTitle, MatDialogContent, MatDialogActions, MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatInputModule } from "@angular/material/input";

export interface DeleteDialogData {
  name: string;
  itemType: string;
}

@Component({
  selector: 'dynsec-delete-dialog',
  templateUrl: 'delete-dialog.html',
  imports: [
    MatFormFieldModule,
    MatInputModule,
    FormsModule,
    MatButtonModule,
    MatDialogTitle,
    MatDialogContent,
    MatDialogActions,
  ],
})
export class DeleteDialog {
  private readonly dialogRef = inject(MatDialogRef<DeleteDialog>)
  readonly data = inject<DeleteDialogData>(MAT_DIALOG_DATA);

  confirmationString = '';

  onNoClick(): void {
    this.dialogRef.close();
  }
  confirm(): void {
    if (this.confirmationString === this.data.name) {
      this.dialogRef.close('confirm');
    }
  }
}

