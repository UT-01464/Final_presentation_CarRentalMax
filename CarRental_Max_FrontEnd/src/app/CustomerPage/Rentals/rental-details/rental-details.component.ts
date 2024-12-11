import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-rental-details',
  imports: [CommonModule,FormsModule],
  templateUrl: './rental-details.component.html',
  styleUrl: './rental-details.component.css'
})
export class RentalDetailsComponent {

  pendingRequests = [
    { id: 1, model: 'Toyota Camry', requestDate: '2023-12-01', status: 'Pending' },
    { id: 2, model: 'Honda Accord', requestDate: '2023-12-02', status: 'Pending' }
  ];

  rentedCars = [
    { id: 1, model: 'Ford Mustang', rentalDate: '2023-11-20', returnDate: '2023-11-27' },
    { id: 2, model: 'Chevrolet Impala', rentalDate: '2023-11-22', returnDate: '2023-11-29' }
  ];

  returnedCars = [
    { id: 1, model: 'Toyota Camry', rentalDate: '2023-11-10', returnDate: '2023-11-15' }
  ];

}
