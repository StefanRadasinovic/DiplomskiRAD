import { AfterViewInit, Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserService } from '../../../services/userService';
import { Router } from '@angular/router';
import { CreateWorkerDtO } from '../../../models/userDTO';

@Component({
  selector: 'app-add-worker',
  templateUrl: './add-worker.component.html',
  styleUrl: './add-worker.component.css'
})
export class AddWorkerComponent{

equipmentForm: FormGroup;
createMessage: string = "";

constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private router: Router
  ) {
    this.equipmentForm = this.fb.group({
     
      name: ['', Validators.required],
      surname: [''] ,
      username: [''],
      password: [''],
      salary: [''],
    });
  }


  handleSubmit() {
    if (this.equipmentForm.valid) {
      const motorData: CreateWorkerDtO = this.equipmentForm.value;

      this.userService.createWorker(motorData).subscribe({
        next: () => {
          this.createMessage = "Uspesno dodat!";
          setTimeout(() => {
            this.router.navigate(['/all-users']);
          }, 800);
        },
        error: (error) => {
          console.error('Error adding user ', error);
          console.log(motorData);
        }
      });
    }
  }



 
}