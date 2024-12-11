import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-all-cars',
  imports: [CommonModule,FormsModule],
  templateUrl: './all-cars.component.html',
  styleUrl: './all-cars.component.css'
})
export class AllCarsComponent implements OnInit{


  constructor(private router:Router){}

  
  cars = [
    {
      name: 'BMW',
      price: 'Starting from $80/Day',
      image: '/images/homecar.jpg',
      type: 'Sedan',
      seats: 4,
      doors: 4,
      ac: true,
    },
    {
      name: 'BMW',
      price: 'Starting from $80/Day',
      image: '/images/homecar.jpg',
      type: 'Sedan',
      seats: 4,
      doors: 4,
      ac: true,
    },
    {
      name: 'BMW',
      price: 'Starting from $80/Day',
      image: '/images/homecar.jpg',
      type: 'Sedan',
      seats: 4,
      doors: 4,
      ac: true,
    },
    {
      name: 'BMW',
      price: 'Starting from $80/Day',
      image: '/images/homecar.jpg',
      type: 'Sedan',
      seats: 4,
      doors: 4,
      ac: true,
    },
    {
      name: 'honda',
      price: 'Starting from $80/Day',
      image: '/images/homecar.jpg',
      type: 'Sedan',
      seats: 4,
      doors: 4,
      ac: true,
    },
    {
      name: 'ferrare',
      price: 'Starting from $80/Day',
      image: '/images/homecar.jpg',
      type: 'Sedan',
      seats: 4,
      doors: 4,
      ac: true,
    },
    {
      name: 'car',
      price: 'Starting from $80/Day',
      image: '/images/homecar.jpg',
      type: 'Sedan',
      seats: 4,
      doors: 4,
      ac: true,
    },
    // Add more cars here
  ];

  filterText: string = '';
  filteredCars = [...this.cars];

  ngOnInit(): void {
    this.filterCars();
  }

  filterCars(): void {
    this.filteredCars = this.cars.filter((car) =>
      car.name.toLowerCase().includes(this.filterText.toLowerCase())
    );
  }
 

}
