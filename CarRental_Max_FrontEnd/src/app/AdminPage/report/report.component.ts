import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';

interface CarHistory {
  registrationNumber: string;
  model: string;
  rentalDate: string;
  returnDate: string;
}

interface CustomerHistory {
  name: string;
  registrationNumber: string;
  rentalDate: string;
}

interface ManageCarHistory {
  registrationNumber: string;
  action: string;
  date: string;
}












@Component({
  selector: 'app-report',
  imports: [CommonModule,FormsModule],
  templateUrl: './report.component.html',
  styleUrl: './report.component.css'
})
export class ReportComponent {

  totalRentals: number = 15; // Sample total rentals
  totalCustomers: number = 10; // Sample total customers
  totalCars: number = 20; // Sample total cars
  carHistories: CarHistory[] = [
    { registrationNumber: 'ABC1234', model: 'Toyota Camry', rentalDate: '2023-10-01', returnDate: '2023-10-05' },
    { registrationNumber: 'XYZ5678', model: 'Honda Accord', rentalDate: '2023-10-02', returnDate: '2023-10-07' },
    { registrationNumber: 'LMN9101', model: 'Ford Focus', rentalDate: '2023-10-03', returnDate: '2023-10-08' },
    // Add more sample data as needed
  ];
  
  customerHistories: CustomerHistory[] = [
    { name: 'John Doe', registrationNumber: 'ABC1234', rentalDate: '2023-10-01' },
    { name: 'Jane Smith', registrationNumber: 'XYZ5678', rentalDate: '2023-10-02' },
    // Add more sample data as needed
  ];
  
  manageCarHistories: ManageCarHistory[] = [
    { registrationNumber: 'ABC1234', action: 'Rented', date: '2023-10-01' },
    { registrationNumber: 'XYZ5678', action: 'Returned', date: '2023-10-02' },
    // Add more sample data as needed
  ];

  searchCarHistory: string = '';
  searchCustomerHistory: string = '';
  searchManageCarHistory: string = '';

  currentCarPage: number = 1;
  currentCustomerPage: number = 1;
  currentManageCarPage: number = 1;
  itemsPerPage: number = 5;

  get paginatedCarHistories() {
    return this.carHistories.filter(car => car.registrationNumber.includes(this.searchCarHistory))
      .slice((this.currentCarPage - 1) * this.itemsPerPage, this.currentCarPage * this.itemsPerPage);
  }

  get paginatedCustomerHistories() {
    return this.customerHistories.filter(customer => customer.registrationNumber.includes(this.searchCustomerHistory))
      .slice((this.currentCustomerPage - 1) * this.itemsPerPage, this.currentCustomerPage * this.itemsPerPage);
  }

  get paginatedManageCarHistories() {
    return this.manageCarHistories.filter(history => history.registrationNumber.includes(this.searchManageCarHistory))
      .slice((this.currentManageCarPage - 1) * this.itemsPerPage, this.currentManageCarPage * this.itemsPerPage);
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

  nextManageCarPage() {
    if (this.currentManageCarPage * this.itemsPerPage < this.manageCarHistories.length) {
      this.currentManageCarPage++;
    }
  }

  previousManageCarPage() {
    if (this.currentManageCarPage > 1) {
      this.currentManageCarPage--;
    }
  }

  printHistory(filterBy: 'month' | 'week') {
    // Implement logic for printing based on filterBy
    console.log(`Printing history filtered by: ${filterBy}`);
  }

}
