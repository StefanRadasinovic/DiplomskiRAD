import { AfterViewInit, Component, ElementRef, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { EquipmentService } from '../../../services/equipmentServices';
import { CreateEquipmentDto } from '../../../models/equipmentDTO';

@Component({
  selector: 'app-add-equipment',
  templateUrl: './add-equipment.component.html',
  styleUrl: './add-equipment.component.css'
})
export class AddEquipmentComponent implements AfterViewInit{

equipmentForm: FormGroup;
createMessage: string = "";
imagePreview: string | ArrayBuffer | null = null;
originalFileName: string | undefined;
dropZoneWidth: number = 0;
dropZoneHeight: number = 0;  

@ViewChild('dropZone', { static: false }) dropZone!: ElementRef<HTMLDivElement>;

constructor(
    private fb: FormBuilder,
    private equipmentService: EquipmentService,
    private router: Router
  ) {
    this.equipmentForm = this.fb.group({
     
      name: ['', Validators.required],
      slika: [''] ,
      amount: [null],
      equipmentState: [''],
    });
  }

  ngAfterViewInit() {
    if (this.dropZone) {
      const dropZoneElement = this.dropZone.nativeElement;
      this.dropZoneWidth = dropZoneElement.offsetWidth;
      this.dropZoneHeight = dropZoneElement.offsetHeight;
      console.log('Drop Zone Width:', this.dropZoneWidth, 'Height:', this.dropZoneHeight);
    }
  }

  handleSubmit() {
    if (this.equipmentForm.valid) {
      const motorData: CreateEquipmentDto = this.equipmentForm.value;
      motorData.slika = this.originalFileName; 

      this.equipmentService.createEquipment(motorData).subscribe({
        next: () => {
          this.createMessage = "Uspesno dodat!";
          setTimeout(() => {
            this.router.navigate(['/all-equipments']);
          }, 800);
        },
        error: (error) => {
          console.error('Error adding motor ', error);
          console.log(motorData);
        }
      });
    }
  }

  onFileSelected(event: Event) {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files[0]) {
      const file = input.files[0];
      console.log('File selected:', file);
      this.originalFileName = file.name; 
      this.resizeImage(file);
    }
  }

  onDrop(event: DragEvent) {
    event.preventDefault(); 
    if (event.dataTransfer && event.dataTransfer.files) {
      const file = event.dataTransfer.files[0];
      console.log('File dropped:', file); 
      this.originalFileName = file.name;
      this.resizeImage(file); 
    }
  }

  onDragOver(event: DragEvent) {
    event.preventDefault(); 
  }

  private resizeImage(file: File) {
    const reader = new FileReader();
    reader.onload = () => {
      const img = new Image();
      img.src = reader.result as string;
  
      img.onload = () => {
        console.log(`Original Image Width: ${img.width}, Height: ${img.height}`);
        
        const widthRatio = this.dropZoneWidth / img.width;
        const heightRatio = this.dropZoneHeight / img.height;
        const scalingFactor = Math.min(widthRatio, heightRatio);
  
        const newWidth = img.width * scalingFactor;
        const newHeight = img.height * scalingFactor;
        console.log(`Resized Image Width: ${newWidth}, Height: ${newHeight}`);
        
        //Create canvas for resizing and adjust widht nad height
        const canvas = document.createElement('canvas');
        canvas.width = newWidth;
        canvas.height = newHeight;
        
        //resize
        const ctx = canvas.getContext('2d');
        if (ctx) {
          ctx.drawImage(img, 0, 0, newWidth, newHeight); //draw new one 
  
          const resizedImageDataUrl = canvas.toDataURL('image/jpeg');
          this.imagePreview = resizedImageDataUrl; // Keep the preview
        }
      };
    };
  
    reader.readAsDataURL(file);
  }


}
