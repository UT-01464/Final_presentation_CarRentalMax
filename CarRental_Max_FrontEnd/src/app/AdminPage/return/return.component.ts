import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Rental, RentalResponse, RentalService, RentalStatus } from '../Services/Rental/rental.service';
import { CarModel, CarService } from '../Services/Cars/car.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-return',
  imports: [CommonModule,FormsModule],
  templateUrl: './return.component.html',
  styleUrl: './return.component.css'
})
export class ReturnComponent implements OnInit {
  rentalReturn: RentalResponse[] = [];
  carModels: CarModel[] = []; 
  searchNIC: string = ''; 
  filteredRentals: RentalResponse[] = [];


  constructor(private rentalService: RentalService, private carService:CarService) {

  }

  ngOnInit(): void {
    this.getallrentals();
    this.loadCarModels();
  }


  getallrentals() {
    this.rentalService.getRentals().subscribe((data) => {
      
      // Filter rentals where status is 'Returned'
      this.rentalReturn = data.filter((rental: RentalResponse) => rental.status === RentalStatus.Accepted);
      this.filteredRentals = this.rentalReturn; 
      console.log(this.rentalReturn);
      
    });
  }

  Return(id:number) {
    this.rentalService.returnCar(id).subscribe((data) => {
      alert("successfully deleted")
    this.getallrentals()
    });
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

  
  getCarModel(id: number): string {
    const carModel = this.carModels.find(m => m.id === id);
    return carModel ? carModel.name : 'Unknown'; // Return model name or 'Unknown' if not found
  }

  // Method to filter rentals based on NIC
  filterRentals(): void {
    if (this.searchNIC) {
      this.filteredRentals = this.rentalReturn.filter(rental =>
        rental.customerNIC.toLowerCase().includes(this.searchNIC.toLowerCase())
      );
    } else {
      this.filteredRentals = this.rentalReturn; // Reset to all rentals if search is empty
    }
  }


}
