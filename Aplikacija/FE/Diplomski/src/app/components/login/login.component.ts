import { Component } from '@angular/core';
import { AuthorisationService } from '../../services/authorisationService';
import { Router } from '@angular/router';
import { UserLoginDto } from '../../models/userDTO';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

  constructor(private authService: AuthorisationService, private router: Router) { }
  
  ngOnInit(): void {
    
  }
 
  ime:any;
  sifra:any;
  
  loginUser(user: any): void {
    let loginUser: UserLoginDto = {
      username: this.ime,
      password: this.sifra
    };
   
    this.authService.login(loginUser).subscribe(
      response => {
        this.authService.setToken(response.token);
        console.log(loginUser);
        console.log("Successfully logged in");

        const userRole = this.authService.getUserRole();
        //console.log("rola je:",userRole);
        const logedUserInfo = this.authService.getLogedUserInfo();
        //console.log("korisnik je", logedUserInfo.name, logedUserInfo.surname, logedUserInfo.username, logedUserInfo.role)
        this.authService.userRoleSubject.next(userRole); 
  
        // Navigate based on user role
        if (userRole === 'DIREKTOR') {
          console.log("Welcome back! Logged in as DIREKTOR.");
          this.router.navigate(['/all-motorcycles']);
        } else if (userRole === 'RADNIK') {
          console.log("Welcome back! Logged in as RADNIK.");
          this.router.navigate(['/all-motorcycles']);
        } else if (userRole === 'KLIJENT') {
          console.log("Welcome back! Logged in as KLIJENT.");
          this.router.navigate(['/all-motorcycles']);
        } else {
          this.router.navigate(['/login']);
        }
      },
      (error: HttpErrorResponse) => {
        alert("Bad Credentials");
      }
    );
  }
   
    
    redirect(){
      this.router.navigate(["/register"]);
    }
}
