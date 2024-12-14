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
  selectedModel: number | null = null;
  isRentalModalOpen: boolean = false;
  rentalDays: number = 0;
  selectedCarId: number | null = null;
   


  constructor(private router: Router , private customerService:CustomerService,private authService:AuthServiceService,
    private carService: CarService,private cardetailService:CarDetailsServiceService , private rentalService: RentalService
   ) {}




  ngOnInit(): void {
    this.filteredCars = this.cars; // Initialize filteredCars with all cars
    this.loadUserData();
   
    this.loadCars();
   
    this.loadFilters();

    this.loadFuelTypes();
    this.loadTransmissions();
    this.loadCarModels();
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

  loadFuelTypes(): void {
    this.cardetailService.getFuelTypes().subscribe(
      (data: FuelType[]) => {
        this.fuelTypes = data;
      },
      (error) => {
        console.error('Error fetching fuel types:', error);
      }
    );
  }

  loadTransmissions(): void {
    this.cardetailService.getTransmissions().subscribe(
      (data: Transmission[]) => {
        this.transmissions = data;
      },
      (error) => {
        console.error('Error fetching transmissions:', error);
      }
    );
  }

  loadCarModels(): void {
    this.carService.getCarModels().subscribe(
      (data: CarModel[]) => {
        this.carModels = data;
      },
      (error) => {
        console.error('Error fetching car models:', error);
      }
    );
  }

  getCarModel(id?: number): string {
    if (id === undefined) return 'Unknown'; // Handle undefined case
    const carModel = this.carModels.find(m => m.id === id);
    return carModel ? carModel.name : 'Unknown'; // Return model name or 'Unknown' if not found
}

getFuelType(id?: number): string {
    if (id === undefined) return 'Unknown'; // Handle undefined case
    const fuelType = this.fuelTypes.find(f => f.id === id);
    return fuelType ? fuelType.type : 'Unknown';
}

getTransmissionType(id?: number): string {
    if (id === undefined) return 'Unknown'; // Handle undefined case
    const transmission = this.transmissions.find(t => t.id === id);
    return transmission ? transmission.type : 'Unknown';
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
        console.log('Loaded Cars:', this.cars); // Log the loaded cars for verification
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
    console.log('Selected Filters:', {
        selectedTransmission: this.selectedTransmission,
        selectedFuelType: this.selectedFuelType,
        selectedModel: this.selectedModel
    });

    this.filteredCars = this.cars.filter(car => {
        const matchesTransmission = this.selectedTransmission ? car.transmissionId === this.selectedTransmission : true;
        const matchesFuelType = this.selectedFuelType ? car.fuelTypeId === this.selectedFuelType : true;
        const matchesModel = this.selectedModel ? car.modelId === this.selectedModel : true;

        console.log(`Car ID: ${car.id}, Matches:`, {
            matchesTransmission,
            matchesFuelType,
            matchesModel
        });

        return matchesTransmission && matchesFuelType && matchesModel;
    });

    console.log('Filtered Cars:', this.filteredCars); // Log filtered results
}

clearFilters(): void {
  this.selectedTransmission = null;
  this.selectedFuelType = null;
  this.selectedModel = null;
  this.filteredCars = this.cars; // Reset to show all cars
  console.log('Filters cleared. Showing all cars:', this.filteredCars);
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






// Method to open the rental modal
openRentalModal(carId: number): void {
  this.selectedCarId = carId; // Set the selected car ID
  this.rentalDays = 0; // Reset rental days
  this.isRentalModalOpen = true; // Open modal
}

// Method to close the rental modal
closeRentalModal(): void {
  this.isRentalModalOpen = false; // Close modal
}

// Method to confirm rental
confirmRental(): void {
  if (this.selectedCarId === null) {
      alert('No car selected.');
      return; // Exit if no car is selected
  }

  if (this.rentalDays <= 0) {
      alert('Please enter a valid number of rental days.');
      return; // Exit if rental days are not valid
  }

  const rentalRequest = {
      customerId: this.customer.id, // Ensure customer ID is available
      carId: this.selectedCarId, // Use selected car ID
      rentalDays: this.rentalDays // Use entered rental days
  };

  this.rentalService.rentCar(rentalRequest).subscribe(
      response => {
          console.log('Rental response:', response);
          this.closeRentalModal(); // Close modal after successful rental
      },
      error => {
          console.error('Error submitting rental request', error);
          alert('Failed to submit rental request. Please try again.');
      }
  );
}


}