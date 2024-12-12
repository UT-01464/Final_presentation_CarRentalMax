import { Component, OnInit } from '@angular/core';
import { CarDto, CarService } from '../Services/Cars/car.service';
import { Rental, RentalService } from '../Services/Rental/rental.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-dashboard',
  imports: [FormsModule,CommonModule],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent implements OnInit {


  cars: CarDto[] | undefined;
  totalPendingRentals: number = 0; // Count of pending rentals
  totalAcceptedRentals: number = 0; // Count of accepted rentals
  totalRejectedRentals: number = 0; // Count of rejected rentals
  totalCars: number = 0; // Total cars available
  totalCustomers: number = 15; // Can be fetched if needed
  rentalRequests: Rental[] = [];

  constructor(private carService: CarService, private rentalService:RentalService) {}

  ngOnInit(): void {
    this.loadCars();
    this.loadRentalCounts(); // Load rental counts on initialization
    this.loadRentalRequests(); 
  }

  loadCars(): void {
    this.carService.getCars().subscribe(
      (cars) => {
        this.cars = cars;
        this.totalCars = cars.length; // Update total cars based on fetched data
      },
      (error) => {
        console.error('Error loading cars:', error);
      }
    );
  }





  loadRentalCounts(): void {
    this.rentalService.getPendingCount().subscribe(
      count => this.totalPendingRentals = count,
      error => console.error('Error loading pending count:', error)
    );

    this.rentalService.getAcceptedCount().subscribe(
      count => this.totalAcceptedRentals = count,
      error => console.error('Error loading accepted count:', error)
    );

    this.rentalService.getRejectedCount().subscribe(
      count => this.totalRejectedRentals = count,
      error => console.error('Error loading rejected count:', error)
    );
  }


  loadRentalRequests(): void {
    this.rentalService.getPendingRentals().subscribe(
      (requests) => {
        this.rentalRequests = requests; // Populate rental requests
      },
      (error) => {
        console.error('Error loading rental requests:', error);
      }
    );
  }

  approveRental(id: number): void {
    this.rentalService.acceptRental(id).subscribe(
      () => {
        console.log(`Approved rental with ID: ${id}`);
        this.loadRentalCounts(); // Refresh counts after approval
      },
      (error) => {
        console.error(`Error approving rental ID ${id}:`, error);
      }
    );
  }

  rejectRental(id: number): void {
    this.rentalService.rejectRental(id).subscribe(
      () => {
        console.log(`Rejected rental with ID: ${id}`);
        this.loadRentalCounts(); // Refresh counts after rejection
      },
      (error) => {
        console.error(`Error rejecting rental ID ${id}:`, error);
      }
    );
  }

}
