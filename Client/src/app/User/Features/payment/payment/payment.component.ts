import { Component } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { Cart } from '../../cart/models/Cart.model';
import { CartService } from '../../cart/service/cart.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { Payment } from '../models/payment.model';
import { PaymentService } from '../service/payment.service';
import { AddOrder } from '../models/order.model';
import { FormsModule, NgForm } from '@angular/forms';

@Component({
  selector: 'app-payment',
  imports: [CommonModule, FormsModule],
  templateUrl: './payment.component.html',
  styleUrl: './payment.component.css',
})
export class PaymentComponent {
  cart$?: Observable<Cart>;
  payment$?: Observable<Payment[]>;
  paymentSubsription?: Subscription;
  total: number = 0;
  orderData = {
    paymentId: '',
    receiverName: '',
    receiverPhone: '',
    receiverAddress: '',
    ward: '',
    district: '',
    city: '',
    notes: '',
  };

  constructor(private cartService: CartService, private paymentService: PaymentService, private router: Router) {
  }

  ngOnInit(): void {
    this.payment$ = this.paymentService.getAllPayment();
    this.cart$ = this.cartService.getCartOfUser();
    this.cart$.subscribe((cart) => {
      this.total = cart.items.reduce(
        (sum, item) => sum + item.quantity * item.price,
        0
      );
    });
  }

  ngOnDestroy(): void {
    this.paymentSubsription?.unsubscribe();
  }


  onFormSubmit(form: NgForm): void {
    var address = this.orderData.ward + ', ' + this.orderData.district + ', ' + this.orderData.city + ',';
    var model: AddOrder = {
      paymentId: this.orderData.paymentId,
      receiverName: this.orderData.receiverName,
      receiverPhone: this.orderData.receiverPhone,
      receiverAddress: address + this.orderData.receiverAddress,
    }
    this.paymentSubsription = this.paymentService.addOrder(model).subscribe(
      {
        next: (response : any) => {
          window.location.href = response.payUrl;
          form.resetForm();
          
        },
        error: err => {
          console.log(err);

        }
      }
    );
  }
}
