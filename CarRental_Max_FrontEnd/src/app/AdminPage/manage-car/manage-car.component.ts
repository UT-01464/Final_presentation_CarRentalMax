import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CarService } from '../Services/Cars/car.service';
import { CarDto,Category,CarModel } from '../Services/Cars/car.service';
import { CloudinaryService } from '../../Services/Cloudinary/cloudinary.service';

@Component({
  selector: 'app-manage-car',
  standalone: true,
  imports: [FormsModule, CommonModule, ReactiveFormsModule],
  templateUrl: './manage-car.component.html',
  styleUrls: ['./manage-car.component.css']
})
export class ManageCarComponent implements OnInit {
  cars: CarDto[] = [];

  imagePreview: string | ArrayBuffer | null = null;


  categories: Category[] = [];
  carModels: CarModel[] = [];


  newCar: CarDto = this.initializeNewCar();
  originalCar: CarDto | null = null;


  showAddCarModal: boolean = false;
  showStatsModal: boolean = false; // Added this line
  selectedStatus: string = ''; // To track the selected stats
  searchTerm: string = '';
  currentPage: number = 1;
  itemsPerPage: number = 5;

  constructor(private carService: CarService , private cloudinaryService:CloudinaryService) {}

  ngOnInit(): void {
    this.loadCars(); // Load cars on initialization
    this.loadCategories();
    this.loadCarModels();
  }


  onFileChange(event: Event): void {
    const file = (event.target as HTMLInputElement).files?.[0];
    if (file) {
      const reader = new FileReader();
      reader.onload = () => {
        this.imagePreview = reader.result as string; // Set the image preview
      };
      reader.readAsDataURL(file);

      this.cloudinaryService.uploadImage(file).subscribe({
        next: (response: any) => {
          this.newCar.imageUrl = response.secure_url; // Save the URL received from Cloudinary
          console.log('Image uploaded:', response.secure_url);
        },
        error: (err) => {
          console.error('Image upload failed:', err);
        },
      });
    } else if (this.originalCar) {
      // If no new file is selected, retain the old image URL
      this.newCar.imageUrl = this.originalCar.imageUrl;
      this.imagePreview = this.originalCar.imageUrl; // Show the old image in the preview
    }
  }


  loadCategories(): void {
    this.carService.getCategories().subscribe(data => {
      this.categories = data;
    });
  }

  loadCarModels(): void {
    this.carService.getCarModels().subscribe(data => {
      this.carModels = data;
    });
  }













  loadCars(): void {
    this.carService.getCars().subscribe(
      (data: CarDto[]) => {
        this.cars = data;
      },
      (error) => {
        console.error('Error fetching cars:', error);
      }
    );
  }

  onAddCar(): void {
    if (this.newCar.id) {
      // Prepare the updated car object
      const updatedCar: CarDto = {
        ...this.originalCar, // Start with the original details
        ...this.newCar // Overwrite with newCar details if they exist
      };
  
      this.carService.updateCar(this.newCar.id, updatedCar).subscribe(
        (response) => {
          // Check if response is valid
          if (response && response.id) {
            const index = this.cars.findIndex(car => car.id === response.id);
            if (index !== -1) {
              this.cars[index] = response; // Update the car in the array
            } else {
              console.error('Car to update not found in the local list.');
            }
          } else {
            console.error('Invalid response from updateCar:', response);
          }
          this.resetNewCar(); // Reset the newCar object
          this.originalCar = null; // Clear the original car details
          this.showAddCarModal = false; // Close the modal
        },
        (error) => {
          console.error('Error updating car:', error);
        }
      );
    } else {
      // Add new car
      this.carService.addCar(this.newCar).subscribe(
        (car) => {
          if (car && car.id) {
            this.cars.unshift(car); // Add the new car to the beginning of the list
            this.resetNewCar(); // Reset the newCar object
            this.showAddCarModal = false; // Close the modal
          } else {
            console.error('Added car response is invalid:', car);
          }
        },
        (error) => {
          console.error('Error adding car:', error);
        }
      );
    }
  }

  resetNewCar(): void {
    this.newCar = this.initializeNewCar();
    this.imagePreview = null; // Clear the image preview
  }

  initializeNewCar(): CarDto {
    return {
      id: 0,
      modelId: 0,
      year: new Date().getFullYear(),
      registrationNumber: '',
      categoryId: 0,
      pricePerDay: 0,
      isAvailable: true,
      imageUrl: ''
    };
  }



  onEditCar(car: CarDto): void {
    this.originalCar = { ...car }; // Keep a copy of the original car details
    this.newCar = { ...car }; // Populate newCar with the selected car's details
    this.imagePreview = car.imageUrl; // Set the preview to the existing image URL
    this.showAddCarModal = true; // Open the modal for editing
  }

  onDeleteCar(carId: number): void {
    this.carService.deleteCar(carId).subscribe(
      () => {
        this.cars = this.cars.filter(car => car.id !== carId);
      },
      (error) => {
        console.error('Error deleting car:', error);
      }
    );
  }

  onSearch(searchTerm: string, status?: string): CarDto[] {
    return this.cars.filter(car => 
      car.registrationNumber.includes(searchTerm) &&
      (status ? (status === 'available' ? car.isAvailable : !car.isAvailable) : true)
    );
  }

  onStatsChange(event: Event): void {
    const target = event.target as HTMLSelectElement;
    this.selectedStatus = target.value; // Set the selected status
    this.showStatsModal = true; // Open the stats modal
  }

  get paginatedCars(): CarDto[] {
    const filteredCars = this.onSearch(this.searchTerm);
    const startIndex = (this.currentPage - 1) * this.itemsPerPage;
    return filteredCars.slice(startIndex, startIndex + this.itemsPerPage);
  }

  nextPage(): void {
    if (this.currentPage * this.itemsPerPage < this.onSearch(this.searchTerm).length) {
      this.currentPage++;
    }
  }

  previousPage(): void {
    if (this.currentPage > 1) {
      this.currentPage--;
    }
  }

 
}