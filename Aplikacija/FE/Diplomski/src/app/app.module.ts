import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MotorCardComponent } from './cards/motor-card/motor-card.component';
import { MotorContainerComponent } from './cards/motor-container/motor-container.component';
import { HttpClientModule } from '@angular/common/http'; 
import { MatIcon, MatIconModule } from '@angular/material/icon'
import { MatTableModule } from '@angular/material/table'
import {MatToolbarModule} from '@angular/material/toolbar'; 
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NO_ERRORS_SCHEMA } from '@angular/core';
import { MatDialogModule } from '@angular/material/dialog';
import { AddMotorsComponent } from './components/motors/add-motors/add-motors.component';
import { AllMotorsComponent } from './components/motors/all-motors/all-motors.component';
import { DialogComponent } from './components/dialog/dialog.component';
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
    MatDialogModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
