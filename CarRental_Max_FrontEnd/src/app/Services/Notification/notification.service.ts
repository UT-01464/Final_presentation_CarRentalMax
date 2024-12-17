import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  private apiUrl = 'https://localhost:7038/api/Notification'; 

  constructor(private http: HttpClient) {}

  getNotifications(userId: number): Observable<UserNotification[]> {
    return this.http.get<UserNotification[]>(`${this.apiUrl}/${userId}`);
  }
}


export interface UserNotification {
  id: number;
  customerId: number;
  rentalId?: number;
  message: string;
  createdAt: string;
  isRead: boolean;
  isAccepted?: boolean; // Optional property for accepted notifications
}
