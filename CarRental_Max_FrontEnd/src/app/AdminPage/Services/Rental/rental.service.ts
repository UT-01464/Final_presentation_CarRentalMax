import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CarDto } from '../Cars/car.service';
import { Customer } from '../Customer/customer.service';

@Injectable({
  providedIn: 'root'
})
export class RentalService {


  private apiUrl = 'https://localhost:7038/api/Rentals'; // Base URL for the rental API

  constructor(private http: HttpClient,
    
  ) {}

  // Get all rentals
  getRentals(): Observable<RentalResponse[]> {
    return this.http.get<RentalResponse[]>(this.apiUrl);
  }

  // Get rental by ID
  getRentalById(rentalId: number): Observable<Rental> {
    return this.http.get<Rental>(`${this.apiUrl}/${rentalId}`);
  }

  // Get rental details by NIC
  getRentalDetails(nic: string): Observable<Rental[]> {
    return this.http.get<Rental[]>(`${this.apiUrl}/details/${nic}`);
  }

  //rentCar(rentalRequest: { customerId: number; carId: number }): Observable<RentalResponse> {
    //return this.http.post<RentalResponse>(`${this.apiUrl}/rent`, rentalRequest,{
      //responseType:'text'
   // });
//}



rentCar(rentalRequest:rentalRequest) {
  return this.http.post<RentalResponse>(this.apiUrl + "/rent", rentalRequest)
}


returnCar(id:number) {
  return this.http.put("https://localhost:7038/api/Rentals/return?id="+id, id)
}
  // Return a rented car
  // Accept a rental
  acceptRental(rentalId: number): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/accept/${rentalId}`, {});
  }

  // Reject a rental
  rejectRental(rentalId: number): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/reject/${rentalId}`, {});
  }

  // Get rental history by customer ID
  getRentalHistory(customerId: number): Observable<Rental[]> {
    return this.http.get<Rental[]>(`${this.apiUrl}/history/${customerId}`);
  }

  // Extend a rental
  extendRental(rentalId: number): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/extend`, { rentalId });
  }

  // Update overdue fees
  updateOverdueFees(): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/update-overdue-fee`, {});
  }

  // Get overdue rentals
  getOverdueRentals(): Observable<Rental[]> {
    return this.http.get<Rental[]>(`${this.apiUrl}/overdue`);
  }


  // Get pending rentals
  getPendingRentals(): Observable<RentalResponse[]> {
    return this.http.get<RentalResponse[]>(`${this.apiUrl}/pending`); // Call the pending rentals API
  }



  // Get count of pending rentals
  getPendingCount(): Observable<number> {
    return this.http.get<number>(`${this.apiUrl}/pending/count`);
  }

  // Get count of accepted rentals
  getAcceptedCount(): Observable<number> {
    return this.http.get<number>(`${this.apiUrl}/accepted/count`);
  }

  // Get count of rejected rentals
  getRejectedCount(): Observable<number> {
    return this.http.get<number>(`${this.apiUrl}/rejected/count`);
  }

 
}



export interface Rental {
  id: number;
  car: CarDto; // Including the car information
  customer: Customer; // Include customer details
  rentalDate: Date;
  status: RentalStatus;
  returnDate?: Date;
  pricePerDay: number;
  overdueFees: number;
  isOverdue: boolean; // Indicate if the rental is overdue
  userId: string;
}


export enum RentalStatus {
  Pending = 0,
  Accepted = 1,
  Rejected = 2,
  Rented = 3,
  Returned = 4
}

export interface RentalResponse {
  id:number,
  car:CarDto,
  customerName:string,
  customerNIC:string,
  status:RentalStatus,
  rentalDate:string,
  returnDate:string
}


export interface rentalRequest{
  customerId:number;
  carId:number;
  rentalDays:number

}