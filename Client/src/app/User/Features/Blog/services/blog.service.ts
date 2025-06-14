import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PaginatedResult } from '../../Product/models/paginated-result.model';
import { BASE_URL } from '../../../../app.config';
import { Blog } from '../models/blog.model';

@Injectable({
  providedIn: 'root',
})
export class BlogService {
  constructor(private http: HttpClient) {}

  getBlogList(
    pageIndex: number,
    pageSize: number
  ): Observable<PaginatedResult<Blog>> {
    const params = {
      pageIndex: pageIndex.toString(),
      pageSize: pageSize.toString(),
    };
    return this.http.get<PaginatedResult<Blog>>(
      `${BASE_URL}/Blog/Get-All-Blog-Pagination`,
      { params }
    );
  }

  getBlogDetail(blogId: string): Observable<Blog> {
    return this.http.get<Blog>(`${BASE_URL}/Blog/get-blog-by-id/${blogId}`);
  }
}
