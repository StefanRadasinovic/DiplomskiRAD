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
  isSubmitting: boolean = false;

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
    this.taskForm.markAsPristine(); 
    this.taskForm.markAsUntouched();
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

  handleSubmit(event: Event) {
    event.preventDefault(); // Prevent form submission from triggering automatically
    event.stopPropagation(); // Stop event bubbling
  
    this.isSubmitting = true; // Mark that submission was triggered by user
  
    if (this.taskForm.valid) {
      // Filter out incomplete tasks
      const validTasks = this.tasks.controls.filter(taskGroup => 
        taskGroup.get('userId')?.value && taskGroup.get('taskDescription')?.value
      );
  
      if (validTasks.length === 0) {
        this.createMessage = "No valid tasks to submit.";
        this.isSubmitting = false;
        return;
      }
  
      validTasks.forEach((taskGroup: AbstractControl) => {
        const userId = taskGroup.get('userId')?.value;
        const taskDto: CreateTaskDto = {
          taskDescription: taskGroup.get('taskDescription')?.value
        };
  
        this.taskService.createTask(this.serviceId, userId, taskDto)
          .subscribe({
            next: () => {
              this.createMessage = "Successfully added!";
              this.router.navigate(['/view-tasks', this.serviceId]).then(() => {
                window.location.reload();
              });
            },
            error: err => {
              console.error('Error adding task:', err);
              this.isSubmitting = false;
            }
          });
      });
    } else {
      this.isSubmitting = false;
    }
  }
  
  
}