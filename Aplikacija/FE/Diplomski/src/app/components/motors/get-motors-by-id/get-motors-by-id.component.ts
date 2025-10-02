import { Component, OnInit } from '@angular/core';
import { Motor, MotorInfo } from '../../../models/motorDTO';
import { ActivatedRoute, Router } from '@angular/router';
import { MotorService } from '../../../services/motorServices';
import { MatDialog } from '@angular/material/dialog';
import { DialogComponent } from '../../dialog/delete-dialog/dialog.component';
import { DisplayClientDto, DisplayDirectorDto, DisplayWorkerDto, User } from '../../../models/userDTO';
import { UserService } from '../../../services/userService';
import { AuthorisationService } from '../../../services/authorisationService';
import { FormBuilder, FormGroup } from '@angular/forms';
import { CreateOrderDto } from '../../../models/orderDTO';
import { OrderService } from '../../../services/orderService';

@Component({
  selector: 'app-get-motors-by-id',
  templateUrl: './get-motors-by-id.component.html',
  styleUrl: './get-motors-by-id.component.css'
})
export class GetMotorsByIdComponent implements OnInit {
  motor!: Motor;
  loading = true; // Add a loading flag
  user!: DisplayWorkerDto | DisplayClientDto | DisplayDirectorDto; 


tp: number = 0;
totalSum: number = 0;

/* ZA CreteOrder*/
creatingOrderForm: FormGroup;
createMessage: string = "";


  constructor(
    private route: ActivatedRoute,
    private motorService: MotorService,
    private userService: UserService,
    private orderService : OrderService,
    private dialog: MatDialog,
    private router: Router,
    private authService : AuthorisationService,
    private fb: FormBuilder,
  ) {
    this.creatingOrderForm = this.fb.group({

      orderAmount: [null]
    });

  }

  ngOnInit(): void {
    const motorId = this.route.snapshot.paramMap.get('id');
    if (motorId !== null) {
      console.log('Motor ID from route:', motorId);
      this.getMotorById(motorId.toString());
    } else {
      console.error('Invalid motor ID');
    }

    const userId = this.authService.getUserId();
    if (userId !== null) {
      
      this.getUserById(userId);  
      
    } else {
      console.error('Invalid Usser ID');
    }

    this.creatingOrderForm.get('orderAmount')?.valueChanges.subscribe(value => {
      let ns = value || 1; // Ensure at least 1
      this.totalSum = ns * this.tp;
    });

  }

  getMotorById(id: string): void {
    this.motorService.getMotorById(id).subscribe({
      next: (res) => {
        this.motor = res;
        if(this.motor.slika == '' || this.motor.slika == null) this.motor.slika ='https://placehold.co/700x300/EEE/31343C?text=300 x 300';
        this.loading = false; // Set loading to false after data is fetched
        if (this.motor.displayPriceOnly && this.motor.displayPriceOnly.length > 0) {
          this.tp = this.motor.displayPriceOnly[0].price;
        }
        console.log(res);
      },
      error: (err) => {
        console.error('Error fetching motor details:', err);
        this.loading = false; // Set loading to false on error as well
      }
    });
  }

  racunaj(a: number, b: number): number {
    this.totalSum = a * b;
    return this.totalSum;
  }

  deleteMotor(): void {
    const dialogRef = this.dialog.open(DialogComponent);
  
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        const motorId = this.route.snapshot.paramMap.get('id');  
        if (motorId !== null) {
          this.motorService.delete(motorId).subscribe({
            next: () => {
              console.log('Motorcycle deleted successfully');
              this.router.navigate(['/all-motorcycles']);
            },
            error: (err) => console.error('Error deleting motorcycle:', err)
          });
        }
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


editMotor(motorId: string): void {
  this.router.navigate(['/edit-motorcycles', motorId]);
}

handleSubmit() {
    if (this.creatingOrderForm.valid) {
      const itemData: CreateOrderDto = this.creatingOrderForm.value;
    
      this.orderService.createOrder(this.user.id, this.motor.id, itemData).subscribe({
        
        next: () => {
          this.createMessage = "Uspesno kupljen!";
          setTimeout(() => {
            this.router.navigate(['/all-motorcycles']); ///POSLE IZMENI NA PUTANJU GETAllOders by userId-tj na taj page 
          }, 800);
        },
        error: (error) => {
          console.error('Error buying motor ', error);
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
