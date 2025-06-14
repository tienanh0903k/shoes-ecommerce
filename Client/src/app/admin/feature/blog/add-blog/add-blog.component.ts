import { Component, input } from '@angular/core';
import { Blog } from '../models/blog.model';
import { Subscription } from 'rxjs';
import { Router } from '@angular/router';
import { BlogService } from '../services/blog.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ProductService } from '../../product/service/product.service';

@Component({
  selector: 'app-add-blog',
  imports: [CommonModule, FormsModule],
  templateUrl: './add-blog.component.html',
  styleUrl: './add-blog.component.css'
})
export class AddBlogComponent {
  model: Blog;
  addBlogSub?: Subscription;
  baseUrl: string = "https://localhost:7158/Images/Product/";
  fileImage: File|null = null;

  constructor(private router: Router, private blogService: BlogService, private productService: ProductService){
    this.model = {
      blogId: '',
      title: '',
      blogImage:'',
      description:'',
      detail:'',
      createDate: new Date()
    }
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

  onFormSubmit(){
    if(this.fileImage != null){
      const formData = new FormData();
      formData.append('file', this.fileImage);
      this.addBlogSub =  this.productService.uploadImage(formData).subscribe(
        {
          next: response =>{
            this.addBlogSub = this.blogService.addNewBlog(this.model).subscribe({
              next: response =>{
                console.log("Success");
                
              },
              error: err =>{
                console.log("Error add blog " + err);
              }
            });
          },
          error: err =>{
            console.log("Error upload Image " + err);
          }
        }
      )
    }
  }
}
