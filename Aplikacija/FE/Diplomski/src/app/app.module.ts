import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MotorCardComponent } from './cards/motor-card/motor-card.component';
import { MotorContainerComponent } from './cards/motor-container/motor-container.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http'; 
import { MatIcon, MatIconModule } from '@angular/material/icon'
import { MatTableModule } from '@angular/material/table'
import {MatToolbarModule} from '@angular/material/toolbar'; 
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NO_ERRORS_SCHEMA } from '@angular/core';
import { NgChartsModule } from 'ng2-charts';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatDialogModule } from '@angular/material/dialog';
import { AddMotorsComponent } from './components/motors/add-motors/add-motors.component';
import { AllMotorsComponent } from './components/motors/all-motors/all-motors.component';
import { DialogComponent } from './components/dialog/delete-dialog/dialog.component';
import { GetMotorsByIdComponent } from './components/motors/get-motors-by-id/get-motors-by-id.component';
import { UpdateMotorComponent } from './components/motors/update-motor/update-motor.component';
import { EquipmentCardComponent } from './cards/equipment-card/equipment-card.component';
import { EquipmentContainerComponent } from './cards/equipment-container/equipment-container.component';
import { AllEquipmentComponent } from './components/equipment/all-equipment/all-equipment.component';
import { AddEquipmentComponent } from './components/equipment/add-equipment/add-equipment.component';
import { GetEquipmentByIdComponent } from './components/equipment/get-equipment-by-id/get-equipment-by-id.component';
import { UpdateEquipmentComponent } from './components/equipment/update-equipment/update-equipment.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { MatSortModule } from '@angular/material/sort';
import { MatButtonModule } from '@angular/material/button';
import { DirectorHomepageComponent } from './components/homepages/director-homepage/director-homepage.component';
import { RadnikHomepageComponent } from './components/homepages/radnik-homepage/radnik-homepage.component';
import { ClientHomepageComponent } from './components/homepages/client-homepage/client-homepage.component';
import { AllUsersComponent } from './components/users/all-users/all-users.component';
import { GetUserByIdComponent } from './components/users/get-user-by-id/get-user-by-id.component';
import { AddWorkerComponent } from './components/users/add-worker/add-worker.component';
import { UpdateUsersComponent } from './components/users/update-users/update-users.component';
import { AuthInterceptor } from './services/InterceptorService';
import { ShowUsersAdminComponent } from './components/users/show-users-admin/show-users-admin.component';
import { AdjustPricesComponent } from './components/adjust-prices/adjust-prices.component';
import { AcceptDialogComponent } from './components/dialog/accept-dialog/accept-dialog.component';
import { DeclineDialogComponent } from './components/dialog/decline-dialog/decline-dialog.component';
import { GetOrderForUserComponent } from './components/orders/get-order-for-user/get-order-for-user.component';
import { GetOrderForAdminComponent } from './components/orders/get-order-for-admin/get-order-for-admin.component';
import { AllServicesUserComponent } from './components/motor-service/all-services-user/all-services-user.component';
import { AllServicesAdminComponent } from './components/motor-service/all-services-admin/all-services-admin.component';
import { AddServiceComponent } from './components/motor-service/add-service/add-service.component';
import { CancelDialogComponent } from './components/dialog/cancel-dialog/cancel-dialog.component';
import { AddReviewComponent } from './components/reviews/add-review/add-review.component';
import { AddTasksComponent } from './components/tasks/add-tasks/add-tasks.component';
import { AllTasksAdminComponent } from './components/tasks/all-tasks-admin/all-tasks-admin.component';
import { AllTasksWorkerComponent } from './components/tasks/all-tasks-worker/all-tasks-worker.component';
import { ViewTasksForServiceComponent } from './components/motor-service/view-tasks-for-service/view-tasks-for-service.component';
import { FinishDialogComponent } from './components/dialog/finish-dialog/finish-dialog.component';
import { AssingNewWorkerComponent } from './components/tasks/assing-new-worker/assing-new-worker.component';



@NgModule({
  declarations: [
    AppComponent,
    MotorCardComponent,
    MotorContainerComponent,
    AddMotorsComponent,
    AllMotorsComponent,
    DialogComponent,
    GetMotorsByIdComponent,
    UpdateMotorComponent,
    EquipmentCardComponent,
    EquipmentContainerComponent,
    AllEquipmentComponent,
    AddEquipmentComponent,
    GetEquipmentByIdComponent,
    UpdateEquipmentComponent,
    NavbarComponent,
    LoginComponent,
    RegisterComponent,
    DirectorHomepageComponent,
    RadnikHomepageComponent,
    ClientHomepageComponent,
    AllUsersComponent,
    GetUserByIdComponent,
    AddWorkerComponent,
    UpdateUsersComponent,
    ShowUsersAdminComponent,
    AdjustPricesComponent,
    AcceptDialogComponent,
    DeclineDialogComponent,
    GetOrderForUserComponent,
    GetOrderForAdminComponent,
    AllServicesUserComponent,
    AllServicesAdminComponent,
    AddServiceComponent,
    CancelDialogComponent,
    AddReviewComponent,
    AddTasksComponent,
    AllTasksAdminComponent,
    AllTasksWorkerComponent,
    ViewTasksForServiceComponent,
    FinishDialogComponent,
    AssingNewWorkerComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    MatTableModule,
    HttpClientModule,
    MatToolbarModule,
    FormsModule,
    MatSortModule,
    MatToolbarModule,
    MatButtonModule,
    MatIcon,
    ReactiveFormsModule,
    MatDialogModule,
    BrowserAnimationsModule,
    NgChartsModule
  ],
  providers: [ 
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
