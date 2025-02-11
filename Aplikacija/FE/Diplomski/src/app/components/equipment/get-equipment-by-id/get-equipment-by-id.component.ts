import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { EquipmentService } from '../../../services/equipmentServices';
import { Equipment } from '../../../models/equipmentDTO';
import { DialogComponent } from '../../dialog/dialog.component';
import { FormBuilder, FormGroup } from '@angular/forms';
import { CreateOrderDto } from '../../../models/orderDTO';
import { OrderService } from '../../../services/orderService';
import { DisplayClientDto, DisplayDirectorDto, DisplayWorkerDto } from '../../../models/userDTO';
import { UserService } from '../../../services/userService';
import { AuthorisationService } from '../../../services/authorisationService';

@Component({
  selector: 'app-get-equipment-by-id',
  templateUrl: './get-equipment-by-id.component.html',
  styleUrl: './get-equipment-by-id.component.css'
})
export class GetEquipmentByIdComponent implements OnInit {

equipment!: Equipment;
loading = true; 
user!: DisplayWorkerDto | DisplayClientDto | DisplayDirectorDto; 

tp: number = 0;
totalSum: number = 0;

/* ZA CreteOrder*/
creatingOrderForm: FormGroup;
createMessage: string = "";



constructor(
    private route: ActivatedRoute,
    private equipmentService: EquipmentService,
    private orderService : OrderService,
    private userService : UserService,
    private authService : AuthorisationService,
    private dialog: MatDialog,
    private router: Router,
    private fb: FormBuilder,
  ) {
    this.creatingOrderForm = this.fb.group({

      orderAmount: [null]
    });

  }

ngOnInit(): void {
    const eqipmentId = this.route.snapshot.paramMap.get('id');
    if (eqipmentId !== null) {
      console.log('Equipment ID from route:', eqipmentId);
      this.getEquipmentById(eqipmentId.toString());
    } else {
      console.error('Invalid Equipment ID');
    }

    const userId = this.authService.getUserId();
    if (userId !== null) {
      
      this.getUserById(userId);  
      
    } else {
      console.error('Invalid User ID');
    }
  
    this.creatingOrderForm.get('orderAmount')?.valueChanges.subscribe(value => {
      let ns = value || 1; // Ensure at least 1
      this.totalSum = ns * this.tp;
    });
  
}

getEquipmentById(id: string): void {
    this.equipmentService.getEquipmentById(id).subscribe({
      next: (res) => {
        this.equipment = res;
        if(this.equipment.slika == '' || this.equipment.slika == null) this.equipment.slika ='https://placehold.co/700x300/EEE/31343C?text=300 x 300';
        this.loading = false; // Set loading to false after data is fetched
        if (this.equipment.displayPriceOnly && this.equipment.displayPriceOnly.length > 0) {
          this.tp = this.equipment.displayPriceOnly[0].price;
        }
        console.log(res);
      },
      error: (err) => {
        console.error('Error fetching motor details:', err);
        this.loading = false; // Set loading to false on error as well
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

racunaj(a: number, b: number): number {
  this.totalSum = a * b;
  return this.totalSum;
}

deleteEquipment(): void {
    const dialogRef = this.dialog.open(DialogComponent);
  
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        const motorId = this.route.snapshot.paramMap.get('id');  
        if (motorId !== null) {
          this.equipmentService.delete(motorId).subscribe({
            next: () => {
              console.log('Equipment deleted successfully');
              this.router.navigate(['/all-equipments']);
            },
            error: (err) => console.error('Error deleting Equipment:', err)
          });
        }
      }
    });
}

editEquipment(motorId: string): void {
    this.router.navigate(['/edit-equipment', motorId]);
}


//ZA CREATE
  handleSubmit() {
    if (this.creatingOrderForm.valid) {
      const itemData: CreateOrderDto = this.creatingOrderForm.value;
    
      this.orderService.createOrder(this.user.id, this.equipment.id, itemData).subscribe({
        
        next: () => {
          this.createMessage = "Uspesno kupljen!";
          setTimeout(() => {
            this.router.navigate(['/all-motorcycles']); ///POSLE IZMENI NA PUTANJU GETAllOders by userId-tj na taj page 
          }, 800);
        },
        error: (error) => {
          console.error('Error buying equipment ', error);
          console.log(itemData);
        }
      });
    }
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
