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

const routes: Routes = [
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
