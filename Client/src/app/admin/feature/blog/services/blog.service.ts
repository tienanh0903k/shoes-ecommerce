import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Blog } from '../models/blog.model';
import { BASE_URL } from '../../../../app.config';
import { BlogRequest } from '../models/addblog.model';

@Injectable({
  providedIn: 'root'
})
export class BlogService {

  constructor(private http: HttpClient) { }

  getAllBlog():Observable<Blog[]>{
    return this.http.get<Blog[]>(`${BASE_URL}/Blog/get-all-blog`)
  }
  getBlogById(id: string): Observable<Blog>{
    return this.http.get<Blog>(`${BASE_URL}/Blog/get-blog-by-id/${id}`)
  }
  addNewBlog(model: BlogRequest): Observable<void>{
    return this.http.post<void>(`${BASE_URL}/Blog/add-new-blog`, model)
  }
  updateBlog(id: string, model: Blog): Observable<void>{
    return this.http.put<void>(`${BASE_URL}/Blog/update-blog/${id}`, model)
  }
  deleteBlog(id : string): Observable<void>{
    return this.http.delete<void>(`${BASE_URL}/Blog/delete-blog/${id}`)
  }
}
