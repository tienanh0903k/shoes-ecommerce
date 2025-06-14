import { CommonModule } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ReviewService } from '../services/review.service';
import { Review } from '../models/review.model';
import { AddReview } from '../models/add-review.model';
import { User } from '../../auth/models/user.model';
import { AuthService } from '../../auth/services/auth.service';
import moment from 'moment';

@Component({
  selector: 'app-review',
  imports: [CommonModule, FormsModule],
  templateUrl: './review.component.html',
  styleUrl: './review.component.css',
})
export class ReviewComponent implements OnInit {
  @Input() productId?: string;
  user: User | null = null;
  reviews: Review[] = [];
  userReviewed: Review | null = null;
  showLoadMore: boolean = false;
  userRating: number = 0;
  userComment: string = '';

  constructor(
    private reviewService: ReviewService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.authService.getUserInfo().subscribe((user) => {
      this.user = user;
    });

    this.loadReviews();
    this.loadUserReviewed();
  }

  loadReviews(): void {
    if (this.productId)
      this.reviewService.getReviewsByProductId(this.productId).subscribe(
        (reviews) => {
          this.reviews = reviews;
          this.showLoadMore = reviews.length >= 10;
        },
        (error) => {
          console.error('Lỗi khi tải đánh giá:', error);
        }
      );
  }

  loadUserReviewed(): void {
    if (this.productId)
      this.reviewService.getUserReviewed(this.productId).subscribe((review) => {
        this.userReviewed = review;
      });
  }

  loadMore(): void {
    // Logic tải thêm đánh giá nếu cần
    console.log('Tải thêm đánh giá...');
  }

  setRating(rating: number) {
    this.userRating = rating;
  }

  submitReview(): void {
    if (!this.userComment || this.userRating === 0) {
      alert('Vui lòng nhập nội dung và đánh giá.');
      return;
    }

    const newReview: AddReview = {
      productId: this.productId,
      rating: this.userRating,
      reviewText: this.userComment,
      status: '1',
    };

    this.reviewService.addReview(newReview).subscribe(
      (response) => {
        this.reviews.unshift(response);
        this.userComment = '';
        this.userRating = 0;
        alert('Gửi đánh giá thành công.');
        this.loadUserReviewed();
      },
      (message) => {
        alert('Bạn chưa mua sản phẩm này.');
      }
    );
  }

  formatDate(date: string): string {
    return moment(date).format('DD-MM-YYYY HH:mm:ss');
  }
}
