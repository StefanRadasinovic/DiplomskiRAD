import { AfterViewInit, Component, OnInit } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { TaskService } from '../../../services/taskService';
import { ActivatedRoute, Router } from '@angular/router';
import { CreateTaskDto } from '../../../models/taskDTO';
import { UserService } from '../../../services/userService';
import { UserInfo } from '../../../models/userDTO';

@Component({
  selector: 'app-add-tasks',
  templateUrl: './add-tasks.component.html',
  styleUrl: './add-tasks.component.css'
})
export class AddTasksComponent implements OnInit {
  taskForm: FormGroup;
  createMessage: string = "";
  serviceId!: string;
  users: UserInfo[] = [];

  constructor(
    private fb: FormBuilder,
    private taskService: TaskService,
    private router: Router,
    private userService: UserService,
    private route: ActivatedRoute
  ) {
    this.taskForm = this.fb.group({
      tasks: this.fb.array([this.createTaskGroup()], Validators.required)
    });
  }

  ngOnInit(): void {
    this.serviceId = this.route.snapshot.params['id'];
    this.retrieveWorkers();
  }

  get tasks(): FormArray { return this.taskForm.get('tasks') as FormArray; }

  createTaskGroup(): FormGroup {
    return this.fb.group({
      userId: ['', Validators.required],
      taskDescription: ['', Validators.required]
    });
  }

  addTask() {
    this.tasks.push(this.createTaskGroup());
  }

  removeTask(index: number) {
    this.tasks.removeAt(index);
    if (this.tasks.length === 0) {
      this.addTask(); 
    }
  }

  retrieveWorkers(): void {
    this.userService.getAllWorkers().subscribe({
      next: (data) => this.users = data,
      error: (e) => console.error(e)
    });
  }

  handleSubmit() {
    if (this.taskForm.valid) {
      this.tasks.controls.forEach((taskGroup: AbstractControl) => {
        const userId = taskGroup.get('userId')?.value;
        const taskDto: CreateTaskDto = {
          taskDescription: taskGroup.get('taskDescription')?.value
        };
  
        this.taskService.createTask(this.serviceId, userId, taskDto)
          .subscribe({
            next: () => {
              this.createMessage = "Uspesno dodat!",
              this.router.navigate(['/view-tasks', this.serviceId]).then(() => {
                window.location.reload();
              });
            },
            error: err => console.error('Error adding task:', err)
          });
      });
    }
  }
  
}