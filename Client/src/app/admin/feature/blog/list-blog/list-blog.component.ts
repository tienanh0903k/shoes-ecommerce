import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Blog } from '../models/blog.model';
import { Observable, Subscription } from 'rxjs';
import { Router } from '@angular/router';
import { BlogService } from '../services/blog.service';

@Component({
  selector: 'app-list-blog',
  imports: [CommonModule],
  templateUrl: './list-blog.component.html',
  styleUrl: './list-blog.component.css'
})
export class ListBlogComponent {

  blogs$?: Observable<Blog[]>
  blogSub?: Subscription;

  constructor(private router: Router, private blogService: BlogService) {

  }

  ngOnInit(): void {
    this.blogs$ = this.blogService.getAllBlog();
  }
  onClick(event: Event): void {
    const selectedValue = (event.target as HTMLSelectElement).value;
    if (selectedValue === 'Block') {
      console.log('User selected Block');
    } else if (selectedValue === 'Active') {
      console.log('User selected Active');
    }
  }

  addBlog() {
    this.router.navigateByUrl('admin/blog/add');
  }
  updateBlog(blogId: string) {
    this.router.navigateByUrl(`admin/blog/update/${blogId}`);
  }
  deleteBlog(blogId: string) {
    if (confirm('Bạn chắc chắn muốn xóa answer này?')) {
      this.blogSub = this.blogService.deleteBlog(blogId).subscribe(
        {
          next: response => {
            this.blogs$ = this.blogService.getAllBlog();
          },
          error: err => {
            console.log(err);
          }
        }
      )
    }
  }
}
