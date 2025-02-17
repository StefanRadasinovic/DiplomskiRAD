import { Component } from '@angular/core';
import { DialogComponent } from '../delete-dialog/dialog.component';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-review-dialog',
  templateUrl: './review-dialog.component.html',
  styleUrl: './review-dialog.component.css'
})
export class ReviewDialogComponent {

  comment : string = "";
  grade : number= 0;
  
    constructor(public dialogRef: MatDialogRef<DialogComponent>) {}
      
       onConfirm(): void {
        this.dialogRef.close({ comment: this.comment, grade : this.grade  });  
      }
      
        onCancel(): void {
          this.dialogRef.close(false);
        }

}
