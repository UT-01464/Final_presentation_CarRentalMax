import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {


  private apiUrl = 'https://localhost:7038/api/Customer'; // Replace with your actual API URL

  constructor(private http: HttpClient) {}

  // Register a new customer
  register(registerDto: RegisterDto): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/register`, registerDto);
  }

  // Login a customer
  login(loginDto: LoginDto): Observable<{ token: string }> {
    return this.http.post<{ token: string }>(`${this.apiUrl}/login`, loginDto);
  }

  // Get all customers
  getAllCustomers(): Observable<Customer[]> {
    return this.http.get<Customer[]>(`${this.apiUrl}/GetAllCustomers`);
  }

  // Get customer by NIC
  getCustomerByNic(nic: string): Observable<Customer> {
    return this.http.get<Customer>(`${this.apiUrl}/GetCustomerByNic/${nic}`);
  }

  // Get customer by ID
  getCustomerById(id: number): Observable<Customer> {
    return this.http.get<Customer>(`${this.apiUrl}/GetCustomerById/${id}`);
  }

  // Update customer
  updateCustomer(id: number, customer: Customer): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/UpdateCustomer/${id}`, customer);
  }

  // Delete customer
  deleteCustomer(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/DeleteCustomer/${id}`);
  }




  
}


export interface Customer {
  id: number;
  firstName: string;
  lastName: string;
  email: string;
  phoneNumber: string;
  driverLicenseNumber: string;
  nic: string;
  roleId: number;
  address: Address; // Assuming you have an Address model
}

export interface Address {
  id: number;
  street: string;
  city: string;
  state: string;
  zipCode: string;
  country: string;
}

export interface RegisterDto {
  firstName: string;
  lastName: string;
  email: string;
  phoneNumber: string;
  driverLicenseNumber: string;
  nic: string; // National Identity Card
  password: string; // Include password for registration
}


export interface LoginDto {
  nic: string; // National Identity Card
  password: string;
}

