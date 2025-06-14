import { Component, OnInit } from '@angular/core';
import { ProductDetailComponent } from '../../Features/Product/product-detail/product-detail.component';
import { RelatedProductComponent } from '../../Features/Product/related-product/related-product.component';
import { ProductDescriptionComponent } from '../../Features/Product/product-description/product-description.component';
import { ActivatedRoute } from '@angular/router';
import { ProductService } from '../../Features/Product/services/product.service';
import { Product } from '../../Features/Product/models/product.model';
import { Subscription } from 'rxjs';
import { Size } from '../../Features/Product/models/size.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-product',
  imports: [
    ProductDetailComponent,
    RelatedProductComponent,
    ProductDescriptionComponent,
    CommonModule,
  ],
  templateUrl: './product.component.html',
  styleUrl: './product.component.css',
})
export class ProductComponent implements OnInit {
  id: string | null = null;
  product?: Product;
  paramsSubcription?: Subscription;
  sizes: Size[] = [];
  constructor(
    private route: ActivatedRoute,
    private productService: ProductService
  ) {}

  ngOnInit(): void {
    this.paramsSubcription = this.route.paramMap.subscribe({
      next: (params) => {
        const newId = params.get('id'); // Lấy id mới từ route
        if (newId && newId !== this.id) {
          this.id = newId; // Cập nhật id
          this.loadProduct(); // Gọi API khi id thay đổi
        }
      },
    });
  }

  loadProduct(): void {
    if (!this.id) return;

    if (this.id) {
      this.productService.getProductById(this.id).subscribe({
        next: (response) => {
          this.product = response;
          console.log(this.product);
        },
      });
      this.productService.getSizeByProductId(this.id).subscribe({
        next: (response) => {
          this.sizes = response;
          this.sizes.sort((a, b) => a.sizeName.localeCompare(b.sizeName));
        },
      });
    }
  }
}
