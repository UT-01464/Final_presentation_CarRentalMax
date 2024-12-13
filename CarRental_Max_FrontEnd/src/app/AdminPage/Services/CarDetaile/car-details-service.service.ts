import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CarDetailsServiceService {


  
  private baseUrl = 'https://localhost:7038/api'; // Base URL for the API

  constructor(private http: HttpClient) {}

  // Features API
  getFeatures(): Observable<Feature[]> {
    return this.http.get<Feature[]>(`${this.baseUrl}/Features`);
  }

  createFeature(feature: Feature): Observable<Feature> {
    return this.http.post<Feature>(`${this.baseUrl}/Features`, feature);
  }

  updateFeature(id: number, feature: Feature): Observable<Feature> {
    return this.http.put<Feature>(`${this.baseUrl}/Features/${id}`, feature);
  }

  deleteFeature(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/Features/${id}`);
  }

  // FuelTypes API
  getFuelTypes(): Observable<FuelType[]> {
    return this.http.get<FuelType[]>(`${this.baseUrl}/FuelTypes`);
  }

  createFuelType(fuelType: FuelType): Observable<FuelType> {
    return this.http.post<FuelType>(`${this.baseUrl}/FuelTypes`, fuelType);
  }

  updateFuelType(id: number, fuelType: FuelType): Observable<FuelType> {
    return this.http.put<FuelType>(`${this.baseUrl}/FuelTypes/${id}`, fuelType);
  }

  deleteFuelType(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/FuelTypes/${id}`);
  }

  // Transmissions API
  getTransmissions(): Observable<Transmission[]> {
    return this.http.get<Transmission[]>(`${this.baseUrl}/Transmissions`);
  }

  createTransmission(transmission: Transmission): Observable<Transmission> {
    return this.http.post<Transmission>(`${this.baseUrl}/Transmissions`, transmission);
  }

  updateTransmission(id: number, transmission: Transmission): Observable<Transmission> {
    return this.http.put<Transmission>(`${this.baseUrl}/Transmissions/${id}`, transmission);
  }

  deleteTransmission(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/Transmissions/${id}`);
  }
}

export interface Feature {
  id: number;
  name: string;
}

export interface FuelType {
  id: number;
  type: string;
}

export interface Transmission {
  id: number;
  type: string;
}
