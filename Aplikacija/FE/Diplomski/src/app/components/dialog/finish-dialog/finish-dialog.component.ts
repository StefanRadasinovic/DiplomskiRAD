import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { DialogComponent } from '../delete-dialog/dialog.component';

@Component({
  selector: 'app-finish-dialog',
  templateUrl: './finish-dialog.component.html',
  styleUrl: './finish-dialog.component.css'
})
export class FinishDialogComponent {

  name : string = "";
  amount : number= 0;

  constructor(public dialogRef: MatDialogRef<DialogComponent>) {}
    
     onConfirm(): void {
      this.dialogRef.close({ name: this.name, amount : this.amount  });  
    }
    
      onCancel(): void {
        this.dialogRef.close(false);
      }
}
