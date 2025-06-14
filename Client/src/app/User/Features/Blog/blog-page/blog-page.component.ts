import { Component, OnInit } from '@angular/core';
import { Blog } from '../models/blog.model';
import { BlogService } from '../services/blog.service';
import { CommonModule } from '@angular/common';
import { IMG_URL } from '../../../../app.config';
import { Router } from '@angular/router';

@Component({
  selector: 'app-blog-page',
  imports: [CommonModule],
  templateUrl: './blog-page.component.html',
  styleUrl: './blog-page.component.css',
})
export class BlogPageComponent implements OnInit {
  urlImage: string = `${IMG_URL}`;
  blogs: Blog[] = [];
  pageIndex: number = 1;
  pageSize: number = 4;
  totalPages: number = 0;
  Math: any;

  constructor(private blogService: BlogService, private router: Router) {}

  ngOnInit(): void {
    this.loadBlogs();
  }

  loadBlogs(): void {
    this.blogService
      .getBlogList(this.pageIndex, this.pageSize)
      .subscribe((response) => {
        this.blogs = response.items;
        this.totalPages = response.totalPages;
      });
  }

  onPageChange(page: number): void {
    if (page > 0 && page <= this.totalPages) {
      this.pageIndex = page;
      this.loadBlogs();
    }
  }

  viewBlogDetail(blogId: string): void {
    this.router.navigate(['/blog', blogId]);
  }
}
