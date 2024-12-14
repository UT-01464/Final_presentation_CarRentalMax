import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Customer, CustomerService } from '../Services/Customer/customer.service';
import { Rental, RentalResponse, RentalService } from '../Services/Rental/rental.service';
import { CarDto, CarService } from '../Services/Cars/car.service';


@Component({
  selector: 'app-report',
  imports: [CommonModule,FormsModule],
  templateUrl: './report.component.html',
  styleUrl: './report.component.css'
})
export class ReportComponent implements OnInit {
  totalRentals: number = 0;
  totalCustomers: number = 0;
  totalCars: number = 0;
  carHistories: CarDto[] = []; // Use CarDto directly
  customerHistories: Customer[] = []; // Use existing Customer interface
  rentalHistories: RentalResponse[] = []; // Use Rental interface directly

  searchCarHistory: string = '';
  searchCustomerHistory: string = '';
  searchRentalHistory: string = ''; // Search for rental history

  currentCarPage: number = 1;
  currentCustomerPage: number = 1;
  currentRentalPage: number = 1; // Current page for rental history
  itemsPerPage: number = 5;

  constructor(
    private carService: CarService,
    private customerService: CustomerService,
    private rentalService: RentalService
  ) {}

  ngOnInit() {
    this.loadData();
    console.log('Initial state:', {
        carHistories: this.carHistories,
        customerHistories: this.customerHistories,
        rentalHistories: this.rentalHistories
    });
}

  loadData() {
    console.log('Loading data...'); // Log when data loading begins

    // Load car data
    this.carService.getCars().subscribe(cars => {
        console.log('Cars fetched:', cars); // Log the fetched car data
        this.totalCars = cars.length;
        this.carHistories = cars || []; // Ensure it's an array
    }, error => {
        console.error('Error fetching cars:', error); // Log any errors
    });

    // Load customer data
    this.customerService.getAllCustomers().subscribe(customers => {
        console.log('Customers fetched:', customers); // Log the fetched customer data
        this.totalCustomers = customers.length;
        this.customerHistories = customers || []; // Ensure it's an array
    }, error => {
        console.error('Error fetching customers:', error); // Log any errors
    });

    // Load rental data
    this.rentalService.getRentals().subscribe(rentals => {
        console.log('Rentals fetched:', rentals); // Log the fetched rental data
        this.totalRentals = rentals.length;
        this.rentalHistories = rentals || []; // Ensure it's an array
        
    }, error => {
        console.error('Error fetching rentals:', error); // Log any errors
    });
}

  get paginatedCarHistories() {
    return this.carHistories.filter(car => car.registrationNumber.includes(this.searchCarHistory))
      .slice((this.currentCarPage - 1) * this.itemsPerPage, this.currentCarPage * this.itemsPerPage);
  }

  get paginatedCustomerHistories() {
    return this.customerHistories.filter(customer => 
      `${customer.firstName} ${customer.lastName}`.includes(this.searchCustomerHistory))
      .slice((this.currentCustomerPage - 1) * this.itemsPerPage, this.currentCustomerPage * this.itemsPerPage);
  }

  get paginatedRentalHistories() {
    console.log('Search term:', this.searchRentalHistory);
    console.log('Rental histories:', this.rentalHistories);
    console.log('Current Page:', this.currentRentalPage);
    console.log('Items per page:', this.itemsPerPage);
    console.log();

    
    return this.rentalHistories.filter(rental => 
      rental.car.registrationNumber.includes(this.searchRentalHistory) || 
      `${rental.customerName}`.includes(this.searchRentalHistory)
    ).slice(
      (this.currentRentalPage - 1) * this.itemsPerPage,
      this.currentRentalPage * this.itemsPerPage
    );
  }
  
  nextCarPage() {
    if (this.currentCarPage * this.itemsPerPage < this.carHistories.length) {
      this.currentCarPage++;
    }
  }

  previousCarPage() {
    if (this.currentCarPage > 1) {
      this.currentCarPage--;
    }
  }

  nextCustomerPage() {
    if (this.currentCustomerPage * this.itemsPerPage < this.customerHistories.length) {
      this.currentCustomerPage++;
    }
  }

  previousCustomerPage() {
    if (this.currentCustomerPage > 1) {
      this.currentCustomerPage--;
    }
  }

  nextRentalPage() {
    if (this.currentRentalPage * this.itemsPerPage < this.rentalHistories.length) {
      this.currentRentalPage++;
    }
  }

  previousRentalPage() {
    if (this.currentRentalPage > 1) {
      this.currentRentalPage--;
    }
  }

  printHistory(filterBy: 'month' | 'week') {
    console.log(`Printing history filtered by: ${filterBy}`);
  }
}
