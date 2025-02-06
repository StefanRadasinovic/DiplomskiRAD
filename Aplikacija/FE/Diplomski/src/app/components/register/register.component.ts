import { Component } from '@angular/core';
import { UserRegisterDto } from '../../models/userDTO';
import { AuthorisationService } from '../../services/authorisationService';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {

  constructor(private authService: AuthorisationService, private router: Router) { }

  userRegisterDto: UserRegisterDto = {
    name:'',
    surname:'',
    username: '',
    password: ''
  };

  registerUser(formValue: any) {
    this.userRegisterDto = {
      name: formValue.name,
      surname: formValue.surname,
      username: formValue.username,
      password: formValue.password
    };

    this.authService.register(this.userRegisterDto).subscribe(
      (response) => {
        console.log('Registration successful', response);
        alert("You have registered successfully");
        this.router.navigate(['/login']); 
      },
      (error) => {
        console.error('Registration failed', error);
      }
    );
  }

  redirect(){
    this.router.navigate(["/login"]);
  }

}
