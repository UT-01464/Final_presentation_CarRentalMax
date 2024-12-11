import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { DashboardComponent } from '../dashboard/dashboard.component';
import { CustomerDetailComponent } from '../customer-detail/customer-detail.component';
import { ManageCarComponent } from '../manage-car/manage-car.component';
import { ReportComponent } from '../report/report.component';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-admin',
  imports: [FormsModule,CommonModule,RouterModule
  ],
  templateUrl: './admin.component.html',
  styleUrl: './admin.component.css'
})
export class AdminComponent {

  isSidebarOpen: boolean = false;
}
