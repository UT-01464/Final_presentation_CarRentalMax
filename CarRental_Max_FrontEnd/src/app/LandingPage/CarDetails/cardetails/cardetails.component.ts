import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Feature, FuelType, Transmission, CarDetailsServiceService } from '../../../AdminPage/Services/CarDetaile/car-details-service.service';
import { CarDto, CarModel, CarService } from '../../../AdminPage/Services/Cars/car.service';

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
  carModels: CarModel[] = []; 

  constructor(private carDetailsService: CarDetailsServiceService, private carService: CarService) {}

  ngOnInit(): void {
    this.loadAdditionalInfo();
    this.loadCarModels(); // Ensure this method is called
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

  loadCarModels(): void {
    this.carService.getCarModels().subscribe(carModels => {
      this.carModels = carModels;
      console.log('Loaded car models:', this.carModels); // Log loaded models
    }, error => {
      console.error('Error loading car models:', error);
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

  getModelName(modelId: number): string {
    return this.carModels.find(model => model.id === modelId)?.name || 'Unknown'; // Return model name or 'Unknown'
  }
}
