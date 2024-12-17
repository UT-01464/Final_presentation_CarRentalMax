import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { CarService, CarDto, CarModel } from '../../AdminPage/Services/Cars/car.service';
import { CardetailsComponent } from '../CarDetails/cardetails/cardetails.component';
import { FuelType, Transmission, CarDetailsServiceService } from '../../AdminPage/Services/CarDetaile/car-details-service.service';

@Component({
  selector: 'app-all-cars',
  imports: [CommonModule,FormsModule,CardetailsComponent],
  templateUrl: './all-cars.component.html',
  styleUrl: './all-cars.component.css'
})
export class AllCarsComponent implements OnInit{
  cars: CarDto[] = [];
  filterText: string = '';
  filteredCars: CarDto[] = [];
  selectedCar: CarDto | null = null;
  isScrolled = false;
  fuelTypes: FuelType[] = [];
    transmissions: Transmission[] = [];
    carModels: CarModel[] = []; 

  constructor(private router: Router, private carService: CarService,private carDetailsService: CarDetailsServiceService) {}

  ngOnInit(): void {
    this.loadCars();
    this.loadFuelTypes();
    this.loadTransmissions();
    this.loadCarModels();
  }

  getstart() {
    this.router.navigate(['/login']);
  }

  

  loadCars(): void {
    this.carService.getCars().subscribe(
      (data: CarDto[]) => {
        this.cars = data;
        this.filteredCars = data; 
      },
      (error) => {
        console.error('Error fetching cars:', error);
      }
    );
  }

  loadFuelTypes(): void {
    this.carDetailsService.getFuelTypes().subscribe(
      (data: FuelType[]) => {
        this.fuelTypes = data;
      },
      (error) => {
        console.error('Error fetching fuel types:', error);
      }
    );
  }

  loadTransmissions(): void {
    this.carDetailsService.getTransmissions().subscribe(
      (data: Transmission[]) => {
        this.transmissions = data;
      },
      (error) => {
        console.error('Error fetching transmissions:', error);
      }
    );
  }

  loadCarModels(): void {
    this.carService.getCarModels().subscribe(
      (data: CarModel[]) => {
        this.carModels = data;
      },
      (error) => {
        console.error('Error fetching car models:', error);
      }
    );
  }



  getCarModel(id: number): string {
    const carModel = this.carModels.find(m => m.id === id);
    return carModel ? carModel.name : 'Unknown'; 
  }


  getFuelType(id: number): string {
    const fuelType = this.fuelTypes.find(f => f.id === id);
    return fuelType ? fuelType.type : 'Unknown';
  }

  getTransmissionType(id: number): string {
    const transmission = this.transmissions.find(t => t.id === id);
    return transmission ? transmission.type : 'Unknown';
  }

  filterCars(): void {
    this.filteredCars = this.cars.filter((car) => {
      const modelName = this.getCarModel(car.modelId).toLowerCase(); 
      return modelName.includes(this.filterText.toLowerCase()); 
    });
    
  }

  openCarDetails(car: CarDto): void {
    this.selectedCar = car; // Set the selected car
  }

  closeCarDetails(): void {
    this.selectedCar = null; // Clear the selected car to close the modal
  }
 
}
