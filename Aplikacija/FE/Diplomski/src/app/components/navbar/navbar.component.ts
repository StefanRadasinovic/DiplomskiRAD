import { Component, OnInit } from '@angular/core';
import { AuthorisationService } from '../../services/authorisationService';
import { Subscription } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent implements OnInit {
  userRole: string | null = 'ROLE_NOTAUTH';
  private authSubscription: Subscription = new Subscription();

  constructor(public authService: AuthorisationService, private router: Router) {}

  ngOnInit(): void {
    this.authSubscription = this.authService.userRole$.subscribe(role => {
      this.userRole = role;
      console.log("Navbar updated with role ", this.userRole);
    });
  }

  ngOnDestroy(): void {
    this.authSubscription.unsubscribe();
  }

  logout() {
    this.authService.logout();
    console.log("User logged out");
  }
}
