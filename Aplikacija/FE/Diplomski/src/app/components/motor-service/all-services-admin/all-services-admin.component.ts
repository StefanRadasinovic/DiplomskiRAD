import { AfterViewInit, Component } from '@angular/core';
import { ServiceService } from '../../../services/serviceService';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { DeclineServiceDto, DirektorServiceInfo, Service, ServiceInfo } from '../../../models/serviceDTO';
import { AcceptDialogComponent } from '../../dialog/accept-dialog/accept-dialog.component';
import { DeclineDialogComponent } from '../../dialog/decline-dialog/decline-dialog.component';
import { UserService } from '../../../services/userService';
import { AuthorisationService } from '../../../services/authorisationService';
import { DisplayClientDto, DisplayDirectorDto, DisplayWorkerDto } from '../../../models/userDTO';

@Component({
  selector: 'app-all-services-admin',
  templateUrl: './all-services-admin.component.html',
  styleUrl: './all-services-admin.component.css'
})
export class AllServicesAdminComponent implements AfterViewInit {

  
  isLoading = true;
  user!: DisplayWorkerDto | DisplayClientDto | DisplayDirectorDto; 

  services: DirektorServiceInfo[] = [];
  currentService: DirektorServiceInfo = {
    id: '',
    failureDescription: '',
    picture: '',
    startDate: '',
    endDate: '',
    serviceStatus: '',
    razlogOdbijanja: '',
    userInfo: {
        id: '',
        name: '',
        surname: '',
        username: '',
        role: ''
    },
    taskServiceInfo: [] ,
    reviewInfos:[]
}


  
  displayForServices: ServiceInfo = {} as ServiceInfo;

  currentItem: any = null;
  loading = true; // flagovanje loading

  createMessage: string = "";

  currentIndex = -1;
  surname = '';
  name = '';
  startingDate = '';
  status = '';

  constructor(private serviceService: ServiceService, 
              private router: Router, 
              private dialog: MatDialog,
              private userService: UserService,
              private authService : AuthorisationService,
          ) { }

  ngAfterViewInit(): void {
    const userId = this.authService.getUserId();
    if (userId !== null) {
      
      this.getUserById(userId);  
      
    } else {
      console.error('Invalid User ID');
    }
    this.retrieveServices();
  }

  
  getUserById(id: string): void {
    this.userService.getUserById(id).subscribe({
      next: (res) => {
        this.user = res;
        this.loading = false;
        console.log('KORISNIK JE:', res);
      },
      error: (err) => {
        console.error('Error fetching user details:', err);
        this.loading = false;
      }
    });
  }


  retrieveServices(): void {
    this.serviceService.getAllPendingAndInprogressServices()
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
    this.currentService = item;
    this.currentIndex = index;

    //this.router.navigate([`/all-orders/${item.id}`]); // Navigate to the userOrder

    if (!item.id) {
      console.error('Error: Item does not have an ID', item);
      return;
  }

      this.serviceService.getServiceById(item.id).subscribe({
          next: (res) => {

              this.displayForServices = res;
             //za Empty slike
             if (this.displayForServices.picture === '' && this.displayForServices.picture === '')
              {
                 this.displayForServices.picture = 'https://placehold.co/700x300/EEE/31343C?text=300x300';
              }
              
              const statusClass  = this.getStatusClass(this.displayForServices.serviceStatus);
              this.loading = false;
              console.log("Displayed Service:", this.displayForServices);
          },
          error: (err) => {
              console.error('Error fetching order details:', err);
              this.loading = false;
          }
      });
  }


  filterByNameAndSurname(): void {
    this.serviceService.getAllPendingAndInprogressServices().subscribe({
        next: (data) => {
          this.services = data.filter(centre => 
            centre.userInfo?.name.includes(this.name.toLowerCase()) &&
            centre.userInfo?.surname.includes(this.surname.toLowerCase()) &&
            centre.startDate.includes(this.startingDate.toLowerCase()) &&
            centre.serviceStatus?.includes(this.status)
          );
        },
        error: (e) => console.error(e)
      });
  }



  handleAccept(): void {
    const dialogRef = this.dialog.open(AcceptDialogComponent);
  
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.serviceService.acceptService(this.currentItem.id).subscribe({
          next: () => {
            console.log('service: U_TOKU');
            this.router.navigate(['/add-task', this.currentItem.id]);
          },
          error: (err) => console.error('Error accepting:', err)
        });
      }
    });
  }
  

  handleDecline(): void {
    const dialogRef = this.dialog.open(DeclineDialogComponent);
  
    dialogRef.afterClosed().subscribe(result => {
      if (result && result.rejectionReason) {
        const declineData: DeclineServiceDto = {
          razlogOdbijanja: result.rejectionReason
        };
  
        this.serviceService.declineService(this.currentItem.id, declineData).subscribe({
          next: () => {
            console.log('Service: ODBIJEN');
            console.log('razlog odbijanja je:', declineData);
            this.router.navigate(['/display-services', this.user.id]);
            window.location.reload();
          },
          error: (err) => console.error('Error declining Service:', err)
        });
      }
    });
  }

  tasksDetails(serviceId: string): void {
    this.router.navigate(['/view-tasks', serviceId]);
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

}
