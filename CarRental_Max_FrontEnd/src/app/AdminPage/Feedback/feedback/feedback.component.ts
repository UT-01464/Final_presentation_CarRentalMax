import { Component, OnInit } from '@angular/core';
import { FeedbackService, Feedback } from '../../../Services/Feedback/feedback.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-feedback',
  imports: [CommonModule,FormsModule],
  templateUrl: './feedback.component.html',
  styleUrl: './feedback.component.css'
})
export class FeedbackComponent implements OnInit{

  constructor(private feedbackService: FeedbackService) {}

  feedbackList: Feedback[] = [];

  ngOnInit(): void {
    this.loadFeedback();
  }

  loadFeedback(): void {
    this.feedbackService.getAllFeedback().subscribe(feedback => {
      this.feedbackList = feedback;
    });
  }

  sendResponse(feedback: Feedback): void {
    // Logic to send response (e.g., open a modal to enter the response)
  }

}
