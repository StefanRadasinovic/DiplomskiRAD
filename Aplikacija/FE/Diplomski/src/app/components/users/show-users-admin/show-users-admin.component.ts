
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from '../../../services/userService';
import { MatDialog } from '@angular/material/dialog';
import { DialogComponent } from '../../dialog/delete-dialog/dialog.component';
import { DisplayClientDto, DisplayDirectorDto, DisplayWorkerDto } from '../../../models/userDTO';
import { AuthorisationService } from '../../../services/authorisationService';

@Component({
  selector: 'app-show-users-admin',
  templateUrl: './show-users-admin.component.html',
  styleUrl: './show-users-admin.component.css'
})
export class ShowUsersAdminComponent implements OnInit {

user!: DisplayWorkerDto | DisplayClientDto | DisplayDirectorDto; 
loading = true; 
loggedUserInfo: any;  
userRole:  any;

constructor(
    private route: ActivatedRoute,
    private userService: UserService,
    private dialog: MatDialog,
    private router: Router,
    private authService : AuthorisationService
  ) {}


ngOnInit(): void {
  this.route.paramMap.subscribe(params => {
    const routeUserId = params.get('id'); // Get user ID from URL

    if (routeUserId) {
      this.getUserById(routeUserId);
    }
  });

  this.authService.userRole$.subscribe(role => {
    this.userRole = role;
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

deleteUser(): void {
    const dialogRef = this.dialog.open(DialogComponent);
  
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        const userId = this.route.snapshot.paramMap.get('id');  
        if (userId !== null) {
          this.userService.deleteUser(userId).subscribe({
            next: () => {
              console.log('User deleted successfully');
              this.router.navigate(['/all-users']);
            },
            error: (err) => console.error('Error deleting User:', err)
          });
        }
      }
    });
}

  editUser(motorId: string): void {
    this.router.navigate(['/edit-users', motorId]);
  }


  isDisplayWorker(user: DisplayWorkerDto | DisplayClientDto | DisplayDirectorDto): user is DisplayWorkerDto {
    return user.role === 'RADNIK'; 
  }

  isDisplayClient(user: DisplayWorkerDto | DisplayClientDto | DisplayDirectorDto): user is DisplayClientDto {
    return user.role === 'KLIJENT';
  }

  isDisplayDirector(user: DisplayWorkerDto | DisplayClientDto | DisplayDirectorDto): user is DisplayDirectorDto {
    return user.role === 'DIREKTOR';
  }
}