import { AfterViewInit, Component } from '@angular/core';
import { TaskService } from '../../../services/taskService';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { UserService } from '../../../services/userService';
import { AuthorisationService } from '../../../services/authorisationService';
import { DisplayClientDto, DisplayDirectorDto, DisplayWorkerDto } from '../../../models/userDTO';
import { TaskServiceInfo } from '../../../models/taskDTO';

@Component({
  selector: 'app-all-tasks-admin',
  templateUrl: './all-tasks-admin.component.html',
  styleUrl: './all-tasks-admin.component.css',
})
export class AllTasksAdminComponent implements AfterViewInit {

  
  isLoading = true;
  user!: DisplayWorkerDto | DisplayClientDto | DisplayDirectorDto; 

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

  createMessage: string = "";

  currentIndex = -1;
  surname = '';
  name = '';
  startingDate = '';
  status = '';

  constructor(private taskService: TaskService, 
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
    this.retrieveTasks();
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


  retrieveTasks(): void {
    this.taskService.getAllInProgressDeclinedTasks()
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

    //this.router.navigate([`/all-orders/${item.id}`]); // Navigate to the userOrder

    if (!item.id) {
      console.error('Error: Item does not have an ID', item);
      return;
  }

      this.taskService.getTaskById(item.id).subscribe({
          next: (res) => {

              this.displayForTasks = res;
             
              const statusClass  = this.getStatusClass(this.displayForTasks.status);
              this.loading = false;
              console.log("Displayed Task:", this.displayForTasks);
          },
          error: (err) => {
              console.error('Error fetching order details:', err);
              this.loading = false;
          }
      });
  }


  filterByNameAndSurname(): void {
    this.taskService.getAllInProgressDeclinedTasks().subscribe({
        next: (data) => {
          this.tasks = data.filter(centre => 
            centre.workerInfo?.name.includes(this.name.toLowerCase()) &&
            centre.workerInfo?.surname.includes(this.surname.toLowerCase()) &&
            centre.status?.includes(this.status)
          );
        },
        error: (e) => console.error(e)
      });
  }


  assignNewWorker(taskId : string) : void {
    this.router.navigate(['/assign-worker',taskId]) 
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
