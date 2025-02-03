import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MotorService } from '../../services/motorServices';
import { UpdateMotorDto } from '../../models/motorDTO';

@Component({
  selector: 'app-update-motor',
  templateUrl: './update-motor.component.html',
  styleUrl: './update-motor.component.css'
})
export class UpdateMotorComponent implements OnInit {
  motorForm!: FormGroup;
  motorId!: string;
  createMessage: string = '';
  imagePreview: string | ArrayBuffer | null = null;
  originalFileName: string | undefined;
  dropZoneWidth: number = 0;
  dropZoneHeight: number = 0;

  constructor(private fb: FormBuilder, private route: ActivatedRoute,
              private motorService: MotorService, private router: Router) {}

  @ViewChild('dropZone', { static: false }) dropZone!: ElementRef<HTMLDivElement>;

  ngOnInit(): void {
    this.motorId = this.route.snapshot.paramMap.get('id')!;
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
    this.motorForm = this.fb.group({
      name: ['', Validators.required],
      kilometraza: [''],
      yearOfProduction: [''],
      motorcycleState: [''],
      motorcycleType: [''],
      amount: [''],
      slika: [''] // Ensure this matches the expected field name
    });
  }

  loadMotorDetails(): void {
    this.motorService.getMotorById(this.motorId).subscribe({
      next: (motor) => {
        this.motorForm.patchValue({
          name: motor.name,
          kilometraza: motor.kilometraza,
          yearOfProduction: motor.yearOfProduction,
          motorcycleState: motor.motorcycleState,
          motorcycleType: motor.motorcycleType,
          amount: motor.amount,
          slika: motor.slika || '' // Default to an empty string if slika is undefined
        });
        this.imagePreview = motor.slika || null; // Set the initial image preview
        console.log(motor);
      },
      error: (err) => console.error('Error fetching motor details:', err)
    });
  }

  handleSubmit(): void {
    if (this.motorForm.valid) {
      const updatedMotor: UpdateMotorDto = this.motorForm.value;
      updatedMotor.Slika = this.originalFileName;
      
      console.log('Motor Object Being Uploaded:', updatedMotor); // Log the entire object
      this.motorService.updateMotor(this.motorId, updatedMotor).subscribe({
        next: () => {
          this.createMessage = "Updated successfully!";
          setTimeout(() => {
            this.router.navigate(['/all-motorcycles']);
          }, 800);
        },
        error: (err) => {
          this.createMessage = 'Error updating motor';
          console.error('Error updating motor:', err);
        }
      });
    } else {
      console.log('Form is invalid:', this.motorForm.errors);
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
