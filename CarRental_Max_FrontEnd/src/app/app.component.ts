import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ToastrModule } from 'ngx-toastr'; // Import ToastrModule

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, ToastrModule], // Import ToastrModule
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'CarRental_Max_FrontEnd';

  constructor(private toastr: ToastrService) {}

}
