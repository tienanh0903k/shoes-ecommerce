import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ProductRequest } from '../models/addproduct.moddel';
import { Observable, Subscription } from 'rxjs';
import { BrandViewModel } from '../../brand/model/brand_viewmodel';
import { BrandService } from '../../brand/service/brand-service.service';
import { ActivatedRoute } from '@angular/router';
import { ProductService } from '../service/product.service';
import { CommonModule } from '@angular/common';
import { Brand } from '../../brand/model/brand';

@Component({
  selector: 'app-update-product',
  imports: [FormsModule, CommonModule],
  templateUrl: './update-product.component.html',
  styleUrl: './update-product.component.css',
})
export class UpdateProductComponent {
  product: ProductRequest;
  id?: string | null;
  fileImage: File | null = null;
  baseUrl: string = '/Images/Product/';

  brand$?: Observable<Brand[]>;

  activedRouteSubscription?: Subscription;
  updateProductSubscripton?: Subscription;

  constructor(
    private productService: ProductService,
    private brandService: BrandService,
    private activeRouter: ActivatedRoute
  ) {
    this.product = {
      productName: '',
      productImage: '',
      price: 0,
      description: '',
      status: '',
      brandId: '',
    };
  }

  ngOnDestroy() {
    this.activedRouteSubscription?.unsubscribe;
    this.updateProductSubscripton?.unsubscribe;
  }

  ngOnInit(): void {
    this.brand$ = this.brandService.getAllBrand();
    this.activedRouteSubscription = this.activeRouter.paramMap.subscribe({
      next: (params) => {
        this.id = params.get('id');
        console.log(this.id);
        if (this.id) {
          this.productService.getProductById(this.id).subscribe({
            next: (response) => {
              this.product = response;
            },
            error: (err) => {
              console.log(err);
            },
          });
        }
      },
    });
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
      this.product.productImage = this.baseUrl + input.files[0].name; // Lấy tên file
      this.fileImage = input.files[0];
    } else {
      this.product.productImage = ''; // Không có file được chọn
    }
  }

  onFormSubmit() {
    if (this.id) {
      if (this.product) {
        if (this.fileImage != null) {
          const formData = new FormData();
          formData.append('file', this.fileImage);

          this.updateProductSubscripton = this.productService
            .uploadImage(formData)
            .subscribe({
              next: (reponse) => {
                this.updateProductSubscripton = this.productService
                  .updateProduct(this.id!, this.product)
                  .subscribe({
                    next: (response) => {
                      console.log('update ok');
                    },
                    error: (err) => {
                      console.log(err);
                    },
                  });
              },
              error: (err) => {
                console.log('update error');
              },
            });
        }
      }
    }
  }
}
