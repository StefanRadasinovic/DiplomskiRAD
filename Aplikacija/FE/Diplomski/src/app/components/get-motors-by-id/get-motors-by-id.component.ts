import { Component, OnInit } from '@angular/core';
import { MotorInfo } from '../../models/motorDTO';
import { ActivatedRoute, Router } from '@angular/router';
import { MotorService } from '../../services/motorServices';
import { MatDialog } from '@angular/material/dialog';
import { DialogComponent } from '../dialog/dialog.component';

@Component({
  selector: 'app-get-motors-by-id',
  templateUrl: './get-motors-by-id.component.html',
  styleUrl: './get-motors-by-id.component.css'
})
export class GetMotorsByIdComponent implements OnInit {
  motor!: MotorInfo;
  loading = true; // Add a loading flag

  constructor(
    private route: ActivatedRoute,
    private motorService: MotorService,
    private dialog: MatDialog,
    private router: Router
  ) {}

  ngOnInit(): void {
    const motorId = this.route.snapshot.paramMap.get('id');
    if (motorId !== null) {
      console.log('Motor ID from route:', motorId);
      this.getMotorById(motorId.toString());
    } else {
      console.error('Invalid motor ID');
    }
  }

  getMotorById(id: string): void {
    this.motorService.getMotorById(id).subscribe({
      next: (res) => {
        this.motor = res;
        if(this.motor.slika == '') this.motor.slika ='https://placehold.co/300x300/EEE/31343C';
        this.loading = false; // Set loading to false after data is fetched
        console.log(res);
      },
      error: (err) => {
        console.error('Error fetching motor details:', err);
        this.loading = false; // Set loading to false on error as well
      }
    });
  }

  deleteMotor(): void {
    const dialogRef = this.dialog.open(DialogComponent);
  
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        const motorId = this.route.snapshot.paramMap.get('id');  
        if (motorId !== null) {
          this.motorService.delete(motorId).subscribe({
            next: () => {
              console.log('Motorcycle deleted successfully');
              this.router.navigate(['/all-motorcycles']);
            },
            error: (err) => console.error('Error deleting motorcycle:', err)
          });
        }
      }
    });
  }

  editMotor(motorId: string): void {
    this.router.navigate(['/edit-motorcycles', motorId]);
  }
}
