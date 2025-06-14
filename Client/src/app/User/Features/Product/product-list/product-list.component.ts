import { CommonModule } from '@angular/common';
import { Component, Input, OnInit, SimpleChanges } from '@angular/core';
import { Product } from '../models/product.model';
import { ProductService } from '../services/product.service';
import { ProductFilter } from '../models/product-filter.model';
import { Router } from '@angular/router';
import { IMG_URL } from '../../../../app.config';

@Component({
  selector: 'app-product-list',
  imports: [CommonModule],
  templateUrl: './product-list.component.html',
  styleUrl: './product-list.component.css',
})
export class ProductListComponent implements OnInit {
  urlImage: string = `${IMG_URL}`;
  @Input() filters!: ProductFilter;
  products: Product[] = [];
  currentPage: number = 1;
  itemsPerPage: number = 8;
  totalPages: number = 0;

  constructor(private productService: ProductService, private router: Router) {}

  ngOnInit(): void {
    this.loadProducts();
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['filters']) {
      this.loadProducts();
    }
  }

  loadProducts(): void {
    this.filters.status = '1';
    const filter = {
      ...this.filters,
      pageIndex: this.currentPage,
      pageSize: this.itemsPerPage,
    };
    this.productService.getFilteredProducts(filter).subscribe((response) => {
      this.products = response.items;
      this.totalPages = response.totalPages;
    });
  }

  onPageChange(page: number): void {
    if (page > 0 && page <= this.totalPages) {
      this.currentPage = page;
      this.loadProducts();
    }
  }

  viewProductDetail(productId: string): void {
    this.router.navigate(['/product', productId]);
  }

  addToCart(product: any): void {
    console.log('Thêm sản phẩm vào giỏ:', product);
  }
}
