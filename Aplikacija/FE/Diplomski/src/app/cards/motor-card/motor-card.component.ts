import { Component, EventEmitter, Input, Output } from '@angular/core';
import { MotorInfo } from '../../models/motorDTO';

@Component({
  selector: 'app-motor-card',
  templateUrl: './motor-card.component.html',
  styleUrl: './motor-card.component.css'
})

export class MotorCardComponent {
  @Input() motor!: MotorInfo;
  @Output() motorSelected = new EventEmitter<string>(); // Emit the motor ID when selected
  resizedImage: string | null = null;

  ngOnInit() {
    if (this.motor.slika) {
      this.resizeImage(this.motor.slika);
    }
  }

  getImage() {
    return this.resizedImage || 'https://placehold.co/300x300/EEE/31343C';
  }

  private resizeImage(imageData: string) {
    const img = new Image();
    img.src = imageData;

    img.onload = () => {
      const canvas = document.createElement('canvas');
      canvas.width = 300;
      canvas.height = 300;

      const ctx = canvas.getContext('2d');
      if (ctx) {
        ctx.drawImage(img, 0, 0, 300, 300);
        this.resizedImage = canvas.toDataURL('image/jpeg');
      }
    };
  }

  onCardClick() {
    this.motorSelected.emit(this.motor.id);
  }
}

