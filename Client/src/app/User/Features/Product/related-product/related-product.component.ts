import { CommonModule } from '@angular/common';
import {
  Component,
  Input,
  OnChanges,
  OnInit,
  SimpleChanges,
} from '@angular/core';
import { Product } from '../models/product.model';
import { ProductService } from '../services/product.service';
import { PaginatedResult } from '../models/paginated-result.model';
import { RouterLink } from '@angular/router';
import { IMG_URL } from '../../../../app.config';

@Component({
  selector: 'app-related-product',
  imports: [CommonModule, RouterLink],
  templateUrl: './related-product.component.html',
  styleUrl: './related-product.component.css',
})
export class RelatedProductComponent implements OnInit, OnChanges {
  urlImage: string = `${IMG_URL}`;
  @Input() product?: Product;
  productsSimilar: Product[] = [];
  chunkedProducts: Product[][] = [];
  status: string = '1';
  pageSize = 8;
  pageIndex = 1;

  constructor(private productService: ProductService) {}

  ngOnInit(): void {
    this.loadRelatedProducts();
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['product']) {
      this.loadRelatedProducts();
    }
  }
  loadRelatedProducts(): void {
    if (this.product)
      this.productService
        .getSimilarProducts(
          this.status,
          this.product.brandId,
          this.product.productId,
          this.pageIndex,
          this.pageSize
        )
        .subscribe((response: PaginatedResult<Product>) => {
          this.chunkedProducts = this.chunkArray(response.items, 4);
        });
  }

  private chunkArray(array: Product[], chunkSize: number): Product[][] {
    if (array.length === 0) {
      return [];
    }

    if (array.length <= chunkSize) {
      return [array];
    }

    const result: Product[][] = [];
    for (let i = 0; i < array.length; i += chunkSize) {
      result.push(array.slice(i, i + chunkSize));
    }
    return result;
  }
}
