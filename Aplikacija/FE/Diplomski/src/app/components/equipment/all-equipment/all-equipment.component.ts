import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-all-equipment',
  templateUrl: './all-equipment.component.html',
  styleUrl: './all-equipment.component.css'
})
export class AllEquipmentComponent {

   constructor(private router: Router) {}
  
    navigateToAddEquipment() {
      this.router.navigate(['/add-equipments']);
    }
}
