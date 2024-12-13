import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { CardetailsComponent } from '../CarDetails/cardetails/cardetails.component';
import { CarService, CarDto } from '../../AdminPage/Services/Cars/car.service';

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

  constructor(private router: Router, private carService: CarService) {}

  getstart() {
    this.router.navigate(['/login']);
  }

  cars: CarDto[] = [];
  filterText: string = '';
  filteredCars: CarDto[] = [];
  selectedCar: CarDto | null = null;
  

  visibleCars: any[] = []; // Cars to display (up to 6)
  

  ngOnInit(): void {
    this.filterCars();
    this.loadCars();
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

  viewAllCars(): void {
    this.router.navigate(['/all-cars']);
  }
  
  setVisibleCars(): void {
    this.visibleCars = this.filteredCars.slice(0, 6); // Only show the first 6 cars
  }








}
