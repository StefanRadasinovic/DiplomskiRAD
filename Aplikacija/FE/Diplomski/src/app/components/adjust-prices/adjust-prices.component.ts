import { AfterViewInit, Component } from '@angular/core';
import { Router } from '@angular/router';
import { MotorService } from '../../services/motorServices';
import { MotorInfo } from '../../models/motorDTO';
import { EquipmentService } from '../../services/equipmentServices';
import { PriceListService } from '../../services/priceListService';
import { PriceListInfo, PriceListInfo22 } from '../../models/priceListDTO';

@Component({
  selector: 'app-adjust-prices',
  templateUrl: './adjust-prices.component.html',
  styleUrl: './adjust-prices.component.css'
})
export class AdjustPricesComponent {

  name: string = '';
  producerName: string = '';
  role: string = '';
  isLoading = false;
  motors: any[] = [];
  data: any[] = []; 
  displayedColumns: string[] = [];

  currentIndex = -1;
  currentItem: any = null;
  

  /**DEO ZA GETbyId PRIKAZ***/
  displayForMotor!: PriceListInfo;
  displayForEquipment!: PriceListInfo22;
  loading = true; // Add a loading flag

  constructor(
    private motorService: MotorService,
    private equipmentService: EquipmentService,
    private priceListService : PriceListService,
    private router: Router
  ) {}

  isButtonEnabled(): boolean {
    return Boolean(this.role && (this.name || this.producerName));
  }

  SearchFunction() {
    this.isLoading = true;
  
    if (this.role === 'MOTORCYCLE') {
      //this.displayedColumns = ['Name', 'Producer', 'Motorcycle Type', 'Price'];
      this.displayedColumns = ['Name', 'Producer', 'Motorcycle Type'];

      if (this.name && this.producerName) { //search combine
        
        this.motorService.getMotorsByNameAndProducerName(this.name, this.producerName)
        .subscribe(motors => {
          this.data = motors.map(m => ({
            id: m.id, 
            name: m.name,
            producer: m.producers[0].name,
            motorType: m.motorcycleType
          }));
          console.log('Processed Motors:', this.data); 
          this.isLoading = false;
        });
      } else if (this.name) { //  search name
        
        this.motorService.getMotorsByName(this.name)
          .subscribe(motors => {
            this.data = motors.map(m => ({
              id: m.id, 
              name: m.name,
              producer: m.producers[0].name,
              motorType: m.motorcycleType
            }));
            console.log('Processed Motors:', this.data); 
            this.isLoading = false;
          });
      } else if (this.producerName) { // search producer
        
        this.motorService.getMotorsByProducerName(this.producerName)
          .subscribe(motors => {
            this.data = motors.map(m => ({
              id: m.id, 
              name: m.name,
              producer: m.producers[0].name,
              motorType: m.motorcycleType
            }));
            console.log('Processed Motors:', this.data); 
            this.isLoading = false;
          });
      }
  
    } else if (this.role === 'EQUIPMENT') {
     //this.displayedColumns = ['Name', 'Producer', 'Equipment State', 'Price'];
      this.displayedColumns = ['Name', 'Producer', 'Equipment State'];

      if (this.name && this.producerName) { //search combine
        // Call combined search
        this.equipmentService.getEquipmentByNameAndProducerName(this.name, this.producerName)
          .subscribe(equipment => {
            this.data = equipment.map(m => ({
              id: m.id, 
              name: m.name,
              producer: m.producers[0].name,
              equipmentState: m.equipmentState
            }));
            console.log('Processed Equipment:', this.data); 
            this.isLoading = false;
          });
      } else if (this.name) { //  search name
        
        this.equipmentService.getEquipmentByName(this.name)
          .subscribe(equipment => {
            this.data = equipment.map(m => ({
              id: m.id, 
              name: m.name,
              producer: m.producers[0].name,
              equipmentState: m.equipmentState
            }));
            console.log('Processed Equipment:', this.data); 
            this.isLoading = false;
          });
      } else if (this.producerName) {  // search producer
      
        this.equipmentService.getEquipmentByProducerName(this.producerName)
          .subscribe(equipment => {
            this.data = equipment.map(m => ({
              id: m.id, 
              name: m.name,
              producer: m.producers[0].name,
              equipmentState: m.equipmentState
            }));
            console.log('Processed Equipment:', this.data); 
            this.isLoading = false;
          });
      }
    }
  }

  transformMotorData(motors: any[]): any[] {
    return motors.map(motor => ({
      name: motor.name,
      producer: motor.producers[0]?.name || 'Unknown',
      motorType: motor.motorcycleType,
      //price: motor.displayPriceOnly?.length > 0 ? motor.displayPriceOnly[0].price : 'Without_Price'
    }));
  }

  transformEquipmentData(equipment: any[]): any[] {
    return equipment.map(equip => ({
      name: equip.name,
      producer: equip.producers[0]?.name || 'Unknown',
      equipmentState: equip.equipmentState,
      //price: equip.displayPriceOnly?.length > 0 ? equip.displayPriceOnly[0].price : 'Without_Price'
    }));
  }

  

  setActiveItem(item: any, index: number): void {//Routing
    console.log('Selected Item:', item); 

    this.currentItem = item;
    this.currentIndex = index;

    if (!item.id) {
        console.error('Error: Item does not have an ID', item);
        return;
    }

    if (this.role === 'MOTORCYCLE') {
      //this.router.navigate([`/motorcycles/${item.id}`]);
      this.priceListService.getCurrentPriceListByMotorId(item.id).subscribe({
        next: (res) => {
          this.displayForMotor = res;
          this.loading = false; // Set loading to false after data is fetched
          console.log("Prikazan predmet:", res);
        },
        error: (err) => {
          console.error('Error fetching motor details:', err);
          this.loading = false; // Set loading to false on error as well
        }
      });

    } else if (this.role === 'EQUIPMENT') {
      //this.router.navigate([`/equipment/${item.id}`]);
      this.priceListService.getCurrentPriceListByEquipmentId(item.id).subscribe({
        next: (res) => {
          this.displayForEquipment = res;
          this.loading = false; // Set loading to false after data is fetched
          console.log("Prikazan predmet:",res);
        },
        error: (err) => {
          console.error('Error fetching equipment details:', err);
          this.loading = false; // Set loading to false on error as well
        }
      });
    }

  }
  


}
