import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PaginatedResult } from '../../Product/models/paginated-result.model';
import { Brand } from '../models/brand.model';
import { BASE_URL } from '../../../../app.config';

@Injectable({
  providedIn: 'root',
})
export class BrandService {
  constructor(private http: HttpClient) {}

  getBrandList(
    pageIndex: number,
    pageSize: number
  ): Observable<PaginatedResult<Brand>> {
    const params = {
      pageIndex: pageIndex.toString(),
      pageSize: pageSize.toString(),
    };
    return this.http.get<PaginatedResult<Brand>>(
      `${BASE_URL}/Brand/Get-All-Brand-Pagination`,
      { params }
    );
  }

  getAllBrand(): Observable<Brand[]> {
    return this.http.get<Brand[]>(`${BASE_URL}/Brand/get-all-brand`);
  }
}
