import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class FeedbackService {
  private apiUrl = 'https://localhost:7038/api/Feedback';

  constructor(private http: HttpClient) {}

  submitFeedback(email: string, message: string) {
    return this.http.post(this.apiUrl, { email, message });
  }

  getAllFeedback() {
    return this.http.get<Feedback[]>(this.apiUrl);
  }

  sendResponse(id: number, response: string) {
    return this.http.post(`${this.apiUrl}/${id}/response`, response);
  }
}

export interface Feedback {
  id: number;
  email: string;
  message: string;
  isResponded: boolean;
  adminResponse?: string; // Optional, as it might not be set initially
}
