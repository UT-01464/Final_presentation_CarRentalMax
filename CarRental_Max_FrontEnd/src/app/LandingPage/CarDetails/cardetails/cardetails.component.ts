import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-cardetails',
  imports: [CommonModule,FormsModule],
  templateUrl: './cardetails.component.html',
  styleUrl: './cardetails.component.css'
})
export class CardetailsComponent {

  @Input() car: any;
  @Output() close = new EventEmitter<void>();

  closeModal() {
    this.close.emit();
  }

}
