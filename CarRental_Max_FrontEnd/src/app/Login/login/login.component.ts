import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AuthServiceService, LoginData, RegisterData } from '../../Services/auth-service.service';
import { Router } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';


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

  constructor(private authService:AuthServiceService, private router: Router , private toastr: ToastrService) {}

  toggleForm(): void {
    this.isRegistering = !this.isRegistering;
  }


  onLogin(): void {
    this.authService.login(this.loginData).subscribe((response) => {
        const token = response.token; 
        localStorage.setItem('token', token); 

        const userDetails = this.parseJwt(token);
        localStorage.setItem('userId', userDetails.nameid); 
        this.authService.saveCurrentUser(response);

        
        this.toastr.success('Login successful!', 'Success');

        this.router.navigate(['/userpage']); 
    }, error => {
        console.error('Login failed', error);

        
        this.toastr.error('Login failed. Please check your NIC or Password and try again.', 'Error');
    });
  }

// Function to parse JWT
parseJwt(token: string) {
    const base64Url = token.split('.')[1];
    const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    const jsonPayload = decodeURIComponent(atob(base64).split('').map(c => '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2)).join(''));
    return JSON.parse(jsonPayload);
}


onRegister(): void {
  this.authService.register(this.registerData).subscribe((response) => {
      // Store user data in local storage after successful registration
      localStorage.setItem('user', JSON.stringify(response));

      // Show success toastr message
      this.toastr.success('Registration successful! You can now log in.', 'Success');

      this.toggleForm(); // Optionally switch back to the login form
  }, error => {
      console.error('Registration failed', error);

      // Show error toastr message
      this.toastr.error('Registration failed. Please try again.', 'Error');
  });
}

}
