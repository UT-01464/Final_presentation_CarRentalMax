import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Rental, RentalResponse, RentalService, RentalStatus } from '../Services/Rental/rental.service';

@Component({
  selector: 'app-return',
  imports: [CommonModule],
  templateUrl: './return.component.html',
  styleUrl: './return.component.css'
})
export class ReturnComponent implements OnInit {
  rentalReturn: RentalResponse[] = [];


  constructor(private rentalService: RentalService) {

  }

  ngOnInit(): void {
    this.getallrentals() 
  }


  getallrentals() {
    this.rentalService.getRentals().subscribe((data) => {
      // Filter rentals where status is 'Returned'
      this.rentalReturn = data.filter((rental: RentalResponse) => rental.status === RentalStatus.Accepted);
      console.log(this.rentalReturn);
      
    });
  }

  Return(id:number) {
    this.rentalService.returnCar(id).subscribe((data) => {
      alert("successfully deleted")
    this.getallrentals()
    });
  }

}
