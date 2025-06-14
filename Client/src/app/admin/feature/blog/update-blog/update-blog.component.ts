import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Blog } from '../models/blog.model';
import { ActivatedRoute, Router } from '@angular/router';
import { BlogService } from '../services/blog.service';
import { Subscription } from 'rxjs';
import { ProductService } from '../../product/service/product.service';

@Component({
  selector: 'app-update-blog',
  imports: [FormsModule, CommonModule],
  templateUrl: './update-blog.component.html',
  styleUrl: './update-blog.component.css'
})
export class UpdateBlogComponent {

  model: Blog;
  id?: string | null;

  activeRouterSub?: Subscription;
  updateBlogSub?: Subscription;

  baseUrl: string = "https://localhost:7158/Images/Product/";
  fileImage: File | null = null;

  constructor(private router: Router, private blogService: BlogService, private activeRouter: ActivatedRoute, private productService: ProductService) {
    this.model = {
      blogId: '',
      title: '',
      blogImage: '',
      description: '',
      detail: '',
      createDate: new Date()
    }
  }

  ngOnInit(): void {
    this.activeRouterSub = this.activeRouter.paramMap.subscribe(
      {
        next: params => {
          this.id = params.get('id');
          if (this.id) {
            this.blogService.getBlogById(this.id).subscribe(
              {
                next: response => {
                  this.model = response;
                },
                error: err => {
                  console.log(err);
                }
              }
            )
          }
        }
      }
    )
  }
  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;

    if (input.files && input.files.length > 0) {
      this.model.blogImage = this.baseUrl + input.files[0].name; // Lấy tên file
      this.fileImage = input.files[0];
    } else {
      this.model.blogImage = ''; // Không có file được chọn
    }
  }
  onFormSubmit() {
    if (this.id) {
      if (this.model) {
        if (this.fileImage != null) {
          const formData = new FormData();
          formData.append('file', this.fileImage);
          this.updateBlogSub = this.productService.uploadImage(formData).subscribe(
            {
              next: response => {
                console.log('upload image success');
              },
              error: err => {
                console.log(err);
              }
            },
          )
        }

        this.updateBlogSub = this.blogService.updateBlog(this.id, this.model).subscribe(
          {
            next: response => {
              this.router.navigateByUrl('admin/blog')
            },
            error: err => {
              console.log(err);
            }
          }
        )
      }
    }
  }
}
