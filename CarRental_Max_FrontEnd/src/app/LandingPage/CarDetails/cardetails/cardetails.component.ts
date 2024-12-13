import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Feature, FuelType, Transmission, CarDetailsServiceService } from '../../../AdminPage/Services/CarDetaile/car-details-service.service';
import { CarDto } from '../../../AdminPage/Services/Cars/car.service';

@Component({
  selector: 'app-cardetails',
  imports: [CommonModule,FormsModule],
  templateUrl: './cardetails.component.html',
  styleUrl: './cardetails.component.css'
})
export class CardetailsComponent {

  @Input() car: CarDto | null = null;
  @Output() close = new EventEmitter<void>();

  features: Feature[] = [];
  fuelTypes: FuelType[] = [];
  transmissions: Transmission[] = [];

  constructor(private carDetailsService: CarDetailsServiceService) {}

  ngOnInit(): void {
    this.loadAdditionalInfo();
  }

  loadAdditionalInfo(): void {
    this.carDetailsService.getFeatures().subscribe(features => {
      this.features = features;
    });

    this.carDetailsService.getFuelTypes().subscribe(fuelTypes => {
      this.fuelTypes = fuelTypes;
    });

    this.carDetailsService.getTransmissions().subscribe(transmissions => {
      this.transmissions = transmissions;
    });
  }

  closeModal() {
    this.close.emit();
  }

  getFeatureNames(featureIds: number[]): string {
    return featureIds.map(id => this.features.find(feature => feature.id === id)?.name).join(', ');
  }

  getFuelTypeName(fuelTypeId: number): string {
    return this.fuelTypes.find(fuelType => fuelType.id === fuelTypeId)?.type || 'Unknown';
  }

  getTransmissionName(transmissionId: number): string {
    return this.transmissions.find(transmission => transmission.id === transmissionId)?.type || 'Unknown';
  }
}
