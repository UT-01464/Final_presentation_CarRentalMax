import { Component, OnInit } from '@angular/core';
import { CarDto, CarService } from '../Services/Cars/car.service';

@Component({
  selector: 'app-dashboard',
  imports: [],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent implements OnInit {


  cars:CarDto[] | undefined;

  totalRentals: number = 5; // Hard-coded value
  totalCars: number =0;    // Hard-coded value
  totalCustomers: number = 15; // Hard-coded value

  constructor(private carService:CarService) {}

  ngOnInit(): void {
    this.loadCars();
  }

  loadCars():void
  {
    this.carService.getCars().subscribe
    (
      (cars)=>
      {
        this.cars=cars;
        this.totalCars=cars.length
      },
      (error) => {
        console.error('Error loading cars:', error);
      }
    );
  }





  approveRental(id: number): void {
    // Logic to approve rental
    console.log(`Approved rental with ID: ${id}`);
  }

  rejectRental(id: number): void {
    // Logic to reject rental
    console.log(`Rejected rental with ID: ${id}`);
  }

}
