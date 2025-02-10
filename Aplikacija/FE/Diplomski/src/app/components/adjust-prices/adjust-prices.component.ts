import { AfterViewInit, Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MotorService } from '../../services/motorServices';
import { MotorInfo } from '../../models/motorDTO';
import { EquipmentService } from '../../services/equipmentServices';
import { PriceListService } from '../../services/priceListService';
import { CreatePriceListDto, PriceListInfo, PriceListInfo22 } from '../../models/priceListDTO';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ChartConfiguration } from 'chart.js';

@Component({
  selector: 'app-adjust-prices',
  templateUrl: './adjust-prices.component.html',
  styleUrl: './adjust-prices.component.css'
})
export class AdjustPricesComponent implements OnInit {

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
displayForMotor: PriceListInfo | null = null;
displayForEquipment: PriceListInfo22 | null = null;
loading = true; // flagovanje loading

/* ZA CretePriceList*/
creatingPriceListForm: FormGroup;
createMessage: string = "";
errorMessage: string = ''; 
minEndingDate: string = '';


//ZA GRAFIK
priceHistory: { date: string; price: number }[] = [];

lineChartData: ChartConfiguration<'line'>['data'] = {
    datasets: [],
    labels: []
};

lineChartOptions: ChartConfiguration<'line'>['options'] = {
    responsive: true
};


  constructor(
    private motorService: MotorService,
    private equipmentService: EquipmentService,
    private priceListService : PriceListService,
    private router: Router,
    private fb: FormBuilder,
  ) {
    this.creatingPriceListForm = this.fb.group({

      price: [null],
      startingDate: [''],
      endingDate: [''],
    });
  }

  ngOnInit() {
   
    this.creatingPriceListForm = this.fb.group({

      price: [null],
      startingDate: [''],
      endingDate: [''],
    });

    this.creatingPriceListForm.get('startingDate')?.valueChanges.subscribe(startingDate => {
      this.updateMinEndingDate(startingDate);
    });
  }

  updateMinEndingDate(startingDate: string) {
    if (startingDate) {
      
      const startDate = new Date(startingDate);
      startDate.setDate(startDate.getDate() + 1);

      this.minEndingDate = startDate.toISOString().split('T')[0];

      // If endingDate is less than or equal to the startingDate, reset it
      const endingDate = this.creatingPriceListForm.get('endingDate')?.value;
      if (endingDate && new Date(endingDate) <= startDate) {
        this.creatingPriceListForm.get('endingDate')?.setValue('');
      }
    }
  }




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

  

  setActiveItem(item: any, index: number): void { 
    console.log('Selected Item:', item); 

    this.currentItem = item;
    this.currentIndex = index;
    this.errorMessage = ''; 

    if (!item.id) {
        console.error('Error: Item does not have an ID', item);
        return;
    }

    if (this.role === 'MOTORCYCLE') {
        this.priceListService.getCurrentPriceListByMotorId(item.id).subscribe({
            next: (res) => {
                this.displayForMotor = res;
                this.errorMessage = '';
                this.loading = false;

               //pre-filluj formu
                this.creatingPriceListForm.patchValue({
                    price: res?.price || null,
                    startingDate: res?.startingDate || '',
                    endingDate: res?.endingDate || '',
                });
                console.log("Displayed Motor:", res);

                //this.loadPriceHistory('motor', item.id); //za grafik
            },
            error: (err) => {
                this.displayForMotor = null;
                this.errorMessage = 'No prices for current motor';
                console.error('Error fetching motor details:', err);
                this.loading = false;
                this.creatingPriceListForm.patchValue({
                  price: '',
                  startingDate: '',
                  endingDate: '',
              });
              //this.clearPriceListDisplay('motor'); //za grafik
            }
        });
    } else if (this.role === 'EQUIPMENT') {
        this.priceListService.getCurrentPriceListByEquipmentId(item.id).subscribe({
            next: (res) => {
                this.displayForEquipment = res;
                this.errorMessage = '';
                this.loading = false;

                // Pre-fill the form with current price list data
                this.creatingPriceListForm.patchValue({
                    price: res?.price || null,
                    startingDate: res?.startingDate || '',
                    endingDate: res?.endingDate || '',
                });

                console.log("Displayed Equipment:", res);

                //this.loadPriceHistory('equipment', item.id); //za grafik
            },
            error: (err) => {
                this.displayForEquipment = null;
                this.errorMessage = 'No prices for current equipment';
                console.error('Error fetching equipment details:', err);
                this.loading = false;
                this.creatingPriceListForm.patchValue({
                  price: '',
                  startingDate: '',
                  endingDate: '',
              });
            }
        });
    }
}
  
  //ZA CREATE
  handleSubmit() {
    if (this.creatingPriceListForm.valid) {
      const motorData: CreatePriceListDto = this.creatingPriceListForm.value;
    
      this.priceListService.createPriceList(this.currentItem.id, motorData).subscribe({
        
        next: () => {
          console.log("ProductId je: ", this.currentItem.id);
          this.createMessage = "Uspesno dodat!";
          setTimeout(() => {
            this.router.navigate(['/all-motorcycles']);
          }, 800);
        },
        error: (error) => {
          console.error('Error adding motor ', error);
          console.log(motorData);
        }
      });
    }
  }


  //ZA GRAFIK
/*
  loadPriceHistory(type: 'motor' | 'equipment', id: string) {
    const priceListObservable =
        type === 'motor'
            ? this.priceListService.getAllPricesListByMotorId(id)
            : this.priceListService.getAllPricesListByEquipmentId(id);

    priceListObservable.subscribe({
        next: (data : any) => {
            this.priceHistory = data.map((entry :any) => ({
                date: entry.startingDate,
                price: entry.price
            })).sort((a, b) => new Date(a.date).getTime() - new Date(b.date).getTime());

            this.updateChart();
        },
        error: (err) => {
            console.error('Error fetching price history:', err);
            this.priceHistory = [];
        }
    });
}

updateChart() {
    this.lineChartData = {
        datasets: [
            {
                data: this.priceHistory.map(entry => entry.price),
                label: 'Price Over Time',
                borderColor: 'rgba(75,192,192,1)',
                backgroundColor: 'rgba(75,192,192,0.2)',
                fill: true,
            }
        ],
        labels: this.priceHistory.map(entry => entry.date),
    };
}
*/
}
