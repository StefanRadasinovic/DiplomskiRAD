import { AfterViewInit, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CreateServiceDto } from '../../../models/serviceDTO';
import { ServiceService } from '../../../services/serviceService';
import { AuthorisationService } from '../../../services/authorisationService';
import { UserService } from '../../../services/userService';
import { DisplayClientDto, DisplayDirectorDto, DisplayWorkerDto } from '../../../models/userDTO';

@Component({
  selector: 'app-add-service',
  templateUrl: './add-service.component.html',
  styleUrl: './add-service.component.css'
})
export class AddServiceComponent implements AfterViewInit, OnInit{

equipmentForm: FormGroup;
createMessage: string = "";
imagePreview: string | ArrayBuffer | null = null;
originalFileName: string | undefined;
dropZoneWidth: number = 0;
dropZoneHeight: number = 0;  
user!: DisplayWorkerDto | DisplayClientDto | DisplayDirectorDto; 


@ViewChild('dropZone', { static: false }) dropZone!: ElementRef<HTMLDivElement>;

constructor(
    private fb: FormBuilder,
    private router: Router,
    private serviceService: ServiceService,
    private authService : AuthorisationService,
    private userService: UserService,
  ) {
    this.equipmentForm = this.fb.group({
     
      failureDescription: ['', Validators.required],
      picture: [''] ,
      startDate: [''],
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

  ngOnInit(): void {

    const userId = this.authService.getUserId();
    if (userId !== null) {
      
      this.getUserById(userId);  
      
    } else {
      console.error('Invalid User ID');
    }
  }

  getUserById(id: string): void {
    this.userService.getUserById(id).subscribe({
      next: (res) => {
        this.user = res;
        console.log('KORISNIK JE:', res);

      },
      error: (err) => {
        console.error('Error fetching user details:', err);
      }
    });
  }

  handleSubmit() {
    if (this.equipmentForm.valid) {
      const motorData: CreateServiceDto = this.equipmentForm.value;
      motorData.picture = this.originalFileName; 

      this.serviceService.createService( this.user.id, motorData).subscribe({
        next: () => {
          this.createMessage = "Uspesno dodat!";
          setTimeout(() => {
            this.router.navigate(['/all-service', this.user.id]); 
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
