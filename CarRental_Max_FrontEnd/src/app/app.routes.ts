import { Routes } from '@angular/router';
import { LandingpageComponent } from './LandingPage/landingpage/landingpage.component';
import { AllCarsComponent } from './LandingPage/all-cars/all-cars.component';
import { LoginComponent } from './Login/login/login.component';
import { AdminComponent } from './AdminPage/admin/admin.component';
import { CustomerDetailComponent } from './AdminPage/customer-detail/customer-detail.component';
import { DashboardComponent } from './AdminPage/dashboard/dashboard.component';
import { ManageCarComponent } from './AdminPage/manage-car/manage-car.component';
import { ReportComponent } from './AdminPage/report/report.component';
import { UserpageComponent } from './CustomerPage/userpage/userpage.component';
import { RentalDetailsComponent } from './CustomerPage/Rentals/rental-details/rental-details.component';
import { CardetailsComponent } from './LandingPage/CarDetails/cardetails/cardetails.component';
import { ReturnComponent } from './AdminPage/return/return.component';
import { AuthGuardService } from './Services/auth-guard.service';


export const routes: Routes = [
    {path:'',component:LandingpageComponent},
    {path:'all-cars',component:AllCarsComponent},
    {path:'login',component:LoginComponent},
    {path:'userpage',component:UserpageComponent, canActivate: [AuthGuardService] },
    {path:'rental-details',component:RentalDetailsComponent},
    {path:'cardetails',component:CardetailsComponent},
    

    {
        path: 'admin',
        component: AdminComponent,
        children: [
          { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
          { path: 'dashboard', component: DashboardComponent },
          { path: 'customers', component: CustomerDetailComponent },
          { path: 'cars', component: ManageCarComponent },
          { path: 'reports', component: ReportComponent },
          { path: 'return', component: ReturnComponent },
        ],
      },
      { path: '', redirectTo: 'admin', pathMatch: 'full' },
      { path: '**', redirectTo: 'admin' }, // Fallback route
];
