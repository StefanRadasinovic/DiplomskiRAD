import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { DialogComponent } from '../delete-dialog/dialog.component';

@Component({
  selector: 'app-cancel-dialog',
  templateUrl: './cancel-dialog.component.html',
  styleUrl: './cancel-dialog.component.css'
})
export class CancelDialogComponent {

   constructor(public dialogRef: MatDialogRef<DialogComponent>) {}
  
    onConfirm(): void {
      this.dialogRef.close(true);
    }
  
    onCancel(): void {
      this.dialogRef.close(false);
    }
}
