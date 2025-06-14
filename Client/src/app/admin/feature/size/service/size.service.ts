import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Size } from '../models/size';
import { BASE_URL } from '../../../../app.config';
import { SizeViewModel } from '../models/size_viewmodel';

@Injectable({
  providedIn: 'root'
})
export class SizeService {

  constructor(private http: HttpClient) { }

  getAllSize() : Observable<Size[]> {
    return this.http.get<Size[]>(`${BASE_URL}/Size/get-all-size`)
  }

  getSizeById(id: string) : Observable<Size> {
    return this.http.get<Size>(`${BASE_URL}/Size/get-size-by-id/${id}`)
  }

  addSize(size: SizeViewModel) : Observable<void> {
    return this.http.post<void>(`${BASE_URL}/Size/add-new-size`, size)
  }

  updateSize(id: string, size: SizeViewModel) : Observable<void> {
    return this.http.put<void> (`${BASE_URL}/Size/update-size${id}`, size)
  }

  deleteSize(id: string) : Observable<void>{
    return this.http.delete<void> (`${BASE_URL}/Size/delete-size/${id}`)
  }
}
