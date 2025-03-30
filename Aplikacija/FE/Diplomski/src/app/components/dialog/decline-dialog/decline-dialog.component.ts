import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { DialogComponent } from '../delete-dialog/dialog.component';

@Component({
  selector: 'app-decline-dialog',
  templateUrl: './decline-dialog.component.html',
  styleUrl: './decline-dialog.component.css'
})
export class DeclineDialogComponent {

  rejectionReason : string = "";

   constructor(public dialogRef: MatDialogRef<DialogComponent>) {}
  
   onConfirm(): void {
    this.dialogRef.close({ rejectionReason: this.rejectionReason });  
  }
  
    onCancel(): void {
      this.dialogRef.close(false);
    }
}
