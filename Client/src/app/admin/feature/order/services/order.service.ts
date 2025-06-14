import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Payment } from '../../../../User/Features/payment/models/payment.model';
import { BASE_URL } from '../../../../app.config';
import { OrderVM } from '../../../../User/Features/presonal-info/models/Order';

@Injectable({
  providedIn: 'root',
})
export class OrderService {
  constructor(private http: HttpClient) {}

  getAllOrder(): Observable<OrderVM[]> {
    return this.http.get<OrderVM[]>(
      `${BASE_URL}/PaymentMethod/get-all-payment-method`
    );
  }
}
