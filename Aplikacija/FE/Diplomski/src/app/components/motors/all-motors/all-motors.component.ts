import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-all-motors',
  templateUrl: './all-motors.component.html',
  styleUrl: './all-motors.component.css'
})
export class AllMotorsComponent {

  constructor(private router: Router) {}

  navigateToAddMotor() {
    this.router.navigate(['/add-motorcycles']);
  }
}
