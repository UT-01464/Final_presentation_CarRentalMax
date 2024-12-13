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
    this.authService.login(this.loginData).subscribe((response) => {
        const token = response.token; // Assuming the response contains the JWT token
        localStorage.setItem('token', token); // Store the token in local storage

        // Decode the token to extract user ID
        const userDetails = this.parseJwt(token);
        localStorage.setItem('userId', userDetails.nameid); // Store the user ID (nameid claim)
        this.authService.saveCurrentUser(response);

        this.router.navigate(['/userpage']); // Navigate to user page
    }, error => {
        console.error('Login failed', error);
        alert('Login failed. Please check your credentials and try again.');
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
        alert('Registration successful! You can now log in.'); // Provide feedback
        this.toggleForm(); // Optionally switch back to the login form
    }, error => {
        console.error('Registration failed', error);
        alert('Registration failed. Please try again.');
    });
}

}
