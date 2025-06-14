import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BrandViewModel } from '../model/brand_viewmodel';
import { Observable } from 'rxjs';
import { BASE_URL } from '../../../../app.config';
import { Brand } from '../model/brand';

@Injectable({
  providedIn: 'root'
})
export class BrandService {

  constructor(private http: HttpClient) { }

  uploadImage(file: FormData) : Observable<void>{
    return this.http.post<void>(`${BASE_URL}/Product/Upload-Image`, file)
  }

  getAllBrand() : Observable<Brand[]>{
    return this.http.get<Brand[]>(`${BASE_URL}/Brand/get-all-brand`)
  }

  getBrandById(id: string) : Observable<Brand>{
    return this.http.get<Brand>(`${BASE_URL}/Brand/get-brand-by-id/${id}`)
  }

  addBrand(brandVm: BrandViewModel) : Observable<void>{
    return this.http.post<void>(`${BASE_URL}/Brand/add-new-brand`, brandVm)
  }

  updateBrand(id: string, brand: BrandViewModel) : Observable<void>{
    return this.http.put<void>(`${BASE_URL}/Brand/update-brand/${id}`, brand)
  }

  deleteBrand(id: string) : Observable<void>{
    return this.http.delete<void>(`${BASE_URL}/Brand/delete-brand/${id}`)
  }
}
