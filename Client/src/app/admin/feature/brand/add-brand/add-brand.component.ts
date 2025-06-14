import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrandViewModel } from '../model/brand_viewmodel';
import { Subscription } from 'rxjs';
import { BrandService } from '../service/brand-service.service';
import { routes } from '../../../../app.routes';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-brand',
  imports: [FormsModule],
  templateUrl: './add-brand.component.html',
  styleUrl: './add-brand.component.css',
})
export class AddBrandComponent {
  brand: BrandViewModel;
  addBrandSubscription?: Subscription;
  fileImage: File | null = null;
  baseUrl: string = '/Images/Brand/';
  constructor(private brandService: BrandService, private router: Router) {
    this.brand = {
      brandName: '',
      brandImage: '',
      description: '',
    };
  }
  ngOnDestroy() {
    this.addBrandSubscription?.unsubscribe;
  }
  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;

    if (input.files && input.files.length > 0) {
      this.brand.brandImage = this.baseUrl + input.files[0].name; // Lấy tên file
      this.fileImage = input.files[0];
    } else {
      this.brand.brandImage = ''; // Không có file được chọn
    }
  }
  addBrand() {
    if (this.fileImage != null) {
      const formData = new FormData();
      formData.append('file', this.fileImage);
      this.addBrandSubscription = this.brandService
        .uploadImage(formData)
        .subscribe({
          next: (reponse) => {
            this.addBrandSubscription = this.brandService
              .addBrand(this.brand)
              .subscribe({
                next: (reponse) => {
                  console.log('add success');
                  this.router.navigateByUrl('admin/brand');
                },
                error: (err) => {
                  console.log(this.brand);
                  console.log('error');
                },
              });
          },
          error: (err) => {
            console.log('upload file error');
          },
        });
    }
  }
}
