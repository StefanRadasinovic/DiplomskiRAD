import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { DialogComponent } from '../delete-dialog/dialog.component';

@Component({
  selector: 'app-accept-dialog',
  templateUrl: './accept-dialog.component.html',
  styleUrl: './accept-dialog.component.css'
})
export class AcceptDialogComponent {

  constructor(public dialogRef: MatDialogRef<DialogComponent>) {}
  
    onConfirm(): void {
      this.dialogRef.close(true);
    }
  
    onCancel(): void {
      this.dialogRef.close(false);
    }

}
