
import { AfterViewInit, Component, OnInit } from '@angular/core';
import { OrderService } from '../../../services/orderService';
import { Router } from '@angular/router';
import { OrderInfo } from '../../../models/orderDTO';
import { DeclineDialogComponent } from '../../dialog/decline-dialog/decline-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { AcceptDialogComponent } from '../../dialog/accept-dialog/accept-dialog.component';
import { AuthorisationService } from '../../../services/authorisationService';
import { UserService } from '../../../services/userService';
import { DisplayClientDto, DisplayDirectorDto, DisplayWorkerDto } from '../../../models/userDTO';


@Component({
  selector: 'app-get-order-for-user',
  templateUrl: './get-order-for-user.component.html',
  styleUrl: './get-order-for-user.component.css'
})


export class GetOrderForUserComponent implements OnInit {

 user!: DisplayWorkerDto | DisplayClientDto | DisplayDirectorDto; 
  isLoading = true;
  orders: OrderInfo[] = [];


  currentOrder: OrderInfo = {
    id: '',
    orderAmount: 0,  
    totalPrice: '',
    orderStatus: '',
    equipmentInfo: [],  
    motorcycleInfo: [],  
    userId: '',
    userInfo: undefined,  
  };
  
  displayForOrder: OrderInfo = {} as OrderInfo;

  currentItem: any = null;
  loading = true; // flagovanje loading
  isMotor = false;

  createMessage: string = "";

  currentIndex = -1;
  surname = '';
  name = '';
  status = '';

  constructor(private orderService: OrderService, 
              private router: Router, 
              private dialog: MatDialog,
              private authService : AuthorisationService,
              private userService: UserService,
          ) { }

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
        this.loading = false;
        console.log('KORISNIK JE:', res);

        this.retrieveOrders();
      },
      error: (err) => {
        console.error('Error fetching user details:', err);
        this.loading = false;
      }
    });
  }
  

  retrieveOrders(): void {
    this.orderService.getAllOrdersForUser(this.user.id)
      .subscribe({
        next: (data) => {
          this.orders = data;
          this.isLoading = false;
          console.log(data);
        },
        error: (e) => console.error(e)
      });
  }

  setActiveOrders(item: any, index: number): void {

    console.log('Selected Item:', item); 
    this.currentItem = item;
    this.currentOrder = item;
    this.currentIndex = index;

    if (!item.id) {
      console.error('Error: Item does not have an ID', item);
      return;
  }

      this.orderService.getOrderById(item.id).subscribe({
          next: (res) => {

              this.displayForOrder = res;
              //za Empty slike
              if ((this.displayForOrder.motorcycleInfo?.[0]?.slika === '' && this.displayForOrder.equipmentInfo?.[0]?.slika === '') || 
                 (this.displayForOrder.motorcycleInfo?.[0]?.slika === null && this.displayForOrder.equipmentInfo?.[0]?.slika === null))
                {
                  if (this.displayForOrder.equipmentInfo?.[0]) this.displayForOrder.equipmentInfo[0].slika = 'https://placehold.co/700x300/EEE/31343C?text=300x300';
                  if (this.displayForOrder.motorcycleInfo?.[0]) this.displayForOrder.motorcycleInfo[0].slika = 'https://placehold.co/700x300/EEE/31343C?text=300x300';
                }

                //jel motor
                if (this.displayForOrder.motorcycleInfo && this.displayForOrder.motorcycleInfo.length > 0) { //is it motor
                  this.isMotor = true;
                } else {
                  this.isMotor = false; 
                }
                
                const statusClass  = this.getStatusClass(this.displayForOrder.orderStatus);
          
              this.loading = false;
              console.log("jel motor: ", this.isMotor);
              console.log("Displayed Order:", this.displayForOrder);
          },
          error: (err) => {
              console.error('Error fetching order details:', err);
              this.loading = false;
          }
      });
  }


  filterByNameAndSurname(): void { 
    this.orderService.getAllOrdersForUser(this.user.id)
      .subscribe({
        next: (data) => {
          const ordersArray = Array.isArray(data) ? data : [data];

          this.orders = ordersArray.filter(centre => {
            const motorcycleNameMatches = centre.motorcycleInfo?.[0]?.name?.toLowerCase().includes(this.name.toLowerCase());
            const equipmentNameMatches = centre.equipmentInfo?.[0]?.name?.toLowerCase().includes(this.name.toLowerCase());
            const producerSurnameMatches = (centre.motorcycleInfo?.[0]?.producers?.[0]?.name?.toLowerCase().includes(this.surname.toLowerCase())) ||
                                          (centre.equipmentInfo?.[0]?.producers?.[0]?.name?.toLowerCase().includes(this.surname.toLowerCase()));
            const statusMatches = centre.orderStatus.toLowerCase().includes(this.status.toLowerCase());

            return (motorcycleNameMatches || equipmentNameMatches) && producerSurnameMatches && statusMatches;
          });
        },
        error: (e) => console.error(e)
      });
}

  getStatusClass(status: string): string {
    switch (status) {
      case 'PRIHVACEN':
        return 'status-PRIHVACEN';
      case 'NA_CEKANJU':
          return 'status-NA_CEKANJU';
      case 'ODBIJEN':
          return 'status-ODBIJEN';
      default:
        return 'status-default'; 
    }
  }



}
