import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { CardetailsComponent } from '../CarDetails/cardetails/cardetails.component';
import { CarService, CarDto, CarModel } from '../../AdminPage/Services/Cars/car.service';
import { CarDetailsServiceService, FuelType, Transmission } from '../../AdminPage/Services/CarDetaile/car-details-service.service';

@Component({
  selector: 'app-landingpage',
  imports: [CommonModule,FormsModule,CardetailsComponent],
  templateUrl: './landingpage.component.html',
  styleUrl: './landingpage.component.css'
})
export class LandingpageComponent implements OnInit {

  isExpanded: boolean = false;

  toggleReadMore(): void {
    this.isExpanded = !this.isExpanded;
  }

  constructor(private router: Router, private carService: CarService,private carDetailsService: CarDetailsServiceService) {}

  getstart() {
    this.router.navigate(['/login']);
  }

  cars: CarDto[] = [];
  filterText: string = '';
  filteredCars: CarDto[] = [];
  selectedCar: CarDto | null = null;
  

  visibleCars: any[] = []; // Cars to display (up to 6)

  fuelTypes: FuelType[] = [];
  transmissions: Transmission[] = [];
  carModels: CarModel[] = []; 
  

  ngOnInit(): void {
    this.filterCars();
    this.loadCars();
    this.loadFuelTypes();
    this.loadTransmissions();
    this.loadCarModels();



  }




  loadCars(): void {
    this.carService.getCars().subscribe(
      (data: CarDto[]) => {
        this.cars = data;
        this.filteredCars = data; // Initialize filteredCars with fetched data
        this.setVisibleCars(); // Set visible cars after loading
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
    return carModel ? carModel.name : 'Unknown'; // Return model name or 'Unknown' if not found
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
      const modelName = this.getCarModel(car.modelId).toLowerCase(); // Get the model name
      return modelName.includes(this.filterText.toLowerCase()); // Check if it includes the filter text
    });
    this.setVisibleCars(); // Update visible cars after filtering
  }

  openCarDetails(car: CarDto): void {
    this.selectedCar = car; // Set the selected car
  }

  closeCarDetails(): void {
    this.selectedCar = null; // Clear the selected car to close the modal
  }

  viewAllCars(): void {
    this.router.navigate(['/all-cars']);
  }

  setVisibleCars(): void {
    this.visibleCars = this.filteredCars.slice(0, 6); // Only show the first 6 cars
  }








}
