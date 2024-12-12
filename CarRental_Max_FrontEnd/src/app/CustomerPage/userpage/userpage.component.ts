import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CardetailsComponent } from '../../LandingPage/CarDetails/cardetails/cardetails.component';
import { Router } from '@angular/router';

// Define the Car interface
interface Car {
  name: string;
  price: string;
  image: string;
  type: string;
  seats: number;
  doors: number;
  ac: boolean;
}


interface Address {
  street: string;
  city: string;
  state: string;
  zipCode: string;
  country: string;
}

interface Customer {
  id: number;
  firstName: string;
  lastName: string;
  email: string;
  phoneNumber: string;
  nic: string;
  address: Address;
}





@Component({
  selector: 'app-userpage',
  imports: [CommonModule, FormsModule,CardetailsComponent],
  templateUrl: './userpage.component.html',
  styleUrls: ['./userpage.component.css']
})
export class UserpageComponent {
  filterText: string = '';
  filteredCars: Car[] = []; // Specify the type for filteredCars
  selectedCar: Car | null = null; // Specify type for selectedCar

  isModalOpen: boolean = false;
  customer: Customer = {
    id: 1,
    firstName: 'John',
    lastName: 'Doe',
    email: 'john.doe@example.com',
    phoneNumber: '+1-234-567-890',
    nic: '123456789V',
    address: {
      street: '',
      city: '',
      state: '',
      zipCode: '',
      country: ''
    }
  };


  cars: Car[] = [ // Specify the type for cars
    {
      name: 'BMW',
      price: 'Starting from $80/Day',
      image: '/images/homecar.jpg',
      type: 'Sedan',
      seats: 4,
      doors: 4,
      ac: true,
    },
    {
      name: 'Honda',
      price: 'Starting from $80/Day',
      image: '/images/homecar.jpg',
      type: 'Sedan',
      seats: 4,
      doors: 4,
      ac: true,
    },
    {
      name: 'Ferrari',
      price: 'Starting from $80/Day',
      image: '/images/homecar.jpg',
      type: 'Sedan',
      seats: 4,
      doors: 4,
      ac: true,
    },
    {
      name: 'Audi',
      price: 'Starting from $100/Day',
      image: '/images/homecar.jpg',
      type: 'SUV',
      seats: 5,
      doors: 5,
      ac: true,
    },
    // Add more cars here if needed
  ];

  constructor(private router: Router) {}

  ngOnInit(): void {
    this.filteredCars = this.cars; // Initialize filteredCars with all cars
  }

  filterCars(): void {
    this.filteredCars = this.cars.filter(car =>
      car.name.toLowerCase().includes(this.filterText.toLowerCase())
    );
  }

  viewAllCars(): void {
    this.router.navigate(['/all-cars']); // Navigate to the page that shows all cars
  }

  openCarDetails(car: Car): void {
    this.selectedCar = car; // Set the selected car
  }

  closeCarDetails(): void {
    this.selectedCar = null; // Clear the selected car to close the modal
  }
  openProfileModal(): void {
    console.log('Opening profile modal');
    this.isModalOpen = true;
}

logout(): void {
    console.log('Logging out');
    // Implement logout logic
}

  closeProfileModal(): void {
    this.isModalOpen = false;
  }

  saveProfile(): void {
    // Implement save logic here, e.g., send the updated customer data to a server
    console.log('Profile saved:', this.customer);
    this.closeProfileModal();
  }

  viewRentals(): void {
    this.router.navigate(['/rental-details'])
  }

  
}