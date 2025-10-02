import { Component, EventEmitter, Input, Output } from '@angular/core';
import { EquipmentInfo } from '../../models/equipmentDTO';

@Component({
  selector: 'app-equipment-card',
  templateUrl: './equipment-card.component.html',
  styleUrl: './equipment-card.component.css'
})
export class EquipmentCardComponent {

@Input() equipment!: EquipmentInfo;
@Output() motorSelected = new EventEmitter<string>(); // Emit the motor ID when selected
resizedImage: string | null = null;

  ngOnInit() {
    if (this.equipment.slika) {
      this.resizeImage(this.equipment.slika);
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
    this.motorSelected.emit(this.equipment.id);
  }

}
