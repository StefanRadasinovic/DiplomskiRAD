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
import { AllUsersComponent } from './components/users/all-users/all-users.component';
import { GetUserByIdComponent } from './components/users/get-user-by-id/get-user-by-id.component';
import { AddWorkerComponent } from './components/users/add-worker/add-worker.component';
import { UpdateUsersComponent } from './components/users/update-users/update-users.component';
import { ShowUsersAdminComponent } from './components/users/show-users-admin/show-users-admin.component';
import { AdjustPricesComponent } from './components/adjust-prices/adjust-prices.component';
import { GetOrderForAdminComponent } from './components/orders/get-order-for-admin/get-order-for-admin.component';
import { GetOrderForUserComponent } from './components/orders/get-order-for-user/get-order-for-user.component';
import { AllServicesAdminComponent } from './components/motor-service/all-services-admin/all-services-admin.component';
import { AllServicesUserComponent } from './components/motor-service/all-services-user/all-services-user.component';
import { AddServiceComponent } from './components/motor-service/add-service/add-service.component';
import { AllTasksAdminComponent } from './components/tasks/all-tasks-admin/all-tasks-admin.component';
import { AddTasksComponent } from './components/tasks/add-tasks/add-tasks.component';
import { AllTasksWorkerComponent } from './components/tasks/all-tasks-worker/all-tasks-worker.component';
import { ViewTasksForServiceComponent } from './components/motor-service/view-tasks-for-service/view-tasks-for-service.component';
import { AssingNewWorkerComponent } from './components/tasks/assing-new-worker/assing-new-worker.component';


const routes: Routes = [
  {path: '', redirectTo: 'login', pathMatch: 'full' },
  {path: 'register', component: RegisterComponent },
  {path: 'login', component: LoginComponent },
  {path: 'all-motorcycles', component: AllMotorsComponent },
  {path: 'add-motorcycles', component: AddMotorsComponent },
  {path: 'motorcycles/:id', component: GetMotorsByIdComponent}, 
  {path: 'edit-motorcycles/:id', component: UpdateMotorComponent},
  {path: 'all-equipments', component: AllEquipmentComponent },
  {path: 'add-equipments', component: AddEquipmentComponent },
  {path: 'equipment/:id', component: GetEquipmentByIdComponent},
  {path: 'edit-equipment/:id', component: UpdateEquipmentComponent},
  {path: 'all-users', component: AllUsersComponent },
  {path: 'users/:id', component: GetUserByIdComponent },
  {path: 'display/:id', component: ShowUsersAdminComponent },
  {path: 'add-workers', component: AddWorkerComponent },
  {path: 'edit-users/:id', component: UpdateUsersComponent },
  {path: 'adjust-prices', component: AdjustPricesComponent },
  {path: 'all-pendingOrders', component: GetOrderForAdminComponent },
  {path: 'all-orders/:id', component: GetOrderForUserComponent },
  {path: 'display-services/:id', component: AllServicesAdminComponent },
  {path: 'all-service/:id', component: AllServicesUserComponent },
  {path: 'add-service', component: AddServiceComponent },
  {path: 'add-task/:id', component: AddTasksComponent },
  {path: 'display-tasks/:id', component: AllTasksAdminComponent },
  {path: 'all-tasks/:id', component: AllTasksWorkerComponent },
  {path: 'view-tasks/:id', component: ViewTasksForServiceComponent },
  {path: 'assign-worker/:id', component: AssingNewWorkerComponent },


  /*
  {path:'director-homepage', component:DirectorHomepageComponent},
  {path:'worker-homepage', component:RadnikHomepageComponent},
  {path:'client-homepage', component:ClientHomepageComponent},
  */ 

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
