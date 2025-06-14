import { Component } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { Payment } from '../../../../User/Features/payment/models/payment.model';
import { OrderService } from '../services/order.service';
import { CommonModule } from '@angular/common';
import { OrderVM } from '../../../../User/Features/presonal-info/models/Order';

@Component({
  selector: 'app-list-order',
  imports: [CommonModule],
  templateUrl: './list-order.component.html',
  styleUrl: './list-order.component.css',
})
export class ListOrderComponent {
  orders$?: Observable<OrderVM[]>;
  orderSubsription?: Subscription;

  constructor(private orderService: OrderService) {}

  gOnInit(): void {
    this.orders$ = this.orderService.getAllOrder();
  }

  ngOnDestroy(): void {
    this.orderSubsription?.unsubscribe();
  }
  onClick(event: Event): void {
    const selectedValue = (event.target as HTMLSelectElement).value;
    if (selectedValue === 'Block') {
      console.log('User selected Block');
      // Thực hiện các hành động khi chọn "Block"
    } else if (selectedValue === 'Active') {
      console.log('User selected Active');
      // Thực hiện các hành động khi chọn "Active"
    }
  }
}
