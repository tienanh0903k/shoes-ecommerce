import { Component } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { Cart } from '../models/Cart.model';
import { CartService } from '../service/cart.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { CartItem } from '../models/CartItem.model';
import { updateCartItem } from '../models/updateCartItem.model';
import { IMG_URL } from '../../../../app.config';

@Component({
  selector: 'app-shopping-cart',
  imports: [CommonModule],
  templateUrl: './shopping-cart.component.html',
  styleUrl: './shopping-cart.component.css',
})
export class ShoppingCartComponent {
  urlImage: string = `${IMG_URL}`;

  cart$?: Observable<Cart>;
  cartSubsription?: Subscription;
  total: number = 0;

  constructor(private cartService: CartService, private router: Router) {}

  ngOnInit(): void {
    this.fetchCart();
  }

  ngOnDestroy(): void {
    this.cartSubsription?.unsubscribe();
  }

  updateQuantity(item: CartItem, newQuantity: number): void {
    if (newQuantity < 1) {
      return;
    }
    item.quantity = newQuantity;
    this.updateCartItem(item);
  }

  onQuantityChange(item: CartItem, event: Event): void {
    const target = event.target as HTMLInputElement;
    if (target && target.value) {
      const newQuantity = parseInt(target.value, 10);
      if (newQuantity > 0) {
        item.quantity = newQuantity;
        this.updateCartItem(item);
      }
    }
  }

  updateCartItem(item: CartItem): void {
    var updateCartItem: updateCartItem = {
      quantity: item.quantity,
      price: item.price,
      size: item.size,
    };
    this.cartSubsription = this.cartService
      .updateCartItem(item.cartId, item.productId, item.size, updateCartItem)
      .subscribe({
        next: () => {
          console.log('Update cart item successfully');
          this.fetchCart();
        },
        error: (error) => {
          console.log('Update cart item failed');
        },
      });
  }

  removeItem(item: CartItem): void {
    this.cartSubsription = this.cartService
      .deleteCartItem(item.cartId, item.productId, item.size)
      .subscribe({
        next: () => {
          console.log('Remove cart item successfully');
          this.fetchCart();
        },
        error: (error) => {
          console.log('Remove cart item failed');
        },
      });
  }

  fetchCart(): void {
    this.cart$ = this.cartService.getCartOfUser();
    this.cart$.subscribe((cart) => {
      this.total = cart.items.reduce(
        (sum, item) => sum + item.quantity * item.price,
        0
      );
    });
  }

  nextOrder(): void {
    this.router.navigateByUrl('/checkout');
  }

  backHome(): void {
    this.router.navigateByUrl('/');
  }
}
