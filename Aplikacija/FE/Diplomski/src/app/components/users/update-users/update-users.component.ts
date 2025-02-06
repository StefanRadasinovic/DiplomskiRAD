import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from '../../../services/userService';
import { DisplayClientDto, DisplayDirectorDto, DisplayWorkerDto, UpdateClientDto, UpdateDirectorDto, UpdateWorkerDto } from '../../../models/userDTO';
import { AuthorisationService } from '../../../services/authorisationService';

@Component({
  selector: 'app-update-users',
  templateUrl: './update-users.component.html',
  styleUrl: './update-users.component.css'
})
export class UpdateUsersComponent implements OnInit {
  userForm!: FormGroup;
  userId!: string;
  loggedInUserRole!: string; 
  user!: DisplayWorkerDto | DisplayClientDto | DisplayDirectorDto; 
  updateMessage: string = '';
  errorMessage: string = '';

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private userService: UserService,
    private authService: AuthorisationService, 
    private router: Router
  ) {}

  ngOnInit(): void {
    this.userId = this.route.snapshot.paramMap.get('id')!;
    this.loggedInUserRole = this.authService.getUserRole(); 
    this.initializeForm();
    this.loadUserDetails();
  }

  initializeForm(): void {
    this.userForm = this.fb.group({
      name: ['', ],
      surname: ['',],
      username: ['',],
      password: [''],
      role:[''],
      salary: [null]
    });
  }

  loadUserDetails(): void {
    this.userService.getUserById(this.userId).subscribe({
      next: (user) => {
        if (!user) {
          console.error('User not found!');
          return;
        }

        this.user = user; 
        
        this.userForm.patchValue({
          name: user.name,
          surname: user.surname,
          username: user.username,
          password: user.password,
          role: user.role,
        });

        if (this.isDisplayWorker(user) || this.isDisplayDirector(user)) {
          this.userForm.patchValue({ salary: user.salary});
        }

        if (this.loggedInUserRole !== 'DIREKTOR') { 
          this.userForm.get('username')?.disable();
          this.userForm.get('role')?.disable();
          this.userForm.get('salary')?.disable();
        }
      },
      error: (err) => console.error('Error fetching user details:', err)
    });
  }
  
  isDisplayWorker(user: DisplayWorkerDto | DisplayClientDto | DisplayDirectorDto): user is DisplayWorkerDto {
    return user && user.role === 'RADNIK';
  }
  
  isDisplayDirector(user: DisplayWorkerDto | DisplayClientDto | DisplayDirectorDto): user is DisplayDirectorDto {
    return user && user.role === 'DIREKTOR';
  }

  handleSubmit(): void {
    if (this.userForm.valid) {
      let updatedUser: UpdateWorkerDto | UpdateClientDto | UpdateDirectorDto;
      const userRole = this.user.role;
  
      const userData: any = {
        name: this.userForm.value.name,
        surname: this.userForm.value.surname,
        username: this.userForm.value.username,
        role: this.userForm.value.role,
        salary: this.userForm.value.salary !== null && this.userForm.value.salary !== undefined
          ? Number(this.userForm.value.salary)
          : null 
      };
  
      // Only add password if it's been modified 
      if (this.userForm.value.password && this.userForm.value.password !== this.user.password) {
        userData.password = this.userForm.value.password;
      }

      if (userRole === 'RADNIK') {
        updatedUser = userData as UpdateWorkerDto;
      } else if (userRole === 'DIREKTOR') {
        updatedUser = userData as UpdateDirectorDto;
      } else {
        updatedUser = userData as UpdateClientDto;
      }
  
      this.userService.updateUser(this.userId, updatedUser).subscribe({
        next: () => {
          console.log("Updated User:", updatedUser);
          this.updateMessage = "User updated successfully!";
          setTimeout(() => {
            this.router.navigate([`/users/${this.userId}`]);
          }, 800);
        },
        error: (err) => {
          this.errorMessage = 'Error updating user';
          console.error('Error updating user:', err);
        }
      });
    } else {
      console.log('Form is invalid:', this.userForm.errors);
    }
  }



}
