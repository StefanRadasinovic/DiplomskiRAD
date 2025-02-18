import { AfterViewInit, Component, OnInit } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { TaskService } from '../../../services/taskService';
import { ActivatedRoute, Router } from '@angular/router';
import { CreateTaskDto, TaskServiceInfo } from '../../../models/taskDTO';
import { UserService } from '../../../services/userService';
import { UserInfo } from '../../../models/userDTO';

@Component({
  selector: 'app-assing-new-worker',
  templateUrl: './assing-new-worker.component.html',
  styleUrl: './assing-new-worker.component.css'
})

export class AssingNewWorkerComponent implements OnInit {

  taskForm: FormGroup;
  createMessage: string = "";
  taskId!: string;
  users: UserInfo[] = [];
  serviceId!: string;
  taskInfo: TaskServiceInfo | null = null; 


  constructor(
    private fb: FormBuilder,
    private taskService: TaskService,
    private router: Router,
    private userService: UserService,
    private route: ActivatedRoute
  ) {
    // Initialize taskForm with just a userId field
    this.taskForm = this.fb.group({
      userId: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.taskId = this.route.snapshot.params['id'];
    this.retrieveWorkers();
  }

  retrieveWorkers(): void {
    this.userService.getAllFreeUsersForTask(this.taskId).subscribe({
      next: (data) => this.users = data,
      error: (e) => console.error(e)
    });
  }

  handleSubmit() {
    if (this.taskForm.valid) {
      const userId = this.taskForm.get('userId')?.value;
      this.taskService.assignOtherWorker(this.taskId, userId)
        .subscribe({
          next: (data) => {

            this.taskInfo = data;
            console.log("novi task je : ", this.taskInfo);
            this.createMessage = "Uspesno dodat!";
            setTimeout(() => {
              this.router.navigate(['/view-tasks', this.taskInfo?.serviceId]); 
            }, 800);
          },
          error: err => console.error('Error adding task:', err)
        });
    }
  }
}