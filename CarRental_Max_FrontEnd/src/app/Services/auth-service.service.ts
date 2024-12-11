import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthServiceService {
  
  private apiUrl = 'https://localhost:7038/api'; 

  constructor(private http: HttpClient) {}

  login(credentials: LoginData): Observable<any> {
    return this.http.post(`${this.apiUrl}/Customer/login`, credentials);
  }

  register(data: RegisterData): Observable<any> {
    return this.http.post(`${this.apiUrl}/Customer/register`, data);
  }
}

export interface RegisterData {
  firstName: string;
  lastName: string;
  email: string;
  phoneNumber: string;
  driverLicenseNumber: string;
  nic: string;
  password: string;
}

export interface LoginData {
  nic: string;
  password: string;
}