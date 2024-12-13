import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthServiceService {
  
  private apiUrl = 'https://localhost:7038/api'; 
  private currentUserKey = 'currentUser'; // Key for local storage

  constructor(private http: HttpClient) {}

  login(credentials: LoginData): Observable<any> {
    return this.http.post(`${this.apiUrl}/Customer/login`, credentials);
  }

  register(data: RegisterData): Observable<any> {
    return this.http.post(`${this.apiUrl}/Customer/register`, data);
  }

  logout(): void {
    localStorage.removeItem(this.currentUserKey); // Clear user info from local storage
  }


  getCurrentUser(): any {
    const user = localStorage.getItem(this.currentUserKey);
    return user ? JSON.parse(user) : null; // Retrieve user info
  }


  saveCurrentUser(user: any): void {
    // Ensure user contains id or nameid
    const userWithId = {
      ...user,
      id: user.id || user.nameid // Ensure id is present
    };
    localStorage.setItem(this.currentUserKey, JSON.stringify(userWithId)); // Store user info
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