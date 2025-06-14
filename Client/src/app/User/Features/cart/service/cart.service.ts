import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Cart } from '../models/Cart.model';
import { BASE_URL } from '../../../../app.config';
import { updateCartItem } from '../models/updateCartItem.model';
import { AddCartItem } from '../models/add-cartItem.model';

@Injectable({
  providedIn: 'root',
})
export class CartService {
  private cartSubject = new BehaviorSubject<any>({ items: [] });
  cart$ = this.cartSubject.asObservable();

  constructor(private http: HttpClient) {}

  getCartOfUser(): Observable<Cart> {
    return this.http.get<Cart>(`${BASE_URL}/Cart/get-cart-of-user`);
  }

  getCartItemsByCartId(cartId: string | null): Observable<any[]> {
    return this.http.get<any[]>(
      `${BASE_URL}/CartItem/cart-item/get-cart-items/${cartId}`
    );
  }

  addToCart(cartItem: AddCartItem): Observable<any> {
    return this.http.post(`${BASE_URL}/CartItem/add-cart-item`, cartItem).pipe(
      tap(() => {
        this.loadCart().subscribe((cart) => {
          this.setCart(cart);
        });
      })
    );
  }

  updateCartItem(
    cartId: string,
    productId: string | undefined,
    size: string,
    model: updateCartItem
  ): Observable<void> {
    return this.http.put<void>(
      `${BASE_URL}/CartItem/update-cart-item/${cartId}/${productId}/${size}`,
      model
    );
  }

  deleteCartItem(
    cartId: string,
    productId: string,
    size: string
  ): Observable<void> {
    return this.http
      .delete<void>(
        `${BASE_URL}/CartItem/remove-cart-item/${cartId}/${productId}/${size}`
      )
      .pipe(
        tap(() => {
          this.loadCart().subscribe((cart) => {
            this.setCart(cart);
          });
        })
      );
  }

  loadCart(): Observable<any> {
    return this.http.get<any>(`${BASE_URL}/Cart/get-cart-of-user`).pipe(
      tap((cart) => {
        this.setCart(cart);
      })
    );
  }
  setCart(cart: any): void {
    this.cartSubject.next(cart);
  }

  getCart(): any {
    return this.cartSubject.value;
  }

  clearCart(): void {
    this.cartSubject.next(null);
  }
}
