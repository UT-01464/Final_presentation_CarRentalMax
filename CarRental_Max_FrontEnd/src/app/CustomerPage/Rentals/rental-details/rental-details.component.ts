import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

interface Rental {
  carImage: string;
  registrationNumber: string;
  model: string;
  rentDate: Date;
  returnDate: Date;
  isOverdue?: boolean;
  status: string;
}







@Component({
  selector: 'app-rental-details',
  imports: [CommonModule,FormsModule],
  templateUrl: './rental-details.component.html',
  styleUrl: './rental-details.component.css'
})
export class RentalDetailsComponent {

  pendingRequests: Rental[] = []; // Populate this with actual data
  rentedCars: Rental[] = []; // Populate this with actual data
  returnHistory: Rental[] = []; // Populate this with actual data

  constructor(private router: Router) {}

  ngOnInit() {
    // Fetch rental data here (e.g., from a service)
    this.loadRentalData();
  }

  loadRentalData() {
    // Simulated data for demonstration
    this.pendingRequests = [
      {
        carImage: 'path/to/car1.jpg',
        registrationNumber: 'ABC123',
        model: 'Toyota Camry',
        rentDate: new Date(),
        returnDate: new Date(),
        status: 'Pending'
      }
    ];
    this.rentedCars = [
      {
        carImage: 'path/to/car2.jpg',
        registrationNumber: 'XYZ789',
        model: 'Honda Civic',
        rentDate: new Date(),
        returnDate: new Date(),
        isOverdue: false,
        status: 'Rented'
      }
    ];
    this.returnHistory = [
      {
        carImage: 'path/to/car3.jpg',
        registrationNumber: 'LMN456',
        model: 'Ford Focus',
        rentDate: new Date(),
        returnDate: new Date(),
        status: 'Returned'
      }
    ];
  }

  goBack(): void {
    this.router.navigate(['/userpage']); // Navigate back to User Page
  }

}
