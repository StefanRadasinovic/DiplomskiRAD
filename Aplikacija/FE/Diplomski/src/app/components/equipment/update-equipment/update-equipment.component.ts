import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { EquipmentService } from '../../../services/equipmentServices';
import { UpdateEquipmentDto } from '../../../models/equipmentDTO';

@Component({
  selector: 'app-update-equipment',
  templateUrl: './update-equipment.component.html',
  styleUrl: './update-equipment.component.css'
})
export class UpdateEquipmentComponent implements OnInit {

    equipmentForm!: FormGroup;
    equipmentId!: string;
    createMessage: string = '';
    imagePreview: string | ArrayBuffer | null = null;
    originalFileName: string | undefined;
    dropZoneWidth: number = 0;
    dropZoneHeight: number = 0;
  
    constructor(private fb: FormBuilder, private route: ActivatedRoute,
                private equipmentService: EquipmentService, private router: Router) {}
  
    @ViewChild('dropZone', { static: false }) dropZone!: ElementRef<HTMLDivElement>;
  
    ngOnInit(): void {
      this.equipmentId = this.route.snapshot.paramMap.get('id')!;
      this.initializeForm();
      this.loadMotorDetails();
      if (this.dropZone) {
        const dropZoneElement = this.dropZone.nativeElement;
        this.dropZoneWidth = dropZoneElement.offsetWidth;
        this.dropZoneHeight = dropZoneElement.offsetHeight;
        console.log('Drop Zone Width:', this.dropZoneWidth, 'Height:', this.dropZoneHeight);
      }
    }
  
    initializeForm(): void {
      this.equipmentForm = this.fb.group({
        name: ['', Validators.required],
        equipmentState: [''],
        amount: [''],
        slika: [''] // Ensure this matches the expected field name
      });
    }
  
    loadMotorDetails(): void {
      this.equipmentService.getEquipmentById(this.equipmentId).subscribe({
        next: (eq) => {
          this.equipmentForm.patchValue({
            name: eq.name,
            equipmentState: eq.equipmentState,
            amount: eq.amount,
            slika: eq.slika || '' // Default to an empty string if slika is undefined
          });
          this.imagePreview = eq.slika || null; // Set the initial image preview
          console.log(eq);
        },
        error: (err) => console.error('Error fetching motor details:', err)
      });
    }
  
    handleSubmit(): void {
      if (this.equipmentForm.valid) {
        const updatedMotor: UpdateEquipmentDto = this.equipmentForm.value;
        updatedMotor.slika = this.originalFileName;
        
        console.log('Equipment Object Being Uploaded:', updatedMotor); // Log the entire object
        this.equipmentService.updateEquipment(this.equipmentId, updatedMotor).subscribe({
          next: () => {
            this.createMessage = "Updated successfully!";
            console.log(updatedMotor);
            setTimeout(() => {
              this.router.navigate(['/all-equipments']);
            }, 800);
          },
          error: (err) => {
            this.createMessage = 'Error updating equipment';
            console.error('Error updating equipment:', err);
          }
        });
      } else {
        console.log('Form is invalid:', this.equipmentForm.errors);
      }
    }
  
    onFileSelected(event: Event): void {
    const file = (event.target as HTMLInputElement).files?.[0];
    if (file) {
      console.log('Picture being uploaded: ', file.name);
      this.originalFileName = file.name;
      const reader = new FileReader();
      reader.onload = (e) => {
        // Set the image preview to the base64 data
        this.imagePreview = e.target?.result as string; // Preview for the uploaded image
      };
      reader.readAsDataURL(file);
  
    }
    
  }
  
    onDrop(event: DragEvent) {
      event.preventDefault(); 
      if (event.dataTransfer && event.dataTransfer.files) {
        const file = event.dataTransfer.files[0];
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
          const widthRatio = this.dropZoneWidth / img.width;
          const heightRatio = this.dropZoneHeight / img.height;
          const scalingFactor = Math.min(widthRatio, heightRatio);
    
          const newWidth = img.width * scalingFactor;
          const newHeight = img.height * scalingFactor;
          
          const canvas = document.createElement('canvas');
          canvas.width = newWidth;
          canvas.height = newHeight;
          
          const ctx = canvas.getContext('2d');
          if (ctx) {
            ctx.drawImage(img, 0, 0, newWidth, newHeight); 
            const resizedImageDataUrl = canvas.toDataURL('image/jpeg');
            this.imagePreview = resizedImageDataUrl; // Update the preview
          }
        };
      };
    
      reader.readAsDataURL(file);
    }
}
