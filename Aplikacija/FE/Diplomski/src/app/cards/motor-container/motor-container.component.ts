import { Component, OnInit } from '@angular/core';
import { PaginatedMotorProps } from '../../models/motorDTO';
import { MotorService } from '../../services/motorServices';
import { Router } from '@angular/router';

@Component({
  selector: 'app-motor-container',
  templateUrl: './motor-container.component.html',
  styleUrl: './motor-container.component.css'
})
export class MotorContainerComponent implements OnInit {

  motors: PaginatedMotorProps = {
  data: [],
  pageNumber: 1,
  pageSize: 5,
  totalPages: 1,
  totalRecord: 0
  };
  
  pageSizes = [5, 10, 15];
  pageNumber = 1;
  pageSize = 5;
  isLoading = true;
  isDisabledPrev = true;
  isDisabledNext = false;

  constructor(private motorService: MotorService, private router:Router) {}

  ngOnInit(): void {
    this.loadMotors();
  }

  loadMotors(): void {
    this.motorService.getAllWithPagination(this.pageNumber, this.pageSize).subscribe({
      next: (res) => {
        this.isLoading = false;
        this.motors = res;
        this.isDisabledPrev = this.pageNumber <= 1;
        this.isDisabledNext = this.pageNumber >= this.motors.totalPages;
        console.log(res);
      },
      error: (err) => console.error('Error fetching motors:', err)
    });
  }

  handlePagePrev(): void {
    if (this.pageNumber > 1) {
      this.pageNumber--;
      this.loadMotors();
    }
  }

  handlePageNext(): void {
    if (this.pageNumber < this.motors.totalPages) {
      this.pageNumber++;
      this.loadMotors();
    }
  }

  handlePageSizeChange(event: any): void {
    this.pageSize = +event.target.value;
    this.pageNumber = 1;
    this.loadMotors();
  }

  onMotorSelected(motorId: string) {
    this.router.navigate(['/motorcycles', motorId]); 
  }

}
