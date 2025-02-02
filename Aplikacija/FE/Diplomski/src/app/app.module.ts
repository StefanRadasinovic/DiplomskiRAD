import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MotorCardComponent } from './cards/motor-card/motor-card.component';
import { MotorContainerComponent } from './cards/motor-container/motor-container.component';
import { HttpClientModule } from '@angular/common/http'; 
import { MatIconModule } from '@angular/material/icon'
import { MatTableModule } from '@angular/material/table'
import {MatToolbarModule} from '@angular/material/toolbar'; 
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NO_ERRORS_SCHEMA } from '@angular/core';
import { MatDialogModule } from '@angular/material/dialog';
import { AddMotorsComponent } from './components/add-motors/add-motors.component';
import { AllMotorsComponent } from './components/all-motors/all-motors.component';
import { DialogComponent } from './components/dialog/dialog.component';
import { GetMotorsByIdComponent } from './components/get-motors-by-id/get-motors-by-id.component';
import { UpdateMotorComponent } from './components/update-motor/update-motor.component';

@NgModule({
  declarations: [
    AppComponent,
    MotorCardComponent,
    MotorContainerComponent,
    AddMotorsComponent,
    AllMotorsComponent,
    DialogComponent,
    GetMotorsByIdComponent,
    UpdateMotorComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    MatTableModule,
    HttpClientModule,
    MatToolbarModule,
    MatIconModule,
    FormsModule,
    ReactiveFormsModule,
    MatDialogModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
