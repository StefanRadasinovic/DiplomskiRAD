import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AllMotorsComponent } from './components/motors/all-motors/all-motors.component';
import { AddMotorsComponent } from './components/motors/add-motors/add-motors.component';
import { GetMotorsByIdComponent } from './components/motors/get-motors-by-id/get-motors-by-id.component';
import { UpdateMotorComponent } from './components/motors/update-motor/update-motor.component';
import { AllEquipmentComponent } from './components/equipment/all-equipment/all-equipment.component';
import { AddEquipmentComponent } from './components/equipment/add-equipment/add-equipment.component';
import { GetEquipmentByIdComponent } from './components/equipment/get-equipment-by-id/get-equipment-by-id.component';
import { UpdateEquipmentComponent } from './components/equipment/update-equipment/update-equipment.component';
import { RegisterComponent } from './components/register/register.component';
import { LoginComponent } from './components/login/login.component';
import { DirectorHomepageComponent } from './components/homepages/director-homepage/director-homepage.component';
import { ClientHomepageComponent } from './components/homepages/client-homepage/client-homepage.component';
import { RadnikHomepageComponent } from './components/homepages/radnik-homepage/radnik-homepage.component';

const routes: Routes = [
  {path: '', redirectTo: 'login', pathMatch: 'full' },
  {path: 'register', component: RegisterComponent },
  {path: 'login', component: LoginComponent },
  {path:'director-homepage', component:DirectorHomepageComponent},
  {path:'worker-homepage', component:RadnikHomepageComponent},
  {path:'client-homepage', component:ClientHomepageComponent},
  {path: 'all-motorcycles', component: AllMotorsComponent },
  {path: 'add-motorcycles', component: AddMotorsComponent },
  {path: 'motorcycles/:id', component: GetMotorsByIdComponent}, 
  {path: 'edit-motorcycles/:id', component: UpdateMotorComponent},
  {path: 'all-equipments', component: AllEquipmentComponent },
  {path: 'add-equipments', component: AddEquipmentComponent },
  {path: 'equipments/:id', component: GetEquipmentByIdComponent},
  {path: 'edit-equipments/:id', component: UpdateEquipmentComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
