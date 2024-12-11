import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AuthServiceService, LoginData, RegisterData } from '../../Services/auth-service.service';
import { Router } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';


@Component({
  selector: 'app-login',
  imports: [CommonModule,FormsModule,HttpClientModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  isRegistering = false;

  // Login fields
  loginData: LoginData = { nic: '', password: '' };

  // Registration fields
  registerData: RegisterData = {
    firstName: '',
    lastName: '',
    email: '',
    phoneNumber: '',
    driverLicenseNumber: '',
    nic: '',
    password: ''
  };

  constructor(private authService:AuthServiceService, private router: Router) {}

  toggleForm(): void {
    this.isRegistering = !this.isRegistering;
  }

  onLogin(): void {
    this.authService.login(this.loginData).subscribe(() => {
      this.router.navigate(['/userpage']);
    }, error => {
      console.error('Login failed', error);
    });
  }

  onRegister(): void {
    this.authService.register(this.registerData).subscribe(() => {
      localStorage.setItem('user', JSON.stringify(this.registerData));
      this.toggleForm(); // Switch back to login form
    }, error => {
      console.error('Registration failed', error);
    });
  }

}
