import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CardetailsComponent } from '../../LandingPage/CarDetails/cardetails/cardetails.component';
import { Router } from '@angular/router';
import { Customer, CustomerService } from '../../AdminPage/Services/Customer/customer.service';
import { AuthServiceService } from '../../Services/auth-service.service';
import { CarDto, CarModel, CarService } from '../../AdminPage/Services/Cars/car.service';
import { CarDetailsServiceService, FuelType, Transmission } from '../../AdminPage/Services/CarDetaile/car-details-service.service';
import { RentalService } from '../../AdminPage/Services/Rental/rental.service';

@Component({
  selector: 'app-userpage',
  imports: [CommonModule, FormsModule,CardetailsComponent],
  templateUrl: './userpage.component.html',
  styleUrls: ['./userpage.component.css']
})
export class UserpageComponent {
  
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

cars: CarDto[] = [];
  filterText: string = '';
  filteredCars: CarDto[] = [];
  selectedCar: CarDto | null = null;
  visibleCars: any[] = []; 

  transmissions: Transmission[] = [];
  fuelTypes: FuelType[] = [];
  carModels: CarModel[] = [];
  
  selectedTransmission: number | null = null;
  selectedFuelType: number | null = null;
  selectedSeats: number | null = null;
  selectedModel: number | null = null;

  constructor(private router: Router , private customerService:CustomerService,private authService:AuthServiceService,
    private carService: CarService,private cardetailService:CarDetailsServiceService , private rentalService: RentalService
   ) {}




  ngOnInit(): void {
    this.filteredCars = this.cars; // Initialize filteredCars with all cars
    this.loadUserData();
    this.filterCars();
    this.loadCars();
   
    this.loadFilters();
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



  loadFilters(): void {
    this.cardetailService.getTransmissions().subscribe(transmissions => this.transmissions = transmissions);
    this.cardetailService.getFuelTypes().subscribe(fuelTypes => this.fuelTypes = fuelTypes);
    this.carService.getCarModels().subscribe(carModels => this.carModels = carModels);
  }

  filterCars(): void {
    this.filteredCars = this.cars.filter(car => {
      const matchesTransmission = this.selectedTransmission ? car.transmissionId === this.selectedTransmission : true;
      const matchesFuelType = this.selectedFuelType ? car.fuelTypeId === this.selectedFuelType : true;
      const matchesSeats = this.selectedSeats ? car.numberOfSeats === this.selectedSeats : true;
      const matchesModel = this.selectedModel ? car.modelId === this.selectedModel : true;

      return matchesTransmission && matchesFuelType && matchesSeats && matchesModel;
    });
  }

  clearFilters(): void {
    this.selectedTransmission = null;
    this.selectedFuelType = null;
    this.selectedSeats = null;
    this.selectedModel = null;
    this.filterCars(); // Reset to show all cars
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
    window.history.back();
  }




  rentCar(car: CarDto): void {
    const customerId = this.customer.id; // Get the logged-in customer ID
    const carId = car.id;

    const rentalRequest = {
        customerId: customerId,
        carId: carId
    };

    this.rentalService.rentCar(rentalRequest).subscribe(
        response => {
            console.log('Rental response:', response); // Log the response
            alert(response.message); // Now this should work without error
        },
        error => {
            console.error('Error submitting rental request', error);
            let errorMessage = error.error?.message || 'Failed to submit rental request. Please try again.';
            alert(errorMessage);
        }
    );
}
}