import { Component, OnInit } from '@angular/core';
import { Motor, MotorInfo } from '../../../models/motorDTO';
import { ActivatedRoute, Router } from '@angular/router';
import { MotorService } from '../../../services/motorServices';
import { MatDialog } from '@angular/material/dialog';
import { DialogComponent } from '../../dialog/dialog.component';
import { DisplayClientDto, DisplayDirectorDto, DisplayWorkerDto, User } from '../../../models/userDTO';
import { UserService } from '../../../services/userService';
import { AuthorisationService } from '../../../services/authorisationService';

@Component({
  selector: 'app-get-motors-by-id',
  templateUrl: './get-motors-by-id.component.html',
  styleUrl: './get-motors-by-id.component.css'
})
export class GetMotorsByIdComponent implements OnInit {
  motor!: Motor;
  loading = true; // Add a loading flag
  user!: DisplayWorkerDto | DisplayClientDto | DisplayDirectorDto; 


  constructor(
    private route: ActivatedRoute,
    private motorService: MotorService,
    private userService: UserService,
    private dialog: MatDialog,
    private router: Router,
    private authService : AuthorisationService
  ) {}

  ngOnInit(): void {
    const motorId = this.route.snapshot.paramMap.get('id');
    if (motorId !== null) {
      console.log('Motor ID from route:', motorId);
      this.getMotorById(motorId.toString());
    } else {
      console.error('Invalid motor ID');
    }

    const userId = this.authService.getUserId();
    if (userId !== null) {
      
      this.getUserById(userId);  
      
    } else {
      console.error('Invalid User ID');
    }
  }

  getMotorById(id: string): void {
    this.motorService.getMotorById(id).subscribe({
      next: (res) => {
        this.motor = res;
        if(this.motor.slika == '' || this.motor.slika == null) this.motor.slika ='https://placehold.co/700x300/EEE/31343C?text=300 x 300';
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

  getUserById(id: string): void {
    this.userService.getUserById(id).subscribe({
      next: (res) => {
        this.user = res;
        this.loading = false;
        console.log(res);
        console.log('KORISNIK JE:', res);
      },
      error: (err) => {
        console.error('Error fetching motor details:', err);
        this.loading = false; 
      }
    });
}


editMotor(motorId: string): void {
  this.router.navigate(['/edit-motorcycles', motorId]);
}

buyMotor(motorId: string): void {
  this.router.navigate(['/buy-items', motorId]);
}


isDisplayWorker(user: DisplayWorkerDto | DisplayClientDto | DisplayDirectorDto | undefined): user is DisplayWorkerDto {
  return user !== undefined && user.role === 'RADNIK';
}

isDisplayClient(user: DisplayWorkerDto | DisplayClientDto | DisplayDirectorDto | undefined): user is DisplayClientDto {
  return user !== undefined && user.role === 'KLIJENT';
}

isDisplayDirector(user: DisplayWorkerDto | DisplayClientDto | DisplayDirectorDto | undefined): user is DisplayDirectorDto {
  return user !== undefined && user.role === 'DIREKTOR';
}


}
