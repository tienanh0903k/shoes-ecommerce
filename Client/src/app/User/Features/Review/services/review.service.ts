import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Review } from '../models/review.model';
import { BASE_URL } from '../../../../app.config';
import { AddReview } from '../models/add-review.model';

@Injectable({
  providedIn: 'root',
})
export class ReviewService {
  constructor(private http: HttpClient) {}

  getReviewsByProductId(productId: string): Observable<Review[]> {
    return this.http.get<Review[]>(
      `${BASE_URL}/Review/Get-Reviews/${productId}`
    );
  }

  getUserReviewed(productId: string): Observable<Review> {
    return this.http.get<Review>(
      `${BASE_URL}/Review/Get-Review-By-Product-User/${productId}`
    );
  }

  addReview(review: AddReview): Observable<Review> {
    return this.http.post<Review>(`${BASE_URL}/Review/Add-Review`, review);
  }
}
