import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { RentalService, RentalStatus } from '../../../AdminPage/Services/Rental/rental.service';
import { Rental } from '../../../AdminPage/Services/Rental/rental.service'; 
import { AuthServiceService } from '../../../Services/auth-service.service';

@Component({
  selector: 'app-rental-details',
  imports: [CommonModule,FormsModule],
  templateUrl: './rental-details.component.html',
  styleUrl: './rental-details.component.css'
})
export class RentalDetailsComponent {

  pendingRequests: Rental[] = [];
  rentedCars: Rental[] = [];
  returnHistory: Rental[] = [];

  constructor(private router: Router, private rentalService: RentalService,private authService: AuthServiceService) {}

  ngOnInit() {
    this.loadRentalData();
  }

  
  loadRentalData() {
    const userIdString = localStorage.getItem('userId'); // Get user ID from local storage
    console.log('User ID from Local Storage:', userIdString); // Log user ID for debugging
  
    // Check if userIdString is not null and convert to number if necessary
    const userId = userIdString ? Number(userIdString) : null;
  
    if (userId !== null) {
      // Fetch pending rental requests
      this.rentalService.getPendingRentals().subscribe(
        pending => {
          this.pendingRequests = pending; // Set pending requests
          console.log('Pending Requests:', this.pendingRequests); // Log for debugging
        },
        error => {
          console.error('Error fetching pending rentals:', error); // Log errors
        }
      );
  
      // Fetch rental history using userId
      this.rentalService.getRentalHistory(userId).subscribe(
        history => {
          // Filter history into returned and rented cars
          this.returnHistory = history.filter(r => r.status === RentalStatus.Returned);
          this.rentedCars = history.filter(r => r.status === RentalStatus.Rented || r.status === RentalStatus.Accepted);
          
          console.log('Rental History:', history); // Log full rental history for debugging
          console.log('Rented Cars:', this.rentedCars); // Log rented cars
          console.log('Return History:', this.returnHistory); // Log return history
        },
        error => {
          console.error('Error fetching rental history:', error); // Log errors
        }
      );
    } else {
      console.error('No user ID found in local storage. User may not be logged in.'); // Log if user ID is not found
    }
  }

  goBack(): void {
    this.router.navigate(['/userpage']);
  }

}