import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { DisplayClientDto, DisplayDirectorDto, DisplayWorkerDto } from '../../../models/userDTO';
import { AuthorisationService } from '../../../services/authorisationService';
import { UserService } from '../../../services/userService';

@Component({
  selector: 'app-all-equipment',
  templateUrl: './all-equipment.component.html',
  styleUrl: './all-equipment.component.css'
})
export class AllEquipmentComponent {

  user!: DisplayWorkerDto | DisplayClientDto | DisplayDirectorDto; 


   constructor(private router: Router,
              private userService : UserService,
              private authService : AuthorisationService,
   ) {}
  
    navigateToAddEquipment() {
      this.router.navigate(['/add-equipments']);
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
