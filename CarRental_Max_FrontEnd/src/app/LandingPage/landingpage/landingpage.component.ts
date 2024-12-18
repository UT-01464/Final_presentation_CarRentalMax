import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { CardetailsComponent } from '../CarDetails/cardetails/cardetails.component';
import { CarService, CarDto, CarModel } from '../../AdminPage/Services/Cars/car.service';
import { CarDetailsServiceService, FuelType, Transmission } from '../../AdminPage/Services/CarDetaile/car-details-service.service';
import { FeedbackService } from '../../Services/Feedback/feedback.service';

@Component({
  selector: 'app-landingpage',
  imports: [CommonModule,FormsModule,CardetailsComponent],
  templateUrl: './landingpage.component.html',
  styleUrl: './landingpage.component.css'
})
export class LandingpageComponent implements OnInit {

  isExpanded: boolean = false;

  toggleReadMore(): void {
    this.isExpanded = !this.isExpanded;
  }

  constructor(private router: Router, private carService: CarService,private carDetailsService: CarDetailsServiceService,
    private feedbackService: FeedbackService 
  ) {}

  getstart() {
    this.router.navigate(['/login']);
  }

  cars: CarDto[] = [];
  filterText: string = '';
  filteredCars: CarDto[] = [];
  selectedCar: CarDto | null = null;
  

  visibleCars: any[] = []; // Cars to display (up to 6)

  fuelTypes: FuelType[] = [];
  transmissions: Transmission[] = [];
  carModels: CarModel[] = []; 


  contact = {
    name: '',
    email: '',
    message: ''
  };

  successMessage: string | null = null; 
  

  ngOnInit(): void {
    this.filterCars();
    this.loadCars();
    this.loadFuelTypes();
    this.loadTransmissions();
    this.loadCarModels();



  }




  loadCars(): void {
    this.carService.getCars().subscribe(
      (data: CarDto[]) => {
        this.cars = data;
        this.filteredCars = data; 
        this.setVisibleCars(); 
      },
      (error) => {
        console.error('Error fetching cars:', error);
      }
    );
  }


  loadFuelTypes(): void {
    this.carDetailsService.getFuelTypes().subscribe(
      (data: FuelType[]) => {
        this.fuelTypes = data;
      },
      (error) => {
        console.error('Error fetching fuel types:', error);
      }
    );
  }

  loadTransmissions(): void {
    this.carDetailsService.getTransmissions().subscribe(
      (data: Transmission[]) => {
        this.transmissions = data;
      },
      (error) => {
        console.error('Error fetching transmissions:', error);
      }
    );
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
    return carModel ? carModel.name : 'Unknown'; 
  }


  getFuelType(id: number): string {
    const fuelType = this.fuelTypes.find(f => f.id === id);
    return fuelType ? fuelType.type : 'Unknown';
  }

  getTransmissionType(id: number): string {
    const transmission = this.transmissions.find(t => t.id === id);
    return transmission ? transmission.type : 'Unknown';
  }


  filterCars(): void {
    this.filteredCars = this.cars.filter((car) => {
      const modelName = this.getCarModel(car.modelId).toLowerCase();
      return modelName.includes(this.filterText.toLowerCase()); 
    });
    this.setVisibleCars(); 
  }

  openCarDetails(car: CarDto): void {
    this.selectedCar = car; 
  }

  closeCarDetails(): void {
    this.selectedCar = null; 
  }

  viewAllCars(): void {
    this.router.navigate(['/all-cars']);
  }

  setVisibleCars(): void {
    this.visibleCars = this.filteredCars.slice(0, 6); 
  }


  submitContactForm(): void {
    this.feedbackService.submitFeedback(this.contact.email, this.contact.message).subscribe(
      () => {
        this.successMessage = 'Your message has been sent successfully!';
        this.contact = { name: '', email: '', message: '' }; 
      },
      (error) => {
        console.error('Error sending feedback:', error);
        this.successMessage = 'There was an error sending your message. Please try again later.';
      }
    );
  }








}
