import { Component, OnDestroy, OnInit } from '@angular/core';
import { AuthService } from '../../../Features/auth/services/auth.service';
import { User } from '../../../Features/auth/models/user.model';
import { CommonModule } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { IMG_URL } from '../../../../app.config';
import { CartService } from '../../../Features/cart/service/cart.service';
import { CartItem } from '../../../Features/cart/models/CartItem.model';
import { Observable, Subscription } from 'rxjs';
import { Cart } from '../../../Features/cart/models/Cart.model';

@Component({
  selector: 'app-header',
  imports: [CommonModule, RouterLink],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css',
})
export class HeaderComponent implements OnInit, OnDestroy {
  urlImage: string = `${IMG_URL}`;
  user: User | null = null;
  userId: string | null = null;

  cart$?: Observable<Cart>;
  cartSubsription?: Subscription;
  cartCount: number = 0;
  uniqueProductCount: number = 0;

  private userSubscription?: Subscription;

  constructor(
    private authService: AuthService,
    private cartService: CartService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.userSubscription = this.authService.user$.subscribe((user) => {
      this.user = user; // Cập nhật thông tin user
      console.log('User updated:', user);
    });
  }
  ngOnDestroy(): void {
    if (this.userSubscription) {
      this.userSubscription.unsubscribe(); // Hủy Subscription để tránh giữ giá trị cũ
    }
  }
  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}
