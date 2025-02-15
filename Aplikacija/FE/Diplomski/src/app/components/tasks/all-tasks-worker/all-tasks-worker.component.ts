import { Component } from '@angular/core';
import { RejectTaskDto, TaskServiceInfo } from '../../../models/taskDTO';
import { MatDialog } from '@angular/material/dialog';
import { AuthorisationService } from '../../../services/authorisationService';
import { UserService } from '../../../services/userService';
import { Router } from '@angular/router';
import { DisplayClientDto, DisplayDirectorDto, DisplayWorkerDto } from '../../../models/userDTO';
import { TaskService } from '../../../services/taskService';
import { DeclineServiceDto } from '../../../models/serviceDTO';
import { DeclineDialogComponent } from '../../dialog/decline-dialog/decline-dialog.component';
import { AcceptDialogComponent } from '../../dialog/accept-dialog/accept-dialog.component';
import { UsedSparePartDto } from '../../../models/sparePartsDTO';
import { FinishDialogComponent } from '../../dialog/finish-dialog/finish-dialog.component';

@Component({
  selector: 'app-all-tasks-worker',
  templateUrl: './all-tasks-worker.component.html',
  styleUrl: './all-tasks-worker.component.css'
})
export class AllTasksWorkerComponent {
user!: DisplayWorkerDto | DisplayClientDto | DisplayDirectorDto; 
  
  isLoading = true;
  tasks: TaskServiceInfo[] = [];

  currentTask: TaskServiceInfo = { 
    id: '',
    taskDescription: '',
    endDateTask: '',
    status: '',
    razlogOdbijanja: '',
    serviceId: '',
    workerInfo: {
        id: '',
        name: '',
        surname: '',
        username: '',
        role: ''
    },
    sparePartInfo: [
      {
        id: '',
        name: '',
        amount: null,
        isPartUsed: ''
      }
    ] 
}
  
  displayForTasks: TaskServiceInfo = {} as TaskServiceInfo;

  currentItem: any = null;
  loading = true; // flagovanje loading
  isMotor = false;

  createMessage: string = "";
  errorMessage:string="";

  currentIndex = -1;

  taskDescription = '';
  status = '';

  constructor(private taskService: TaskService, 
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
    this.taskService.getTasksForUser(this.user.id)
      .subscribe({
        next: (data) => {
          this.tasks = data;
          this.isLoading = false;
          console.log(data);
        },
        error: (e) => console.error(e)
      });
  }

  setActiveOrders(item: any, index: number): void {

    console.log('Selected Item:', item); 
    this.currentItem = item;
    this.currentTask = item;
    this.currentIndex = index;

    if (!item.id) {
      console.error('Error: Item does not have an ID', item);
      return;
  }

      this.taskService.getTaskById(item.id).subscribe({
          next: (res) => {

              this.displayForTasks = res;
              this.loading = false;
              console.log("Displayed task:", this.displayForTasks);
              const statusClass  = this.getStatusClass(this.displayForTasks.status);
          },
          error: (err) => {
              console.error('Error fetching service details:', err);
              this.loading = false;
          }
      });
  }

  filterByNameAndSurname(): void {
    this.taskService.getTasksForUser(this.user.id).subscribe({
        next: (data) => {
          this.tasks = data.filter(centre => 
            centre.taskDescription?.includes(this.taskDescription.toLowerCase()) &&
            centre.status?.includes(this.status)
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

  handleDecline(): void {
      const dialogRef = this.dialog.open(DeclineDialogComponent);
    
      dialogRef.afterClosed().subscribe(result => {
        if (result && result.rejectionReason) {
          const declineData: RejectTaskDto = {
            razlogOdbijanja: result.rejectionReason
          };
          this.taskService.declineTask(this.currentItem.id, declineData).subscribe({
            next: () => {
              console.log('Service: ODBIJEN');
              console.log('razlog odbijanja je:', declineData);
              this.router.navigate(['/all-tasks', this.user.id]).then(() => {
                window.location.reload();
              });
            },
            error: (err) => console.error('Error declining Service:', err)
          });
        }
      });
  }

  handleAccept(): void {
      const dialogRef = this.dialog.open(AcceptDialogComponent);
    
      dialogRef.afterClosed().subscribe(result => {
        if (result) {
          this.taskService.acceptTask(this.currentItem.id, this.user.id).subscribe({
            next: () => {
              console.log('service: U_TOKU');
              this.router.navigate(['/all-tasks', this.user.id]).then(() => {
                window.location.reload();
              });
            },
            error: (err) => console.error('Error accepting:', err)
          });
        }
      });
    }

    handleFinish(): void {
      const dialogRef = this.dialog.open(FinishDialogComponent); //NAPRAVI POSEBAN DIALOG
    
      dialogRef.afterClosed().subscribe(result => {
        if (result) {
          const finishData: UsedSparePartDto = {
            name: result.name,
            amount:result.amount
          };
    
          console.log('Used parts are:', finishData);
          console.log('Task iD JE :', this.currentItem.id);

          this.taskService.finishTask(this.currentItem.id, finishData).subscribe({
            next: () => {
              console.log('Service: ZAVRSEN');
             
              this.router.navigate(['/all-tasks', this.user.id]).then(() => {
                window.location.reload();
              });
            },
            error: (err) => console.error('Error declining Service:', err)
          });
        }
      });
  }
  

}
