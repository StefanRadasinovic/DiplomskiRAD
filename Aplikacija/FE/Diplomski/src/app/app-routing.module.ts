import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AllMotorsComponent } from './components/all-motors/all-motors.component';
import { AddMotorsComponent } from './components/add-motors/add-motors.component';
import { GetMotorsByIdComponent } from './components/get-motors-by-id/get-motors-by-id.component';
import { UpdateMotorComponent } from './components/update-motor/update-motor.component';

const routes: Routes = [
  {path: 'all-motorcycles', component: AllMotorsComponent },
  {path: 'add-motorcycles', component: AddMotorsComponent },
  {path: 'motorcycles/:id', component: GetMotorsByIdComponent}, 
  {path: 'edit-motorcycles/:id', component: UpdateMotorComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
