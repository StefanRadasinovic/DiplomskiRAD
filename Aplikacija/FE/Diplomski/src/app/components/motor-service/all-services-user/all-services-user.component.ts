import { Component } from '@angular/core';
import { ServiceService } from '../../../services/serviceService';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { AuthorisationService } from '../../../services/authorisationService';
import { UserService } from '../../../services/userService';
import { DisplayClientDto, DisplayDirectorDto, DisplayWorkerDto } from '../../../models/userDTO';
import { Service, UserServiceInfo } from '../../../models/serviceDTO';
import { DialogComponent } from '../../dialog/delete-dialog/dialog.component';
import { CancelDialogComponent } from '../../dialog/cancel-dialog/cancel-dialog.component';

@Component({
  selector: 'app-all-services-user',
  templateUrl: './all-services-user.component.html',
  styleUrl: './all-services-user.component.css'
})
export class AllServicesUserComponent {

user!: DisplayWorkerDto | DisplayClientDto | DisplayDirectorDto; 
  
  isLoading = true;
  services: UserServiceInfo[] = [];

  currentOrder: UserServiceInfo = {
    id: '',
    failureDescription: '',  
    picture: '',
    startDate : '',
    endDate : '',
    serviceStatus : '',
    razlogOdbijanja: ''
  };
  
  displayForServices: UserServiceInfo = {} as UserServiceInfo;

  currentItem: any = null;
  loading = true; // flagovanje loading
  isMotor = false;

  createMessage: string = "";
  errorMessage:string="";

  currentIndex = -1;

  startingDate = '';
  endingDate = '';
  status = '';

  constructor(private serviceService: ServiceService, 
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

        this.retrieveServices();
      },
      error: (err) => {
        console.error('Error fetching user details:', err);
        this.loading = false;
      }
    });
  }
  

  retrieveServices(): void {
    this.serviceService.getAllServicesForUser(this.user.id)
      .subscribe({
        next: (data) => {
          this.services = data;
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

      this.serviceService.getServiceById(item.id).subscribe({
          next: (res) => {

              this.displayForServices = res;
              this.loading = false;
             //za Empty slike
             if (this.displayForServices.picture === '' && this.displayForServices.picture === '')
            {
               this.displayForServices.picture = 'https://placehold.co/700x300/EEE/31343C?text=300x300';
            }
              console.log("Displayed Service:", this.displayForServices);
              const statusClass  = this.getStatusClass(this.displayForServices.serviceStatus);
          },
          error: (err) => {
              console.error('Error fetching service details:', err);
              this.loading = false;
          }
      });
  }

  filterByNameAndSurname(): void {
    this.serviceService.getAllServicesForUser(this.user.id).subscribe({
        next: (data) => {
          this.services = data.filter(centre => 
            centre.startDate?.includes(this.startingDate.toLowerCase()) &&
            centre.endDate?.includes(this.endingDate.toLowerCase()) &&
            centre.serviceStatus?.includes(this.status)
          );
        },
        error: (e) => console.error(e)
      });
  }


  getStatusClass(status: string): string {
    switch (status) {
      case 'ZAVRSEN':
        return 'status-ZAVRSEN';
      case 'NA_CEKANJU':
          return 'status-NA_CEKANJU';
      case 'U_TOKU':
          return 'status-U_TOKU';
      case 'ODBIJEN':
          return 'status-ODBIJEN';
      default:
        return 'status-default'; 
    }
  }

  deleteService(): void {
  const dialogRef = this.dialog.open(CancelDialogComponent);

  dialogRef.afterClosed().subscribe(result => {
    if (result) {
      this.serviceService.deleteService(this.currentItem.id).subscribe({
        next: () => {
          console.log('Service deleted successfully');
          this.errorMessage="";
          this.router.navigate(['/all-service', this.user.id]).then(() => {
            window.location.reload();
          });
        },
        error: (err) => {
          this.errorMessage="Service can't be canceled <24h before starting";
          console.error('Service can not be canceled 24h before the start:', err);
        }
      });
    }
  });
}


    reviewService(serviceId: string): void {
      this.router.navigate(['/add-review', serviceId]);
    }

    navigateToAddService() {
      this.router.navigate(['/add-service']);
    }

    downloadPdf(serviceId: string): void {  //OVDE CES ICI NA METODU GetAllTasksForService-svi detalji servisa
      console.log("kliknuo si download");
    }

    


}
