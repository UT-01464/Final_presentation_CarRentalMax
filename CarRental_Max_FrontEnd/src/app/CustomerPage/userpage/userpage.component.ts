import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CardetailsComponent } from '../../LandingPage/CarDetails/cardetails/cardetails.component';
import { Router } from '@angular/router';
import { Customer, CustomerService } from '../../AdminPage/Services/Customer/customer.service';
import { AuthServiceService } from '../../Services/auth-service.service';

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
  isProfileModalOpen: boolean = false; 

  isModalOpen: boolean = false;
  customer: Customer = {
    id: 0, // Set this to the actual ID of the customer
    firstName: '',
    lastName: '',
    email: '',
    phoneNumber: '',
    driverLicenseNumber: '',
    nic: '',
    roleId: 0,
    address: {
      id:0,
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

  constructor(private router: Router , private customerService:CustomerService,private authService:AuthServiceService ) {}

  ngOnInit(): void {
    this.filteredCars = this.cars; // Initialize filteredCars with all cars
    this.loadUserData();
  }




  loadUserData(): void {
    const userIdString = localStorage.getItem('userId'); // Retrieve user ID from local storage
    if (userIdString) {
      const userId = Number(userIdString); // Convert to number
      this.customerService.getCustomerById(userId).subscribe((data) => {
        this.customer = data; // Set the retrieved customer data
      }, error => {
        console.error('Error fetching user data', error);
      });
    }
  }


  saveProfile(): void {
    const customerId = this.customer.id; // Get the customer ID from the customer object
    this.customerService.updateCustomer(customerId, this.customer).subscribe(() => {
      // Update local storage with the new customer data
      localStorage.setItem('user', JSON.stringify(this.customer));
      alert('Profile updated successfully!'); // Notify user of success
    }, error => {
      console.error('Failed to update profile', error);
      alert('Failed to update profile. Please try again.'); // Notify user of error
    });
  }








  openProfileModal(): void {
    this.isProfileModalOpen = !this.isProfileModalOpen; // Toggle modal state
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
  

logout(): void {
    console.log('Logging out');
    // Implement logout logic
}

  closeProfileModal(): void {
    this.isModalOpen = false;
  }

  
  viewRentals(): void {
    this.router.navigate(['/rental-details'])
  }


  closeModal(): void {
    // Logic to close the modal, e.g., navigate away or hide it
    // For example, if using a modal library, you might call a function to close it
    window.history.back(); // Navigate back to the previous page
  }

  
}