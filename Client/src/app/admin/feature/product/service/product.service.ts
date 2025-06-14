import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Product } from '../models/product.model';
import { BASE_URL } from '../../../../app.config';
import { ProductRequest } from '../models/addproduct.moddel';
import { ImageRequest } from '../models/image.model';
import { DeleteProduct } from '../models/delete-product.model';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  constructor(private http: HttpClient) {}

  getAllProduct(): Observable<Product[]> {
    return this.http.get<Product[]>(`${BASE_URL}/Product/Get-All-Product`);
  }
  getProductById(id: string): Observable<Product> {
    return this.http.get<Product>(
      `${BASE_URL}/Product/Get-Product-By-Id/${id}`
    );
  }
  addProduct(model: ProductRequest): Observable<void> {
    return this.http.post<void>(`${BASE_URL}/Product/Add-Product`, model);
  }
  updateProduct(id: string, moddel: ProductRequest): Observable<void> {
    return this.http.put<void>(
      `${BASE_URL}/Product/Update-Product/${id}`,
      moddel
    );
  }
  deleteProduct(id: string, model: DeleteProduct): Observable<void> {
    return this.http.put<void>(
      `${BASE_URL}/Product/Delete-Product/${id}`,
      model
    );
  }

  uploadImage(file: FormData): Observable<void> {
    return this.http.post<void>(`${BASE_URL}/Product/Upload-Image`, file);
  }
}
