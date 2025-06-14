import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Customer } from '../models/customer.model';
import { BASE_URL } from '../../../../app.config';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  constructor(private http: HttpClient) { }

  getAllCustomer(): Observable<Customer[]>{
    return this.http.get<Customer[]>(`${BASE_URL}/User/get-all-user`)
  }
  getCustomerById(id: string): Observable<Customer>{
    return this.http.get<Customer>(`${BASE_URL}/User/get-user-by-id/${id}`)
  }
  updateStatus(id: string, model: Customer) : Observable<void>{
    return this.http.put<void>(`${BASE_URL}/User/update-user-status/${id}`, model)
  }
}
