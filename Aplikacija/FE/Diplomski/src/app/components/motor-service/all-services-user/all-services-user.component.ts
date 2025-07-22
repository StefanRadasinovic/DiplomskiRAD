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
import { jsPDF } from 'jspdf';
import autoTable from 'jspdf-autotable';
import { ReviewDialogComponent } from '../../dialog/review-dialog/review-dialog.component';
import { CreateReviewDto } from '../../../models/reviewDTO';
import { ReviewService } from '../../../services/reviewService';

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
    razlogOdbijanja: '',
    reviewInfos:[]
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
              private reviewService: ReviewService,
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

addReview(): void {
  const dialogRef = this.dialog.open(ReviewDialogComponent);

  dialogRef.afterClosed().subscribe(result => {
    if (result && result.comment && result.grade) {
      const reviewData: CreateReviewDto = {
        comment: result.comment,
        grade: result.grade
      };

      this.reviewService.createReview(this.currentItem.id, this.user.id, reviewData).subscribe({
        next: () => {
        
          console.log('Komentar je:', reviewData);
          this.router.navigate(['/display-services', this.user.id]);
          window.location.reload();
        },
        error: (err) => console.error('Error reviewing Service:', err)
      });
    }
  });
}
 

    navigateToAddService() {
      this.router.navigate(['/add-service']);
    }


    downloadPdf(serviceId: string): void {
      this.serviceService.getServiceById(serviceId).subscribe(service => {
        const doc = new jsPDF();
    
        doc.setFontSize(20).text('Izvestaj Servisiranja', 105, 15, { align: 'center' });
        doc.setFontSize(14).text(`STATUS: ${service.serviceStatus}`, 105, 40, { align: 'center' });
    
        doc.setFontSize(10).text('Motorcycle-Service Ltd.', 150, 10);
        doc.text('Bulevar Oslobodjenja 11, Novi Sad', 150, 15);
        doc.text('Tel: 021-456-7890', 150, 20);
    
        doc.setFontSize(12).setFont('helvetica', 'bold').text('Detalji Klijenta:', 10, 50);
        doc.setFont('helvetica', 'normal')
           .text('Ime:', 10, 60).text(`${service.userInfo.name}`, 42, 60).line(41, 61, 75, 61)
           .text('Prezime:', 10, 70).text(`${service.userInfo.surname}`, 42, 70).line(41, 71, 75, 71)
           .text('Korisnicko Ime:', 10, 80).text(`${service.userInfo.username}`, 42, 80).line(41, 81, 75, 81);
    
        doc.setFont('helvetica', 'bold').text('Detalji Servisiranja:', 10, 95);
        doc.setFont('helvetica', 'normal') 
           .text('Razlog Odbijanja:', 10, 105).text(`${service.razlogOdbijanja ?? '-'}`, 48, 105).line(47, 106, 120, 106)
           .text('Opis Kvara:', 10, 115).text(`${service.failureDescription}`, 48, 115).line(47, 116, 120, 116)
           .text('Datum pocetka:', 10, 125).text(`${service.startDate}`, 48, 125).line(47, 126, 75, 126)
           .text('Datum zavrsetka:', 10, 135).text(`${service.endDate}`, 48, 135).line(47, 136, 75, 136);
    
        
        if (service.taskServiceInfo && service.taskServiceInfo.length > 0) {
          autoTable(doc, {
            startY: 145,
            headStyles: { fontSize: 12 },
            bodyStyles: { fontSize: 11 },
            columnStyles: {
              3: { halign: 'center' }
            },
            head: [['Opis Zadatka', 'Datum Zavrsetka', 'Status', 'Rezervni Delovi', 'Kolicina']],
            body: service.taskServiceInfo.map(task => [
              task.taskDescription,
              task.endDateTask || '-',
              task.status,
              task.sparePartInfo.map(sp => sp.name ?? '-').join(', ') || '-',
              task.sparePartInfo.map(sp => (sp.amount && sp.amount !== 0 ? sp.amount : '-')).join(', ')
            ]),
          });
        }
    
        const finalY = (doc as any).lastAutoTable ? (doc as any).lastAutoTable.finalY + 25 : 160;
    
        doc.setFontSize(12).setFont('helvetica', 'bold').text('Potpis:', 10, finalY);
        const signatureImg = new Image();
        signatureImg.src = 'signature.jpg'; 
        signatureImg.onload = () => {
          doc.addImage(signatureImg, 'JPEG', 35, finalY - 15, 50, 20);
    
          const sealImg = new Image();
          sealImg.src = 'seal.jpg';
    
          sealImg.onload = () => {
                                          //x,y,width,height
            doc.addImage(sealImg, 'JPEG', 115, finalY - 22, 60, 57); 
            doc.save(`Service_${serviceId}.pdf`);
          };
        };
      });
    }
    

}
