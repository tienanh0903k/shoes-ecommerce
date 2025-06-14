import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Product } from '../models/product.model';
import { Observable, Subscription } from 'rxjs';
import { Router } from '@angular/router';
import { ProductService } from '../service/product.service';
import { Brand } from '../../brand/model/brand';
import { ProductRequest } from '../models/addproduct.moddel';
import { BrandService } from '../../brand/service/brand-service.service';

@Component({
  selector: 'app-addproduct',
  imports: [FormsModule, CommonModule],
  templateUrl: './addproduct.component.html',
  styleUrl: './addproduct.component.css',
})
export class AddproductComponent {
  model: ProductRequest;
  // brand$?: BrandService
  addProductSub?: Subscription;
  baseUrl: string = '/Images/Product/';
  fileImage: File | null = null;
  brand$?: Observable<Brand[]>;
  constructor(
    private router: Router,
    private productService: ProductService,
    private brandService: BrandService
  ) {
    this.model = {
      productName: '',
      productImage: '',
      price: 0,
      description: '',
      status: '',
      brandId: '',
    };
  }

  ngOnInit() {
    this.brand$ = this.brandService.getAllBrand();
  }

  ngOnDestroy() {
    this.addProductSub?.unsubscribe;
  }
  onClick(event: Event): void {
    const selectedValue = (event.target as HTMLSelectElement).value;
    if (selectedValue === 'Block') {
      console.log('User selected Block');
    } else if (selectedValue === 'Active') {
      console.log('User selected Active');
    }
  }

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;

    if (input.files && input.files.length > 0) {
      this.model.productImage = this.baseUrl + input.files[0].name; // Lấy tên file
      this.fileImage = input.files[0];
    } else {
      this.model.productImage = ''; // Không có file được chọn
    }
  }
  onFormSubmit() {
    if (this.fileImage != null) {
      const formData = new FormData();
      formData.append('file', this.fileImage);
      this.addProductSub = this.productService.uploadImage(formData).subscribe({
        next: (reponse) => {
          this.addProductSub = this.productService
            .addProduct(this.model)
            .subscribe({
              next: (reponse) => {
                console.log('add product cuccess');
              },
              error: (err) => {
                console.log('add product error');
                console.log(this.model);
              },
            });
        },
        error: (err) => {
          console.log('upload image arr');
        },
      });
    }
  }
}
