import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { CarService, CarDto } from '../../AdminPage/Services/Cars/car.service';
import { CardetailsComponent } from '../CarDetails/cardetails/cardetails.component';

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

  constructor(private router: Router, private carService: CarService) {}

  ngOnInit(): void {
    this.loadCars();
  }

  getstart() {
    this.router.navigate(['/login']);
  }

  

  loadCars(): void {
    this.carService.getCars().subscribe(
      (data: CarDto[]) => {
        this.cars = data;
        this.filteredCars = data; // Initialize filteredCars with fetched data
      },
      (error) => {
        console.error('Error fetching cars:', error);
      }
    );
  }

  filterCars(): void {
    this.filteredCars = this.cars.filter((car) =>
      car.registrationNumber.toLowerCase().includes(this.filterText.toLowerCase())
    );
  }

  openCarDetails(car: CarDto): void {
    this.selectedCar = car; // Set the selected car
  }

  closeCarDetails(): void {
    this.selectedCar = null; // Clear the selected car to close the modal
  }
 
}
