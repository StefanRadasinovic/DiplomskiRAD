import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../../../services/userService';
import { DisplayClientDto, DisplayDirectorDto, DisplayWorkerDto } from '../../../models/userDTO';
import { AuthorisationService } from '../../../services/authorisationService';

@Component({
  selector: 'app-all-motors',
  templateUrl: './all-motors.component.html',
  styleUrl: './all-motors.component.css'
})
export class AllMotorsComponent {

 user!: DisplayWorkerDto | DisplayClientDto | DisplayDirectorDto; 

  constructor(private router: Router,
              private userService : UserService,
              private authService : AuthorisationService,
  ) {}

  navigateToAddMotor() {
    this.router.navigate(['/add-motorcycles']);
  }



  ngOnInit(): void {
        
        const userId = this.authService.getUserId();
        if (userId !== null) {
          
          this.getUserById(userId);  
          
        } else {
          console.error('Invalid User ID');
        }
      
       
    }
  
      getUserById(id: string): void {
        this.userService.getUserById(id).subscribe({
          next: (res) => {
            this.user = res;
            console.log(res);
            console.log('KORISNIK JE:', res);
          },
          error: (err) => {
            console.error('Error fetching user details:', err);
          }
        });
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
