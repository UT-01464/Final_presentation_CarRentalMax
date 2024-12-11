import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CloudinaryService {
  private cloudinaryUrl = 'https://api.cloudinary.com/v1_1/dtttlg4se/upload';
  private uploadPreset = 'Thenu_CarRent_Max'; 

  constructor(private http: HttpClient) {}

  uploadImage(image: File): Observable<any> {
    const formData = new FormData();
    formData.append('file', image);
    formData.append('upload_preset', this.uploadPreset);
    return this.http.post(this.cloudinaryUrl, formData);
  }
}
