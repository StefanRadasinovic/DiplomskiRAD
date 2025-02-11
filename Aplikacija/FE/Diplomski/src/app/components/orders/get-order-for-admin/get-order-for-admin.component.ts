import { AfterViewInit, Component } from '@angular/core';
import { OrderService } from '../../../services/orderService';
import { Router } from '@angular/router';
import { OrderInfo } from '../../../models/orderDTO';
import { DeclineDialogComponent } from '../../dialog/decline-dialog/decline-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { AcceptDialogComponent } from '../../dialog/accept-dialog/accept-dialog.component';

@Component({
  selector: 'app-get-order-for-admin',
  templateUrl: './get-order-for-admin.component.html',
  styleUrl: './get-order-for-admin.component.css'
})
export class GetOrderForAdminComponent  implements AfterViewInit {

  displayedColumns: string[] = ['itemName', 'producer', 'orderAmount', 'totalPrice'];
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

  createMessage: string = "";

  currentIndex = -1;
  surname = '';
  name = '';

  constructor(private orderService: OrderService, private router: Router, private dialog: MatDialog,) { }

  ngAfterViewInit(): void {
    this.retrieveOrders();
  }

  retrieveOrders(): void {
    this.orderService.getAllPendingOrders()
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

    //this.router.navigate([`/all-orders/${item.id}`]); // Navigate to the userOrder

    if (!item.id) {
      console.error('Error: Item does not have an ID', item);
      return;
  }

      this.orderService.getOrderById(item.id).subscribe({
          next: (res) => {

              this.displayForOrder = res;
              this.loading = false;
              console.log("Displayed Order:", this.displayForOrder);
          },
          error: (err) => {
              console.error('Error fetching order details:', err);
              this.loading = false;
          }
      });
  }


  filterByNameAndSurname(): void { 
    this.orderService.getAllPendingOrders()
      .subscribe({
        next: (data) => {
          this.orders = data.filter(centre => {
            const motorcycleNameMatches = centre.motorcycleInfo?.[0]?.name?.toLowerCase().includes(this.name.toLowerCase());
            const equipmentNameMatches = centre.equipmentInfo?.[0]?.name?.toLowerCase().includes(this.name.toLowerCase());
            const producerSurnameMatches = (centre.motorcycleInfo?.[0]?.producers?.[0]?.name?.toLowerCase().includes(this.surname.toLowerCase())) ||
                                          (centre.equipmentInfo?.[0]?.producers?.[0]?.name?.toLowerCase().includes(this.surname.toLowerCase()));
  
            return (motorcycleNameMatches || equipmentNameMatches) && producerSurnameMatches;
          });
        },
        error: (e) => console.error(e)
      });
  }



  handleAccept(): void {
    const dialogRef = this.dialog.open(AcceptDialogComponent);
  
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.orderService.acceptOrder(this.currentItem.id).subscribe({
          next: () => {
            console.log('Order PRIHVACEN');
            this.router.navigate(['/all-pendingOrders']).then(() => {
              window.location.reload();  
            });
          },
          error: (err) => console.error('Error deleting Equipment:', err)
        });
      }
    });
  }
  

 
handleDecline(): void {
    const dialogRef = this.dialog.open(DeclineDialogComponent);
  
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.orderService.declineOrder(this.currentItem.id).subscribe({
            next: () => {
              console.log('Order ODBIJEN');
              this.router.navigate(['/all-pendingOrders']);
              window.location.reload();
            },
            error: (err) => console.error('Error deleting Equipment:', err)
          });
        
      }
    });
}
    
  
}
