import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';

interface CustomerDto {
  id: number;
  nic: string;
  firstName: string;
  lastName: string;
  email: string;
  phoneNumber: string;
}

interface RentalDto {
  carRegistrationNumber: string;
  rentalDate: string;
  returnDate: string;
  status: string;
}















@Component({
  selector: 'app-customer-detail',
  imports: [FormsModule,CommonModule],
  templateUrl: './customer-detail.component.html',
  styleUrl: './customer-detail.component.css'
})
export class CustomerDetailComponent {

  customers: CustomerDto[] = [
    { id: 1, nic: '123456789V', firstName: 'John', lastName: 'Doe', email: 'john.doe@example.com', phoneNumber: '(123) 456-7890' },
    { id: 2, nic: '987654321V', firstName: 'Jane', lastName: 'Smith', email: 'jane.smith@example.com', phoneNumber: '(987) 654-3210' },
  ];
  
  selectedCustomer: CustomerDto | null = null;
  rentalHistory: RentalDto[] = [];
  showRentalModal: boolean = false;

  onEdit(customer: CustomerDto): void {
    this.selectedCustomer = { ...customer }; // Create a copy for editing
  }

  onViewRentals(customerId: number): void {
    // Hard-coded rental history
    this.rentalHistory = [
      { carRegistrationNumber: 'ABC1234', rentalDate: '2024-12-01', returnDate: '2024-12-10', status: 'Returned' },
      { carRegistrationNumber: 'XYZ5678', rentalDate: '2024-11-15', returnDate: '2024-11-20', status: 'Completed' },
    ];
    this.showRentalModal = true; // Open modal
  }

  onSave(): void {
    if (this.selectedCustomer) {
      // Normally, here you would save to the backend, but for now, just close the modal
      this.selectedCustomer = null; // Close edit form
    }
  }

  onCloseModal(): void {
    this.showRentalModal = false; // Close rental history modal
  }

}
