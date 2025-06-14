import { Component, OnInit } from '@angular/core';
import { IMG_URL } from '../../../../app.config';
import { Blog } from '../../../Features/Blog/models/blog.model';
import { BlogService } from '../../../Features/Blog/services/blog.service';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-blog-list',
  imports: [CommonModule],
  templateUrl: './blog-list.component.html',
  styleUrl: './blog-list.component.css',
})
export class BlogListComponent implements OnInit {
  urlImage: string = `${IMG_URL}`;
  blogs: Blog[] = [];
  currentPage: number = 1;
  itemsPerPage: number = 6;
  totalPages: number = 0;

  chunkedBlogs: Blog[][] = [];

  constructor(private blogService: BlogService, private router: Router) {}

  ngOnInit(): void {
    this.loadBlogs();
  }

  loadBlogs(): void {
    this.blogService
      .getBlogList(this.currentPage, this.itemsPerPage)
      .subscribe((response) => {
        this.blogs = response.items;
        this.chunkBlogs();
        this.totalPages = response.totalPages;
      });
  }

  chunkBlogs(): void {
    const chunkSize = 3;
    for (let i = 0; i < this.blogs.length; i += chunkSize) {
      this.chunkedBlogs.push(this.blogs.slice(i, i + chunkSize));
    }
  }

  viewBlogDetail(blogId: string): void {
    this.router.navigate(['/blog', blogId]);
  }
}
