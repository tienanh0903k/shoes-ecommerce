import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BASE_URL } from '../../../../app.config';
import { OrderVM } from '../models/Order';

@Injectable({
  providedIn: 'root'
})
export class PersonalInfoService {

  constructor(private http: HttpClient) {}

  getAllUnpaidOrderOfUser() : Observable<OrderVM[]> {
    return this.http.get<OrderVM[]>(`${BASE_URL}/Order/get-all-unpaid-order-of-user`);
  }
}
