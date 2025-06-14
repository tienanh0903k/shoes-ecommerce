import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Payment } from '../models/payment.model';
import { BASE_URL } from '../../../../app.config';
import { AddOrder } from '../models/order.model';

@Injectable({
  providedIn: 'root'
})
export class PaymentService {

  constructor(private http: HttpClient) { }
  
    getAllPayment() : Observable<Payment[]> {
      return this.http.get<Payment[]>(`${BASE_URL}/PaymentMethod/get-all-payment-method`);
    }

    addOrder(model: AddOrder): Observable<PaymentResponse> {
      return this.http.post<PaymentResponse>(`${BASE_URL}/Order/add-new-order`, model);
    }
}
