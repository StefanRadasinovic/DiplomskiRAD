import { Component, OnInit } from '@angular/core';
import { PaginatedEquipmentProps } from '../../models/equipmentDTO';
import { EquipmentService } from '../../services/equipmentServices';
import { Router } from '@angular/router';

@Component({
  selector: 'app-equipment-container',
  templateUrl: './equipment-container.component.html',
  styleUrl: './equipment-container.component.css'
})
export class EquipmentContainerComponent implements OnInit {

equipments: PaginatedEquipmentProps = {
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

  constructor(private equipmentService: EquipmentService, private router:Router) {}

  ngOnInit(): void {
    this.loadEquipment();
  }

  loadEquipment(): void {
    this.equipmentService.getAllWithPagination(this.pageNumber, this.pageSize).subscribe({
      next: (res) => {
        this.isLoading = false;
        this.equipments = res;
        this.isDisabledPrev = this.pageNumber <= 1;
        this.isDisabledNext = this.pageNumber >= this.equipments.totalPages;
        console.log(res);
      },
      error: (err) => console.error('Error fetching motors:', err)
    });
  }

  handlePagePrev(): void {
    if (this.pageNumber > 1) {
      this.pageNumber--;
      this.loadEquipment();
    }
  }

  handlePageNext(): void {
    if (this.pageNumber < this.equipments.totalPages) {
      this.pageNumber++;
      this.loadEquipment();
    }
  }

  handlePageSizeChange(event: any): void {
    this.pageSize = +event.target.value;
    this.pageNumber = 1;
    this.loadEquipment();
  }

  onEquipmentSelected(equipmentId: string) {
    this.router.navigate(['/equipments', equipmentId]); 
  }


}
