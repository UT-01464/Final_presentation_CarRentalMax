import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RentalDetailsComponent } from '../Rentals/rental-details/rental-details.component';

@Component({
  selector: 'app-userpage',
  imports: [CommonModule, FormsModule, RentalDetailsComponent],
  templateUrl: './userpage.component.html',
  styleUrls: ['./userpage.component.css']
})
export class UserpageComponent {
  userName: string = 'John Doe'; // Replace with actual user data
  searchQuery: string = '';
  showRentalDetails: boolean = false; // Control visibility of rental details

  // Sample data for cars
  cars = [
    { model: 'Toyota Camry', available: true },
    { model: 'Honda Accord', available: true },
    { model: 'Ford Mustang', available: false },
    { model: 'Chevrolet Impala', available: true }
  ];

  filteredCars = this.cars;

  // Method to filter cars based on the search query
  filterCars() {
    this.filteredCars = this.cars.filter(car => car.model.toLowerCase().includes(this.searchQuery.toLowerCase()));
  }

  // Method to log out the user
  logout() {
    // Implement logout logic
  }

  // Methods for navigation
  editProfile() {
    // Implement profile editing logic
  }

  viewRentals() {
    this.showRentalDetails = true; // Show rental details when clicked
  }

  viewCarDetails(car: any) {
    // Logic to show details of the selected car
    // You may want to implement a modal or another component for this
  }

  toggleMenu() {
    const menu = document.getElementById('offCanvasMenu');
    if (menu) {
      menu.style.display = menu.style.display === 'block' ? 'none' : 'block';
    }
  }
}