import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CarService {

  private baseUrl = 'https://localhost:7038/api/Car'; // Base URL for the API

  constructor(private http: HttpClient) {}

  // Get all cars
  getCars(): Observable<CarDto[]> {
    return this.http.get<CarDto[]>(`${this.baseUrl}/GetCars`);
  }

  // Get a specific car by ID
  getCar(id: number): Observable<CarDto> {
    return this.http.get<CarDto>(`${this.baseUrl}/GetCar/${id}`);
  }

  // Add a new car
  addCar(car: CarDto): Observable<CarDto> {
    return this.http.post<CarDto>(`${this.baseUrl}/AddCar`, car);
  }

  // Update an existing car
  updateCar(id: number, car: CarDto): Observable<CarDto> {
    return this.http.put<CarDto>(`${this.baseUrl}/UpdateCar/${id}`, car);
  }

  // Delete a car by ID
  deleteCar(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/DeleteCar/${id}`);
  }

  // Get car models
  getCarModels(): Observable<CarModel[]> {
    return this.http.get<CarModel[]>(`${this.baseUrl}/GetCarModels`);
  }

  // Get a specific car model by ID
  getCarModel(id: number): Observable<CarModel> {
    return this.http.get<CarModel>(`${this.baseUrl}/GetCarModel/${id}`);
  }

  // Add a new car model
  addCarModel(carModel: CarModel): Observable<CarModel> {
    return this.http.post<CarModel>(`${this.baseUrl}/AddCarModel`, carModel);
  }

  // Update an existing car model
  updateCarModel(id: number, carModel: CarModel): Observable<CarModel> {
    return this.http.put<CarModel>(`${this.baseUrl}/UpdateCarModel/${id}`, carModel);
  }

  // Delete a car model by ID
  deleteCarModel(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/DeleteCarModel/${id}`);
  }

  // Get categories
  getCategories(): Observable<Category[]> {
    return this.http.get<Category[]>(`${this.baseUrl}/GetCategories`);
  }

  // Get a specific category by ID
  getCategory(id: number): Observable<Category> {
    return this.http.get<Category>(`${this.baseUrl}/GetCategory/${id}`);
  }

  // Add a new category
  addCategory(category: Category): Observable<Category> {
    return this.http.post<Category>(`${this.baseUrl}/AddCategory`, category);
  }

  // Update an existing category
  updateCategory(id: number, category: Category): Observable<Category> {
    return this.http.put<Category>(`${this.baseUrl}/UpdateCategory/${id}`, category);
  }

  // Delete a category by ID
  deleteCategory(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/DeleteCategory/${id}`);
  }

  
}




export interface CarDto {
    id: number;
    modelId: number;
    year: number;
    registrationNumber: string;
    categoryId: number;
    pricePerDay: number;
    isAvailable: boolean;
    imageUrl: string;

}

export interface CarModel {
    id: number;
    name: string; 
    make: string; 
    categoryId: number; 
    category?: Category; 
    cars?: CarDto[]; 
  
}

export interface Category {
  id: number; 
  name: string;
  carModels?: CarModel[]; 
  cars?: CarDto[];
  
}