import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Product } from '../models/product.model';
import { BASE_URL } from '../../../../app.config';
import { PaginatedResult } from '../models/paginated-result.model';
import { ProductFilter } from '../models/product-filter.model';
import { Size } from '../models/size.model';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  constructor(private http: HttpClient) {}

  getProductsByStatus(
    status: string,
    pageIndex: number,
    pageSize: number
  ): Observable<PaginatedResult<Product>> {
    const params = {
      status,
      pageIndex: pageIndex.toString(),
      pageSize: pageSize.toString(),
    };
    return this.http.get<PaginatedResult<Product>>(
      `${BASE_URL}/Product/Get-All-Product-With-Status`,
      { params }
    );
  }

  getFilteredProducts(
    filter: ProductFilter
  ): Observable<PaginatedResult<Product>> {
    return this.http.post<PaginatedResult<Product>>(
      `${BASE_URL}/Product/Get-Filtered-Products`,
      filter
    );
  }

  getProductById(productId: string): Observable<Product> {
    return this.http.get<Product>(
      `${BASE_URL}/Product/Get-Product-By-Id/${productId}`
    );
  }

  getSizeByProductId(productId: string): Observable<Size[]> {
    return this.http.get<Size[]>(
      `${BASE_URL}/Size/get-size-by-product/${productId}`
    );
  }

  getSimilarProducts(
    status: string,
    brandId: string,
    productId: string,
    pageIndex: number,
    pageSize: number
  ): Observable<PaginatedResult<Product>> {
    let params = {
      status,
      brandId,
      productId,
      pageIndex: pageIndex.toString(),
      pageSize: pageSize.toString(),
    };
    return this.http.get<PaginatedResult<Product>>(
      `${BASE_URL}/Product/Get-Similar-Product`,
      { params }
    );
  }
}
