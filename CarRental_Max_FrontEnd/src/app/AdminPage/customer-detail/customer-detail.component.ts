import { CommonModule } from '@angular/common';
import { Component,OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Customer, CustomerService, RegisterDto } from '../Services/Customer/customer.service';
import { Rental, RentalService, RentalStatus } from '../Services/Rental/rental.service';
import { CarModel, CarService } from '../Services/Cars/car.service';

@Component({
  selector: 'app-customer-detail',
  imports: [FormsModule,CommonModule],
  templateUrl: './customer-detail.component.html',
  styleUrl: './customer-detail.component.css'
})
export class CustomerDetailComponent implements OnInit {

  customers: Customer[] = [];
  selectedCustomer: Customer | null = null;
  rentalHistory: Rental[] = [];
  showRentalModal: boolean = false;
  loadingCustomers: boolean = false;
  message: string | null = null;
   carModels: CarModel[] = []; 

  newCustomer: RegisterDto = {
    firstName: '',
    lastName: '',
    email: '',
    phoneNumber: '',
    driverLicenseNumber: '',
    nic: '',
    password: '',
  };

  searchTerm: string = '';

  constructor(
    private customerService: CustomerService,
    private rentalService: RentalService,
    private carService : CarService
  ) {}

  ngOnInit(): void {
    this.loadCustomers();
  }

  loadCustomers(): void {
    this.loadingCustomers = true;
    this.customerService.getAllCustomers().subscribe({
      next: (data) => {
        this.customers = data;
        this.loadingCustomers = false;
      },
      error: (err) => {
        console.error('Error fetching customers:', err);
        this.loadingCustomers = false;
      },
    });
  }

  onEdit(customer: Customer): void {
    this.selectedCustomer = { ...customer }; // Create a copy for editing
  }

  

  onViewRentals(customerId: number): void {
    this.rentalService.getRentalHistory(customerId).subscribe({
      next: (data) => {
        console.log('Rental History Data:', data); // Log the response
        this.rentalHistory = Array.isArray(data) ? data : [];
        this.showRentalModal = true; // Open modal
      },
      error: (err) => {
        console.error('Error fetching rental history:', err);
        this.rentalHistory = []; // Clear rentals on error
        this.showRentalModal = true; // Show modal on error
      },
    });
  }



  onAddCustomer(): void {
    this.customerService.register(this.newCustomer).subscribe({
      next: () => {
        this.loadCustomers();
        this.resetNewCustomer(); // Reset form fields
      },
      error: (err) => {
        console.error('Error adding customer:', err);
      },
    });
  }

  resetNewCustomer(): void {
    this.newCustomer = {
      firstName: '',
      lastName: '',
      email: '',
      phoneNumber: '',
      driverLicenseNumber: '',
      nic: '',
      password: '',
    };
  }

  onDelete(customerId: number): void {
    if (confirm('Are you sure you want to delete this customer?')) {
      this.customerService.deleteCustomer(customerId).subscribe({
        next: () => this.loadCustomers(),
        error: (err) => console.error('Error deleting customer:', err),
      });
    }
  }

  onSave(): void {
    if (this.selectedCustomer) {
      const updatedCustomer = this.selectedCustomer;

      // Check if any details were updated, otherwise save old details
      this.customerService.updateCustomer(updatedCustomer.id, updatedCustomer).subscribe({
        next: () => {
          this.selectedCustomer = null;
          this.loadCustomers();
          this.message = 'Customer updated successfully!';
        },
        error: (err) => {
          console.error('Error updating customer:', err);
          this.message = 'Error updating customer.';
        },
      });
    }
  }

  onCloseModal(): void {
    this.showRentalModal = false;
  }

  get filteredCustomers(): Customer[] {
    const lowerCaseTerm = this.searchTerm.toLowerCase();
    return this.customers.filter(customer =>
      customer.firstName.toLowerCase().includes(lowerCaseTerm) ||
      customer.lastName.toLowerCase().includes(lowerCaseTerm)
    );
  }


  getCarModel(id: number): string {
    const carModel = this.carModels.find(m => m.id === id);
    return carModel ? carModel.name : 'Unknown'; 
  }

  getStatus(status: RentalStatus): string {
      switch (status) {
        case RentalStatus.Pending: return 'Pending';
        case RentalStatus.Accepted: return 'Accepted';
        case RentalStatus.Rejected: return 'Rejected';
        case RentalStatus.Rented: return 'Rented';
        case RentalStatus.Returned: return 'Returned';
        default: return 'Unknown';
      }
    }




}
