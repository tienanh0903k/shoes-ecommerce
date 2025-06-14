import { Component } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { Payment } from '../../../../User/Features/payment/models/payment.model';
import { OrderService } from '../services/order.service';

@Component({
  selector: 'app-order-details',
  imports: [],
  templateUrl: './order-details.component.html',
  styleUrl: './order-details.component.css',
})
export class OrderDetailsComponent {
  order$?: Observable<Payment[]>;
  orderSubsription?: Subscription;

  constructor(private orderService: OrderService) {}

  gOnInit(): void {
    this.order$ = this.orderService.getAllOrder();
  }

  ngOnDestroy(): void {
    this.orderSubsription?.unsubscribe();
  }
}
