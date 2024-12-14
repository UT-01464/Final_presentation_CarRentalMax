import { Component, OnInit } from '@angular/core';
import { CarDto, CarService } from '../Services/Cars/car.service';
import { Rental, RentalResponse, RentalService } from '../Services/Rental/rental.service';
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
  totalPendingRentals: number = 0;
  totalAcceptedRentals: number = 0;
  totalRejectedRentals: number = 0;
  totalCars: number = 0;
  totalCustomers: number = 15;
  rentalRequests: RentalResponse[] = [];

  constructor(private carService: CarService, private rentalService: RentalService) {}

  ngOnInit(): void {
    this.loadCars();
    this.loadRentalCounts();
    this.loadRentalRequests();
  }

  loadCars(): void {
    this.carService.getCars().subscribe(
      (cars) => {
        this.cars = cars;
        this.totalCars = cars.length;
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
        this.rentalRequests = requests;
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
        this.loadRentalCounts();
        this.loadRentalRequests(); // Refresh rental requests after approval
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
        this.loadRentalCounts();
        this.loadRentalRequests(); // Refresh rental requests after rejection
      },
      (error) => {
        console.error(`Error rejecting rental ID ${id}:`, error);
      }
    );
  }

}
