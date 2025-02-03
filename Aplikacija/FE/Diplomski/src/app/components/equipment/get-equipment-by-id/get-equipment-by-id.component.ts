import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { EquipmentService } from '../../../services/equipmentServices';
import { Equipment } from '../../../models/equipmentDTO';
import { DialogComponent } from '../../dialog/dialog.component';

@Component({
  selector: 'app-get-equipment-by-id',
  templateUrl: './get-equipment-by-id.component.html',
  styleUrl: './get-equipment-by-id.component.css'
})
export class GetEquipmentByIdComponent implements OnInit {

equipment!: Equipment;
loading = true; // Add a loading flag

constructor(
    private route: ActivatedRoute,
    private equipmentService: EquipmentService,
    private dialog: MatDialog,
    private router: Router
  ) {}

ngOnInit(): void {
    const eqipmentId = this.route.snapshot.paramMap.get('id');
    if (eqipmentId !== null) {
      console.log('Equipment ID from route:', eqipmentId);
      this.getEquipmentById(eqipmentId.toString());
    } else {
      console.error('Invalid Equipment ID');
    }
}

getEquipmentById(id: string): void {
    this.equipmentService.getEquipmentById(id).subscribe({
      next: (res) => {
        this.equipment = res;
        if(this.equipment.slika == '' || this.equipment.slika == null) this.equipment.slika ='https://placehold.co/700x300/EEE/31343C?text=300 x 300';
        this.loading = false; // Set loading to false after data is fetched
        console.log(res);
      },
      error: (err) => {
        console.error('Error fetching motor details:', err);
        this.loading = false; // Set loading to false on error as well
      }
    });
}

deleteEquipment(): void {
    const dialogRef = this.dialog.open(DialogComponent);
  
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        const motorId = this.route.snapshot.paramMap.get('id');  
        if (motorId !== null) {
          this.equipmentService.delete(motorId).subscribe({
            next: () => {
              console.log('Equipment deleted successfully');
              this.router.navigate(['/all-equipments']);
            },
            error: (err) => console.error('Error deleting Equipment:', err)
          });
        }
      }
    });
}

editEquipment(motorId: string): void {
    this.router.navigate(['/edit-equipments', motorId]);
}

}
