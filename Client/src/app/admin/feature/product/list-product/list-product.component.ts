import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { Product } from '../models/product.model';
import { Router } from '@angular/router';
import { ProductService } from '../service/product.service';
import { IMG_URL } from '../../../../app.config';
import { DeleteProduct } from '../models/delete-product.model';

@Component({
  selector: 'app-list-product',
  imports: [CommonModule],
  templateUrl: './list-product.component.html',
  styleUrl: './list-product.component.css',
})
export class ListProductComponent {
  urlImage: string = `${IMG_URL}`;

  products$?: Observable<Product[]>;
  productsSub?: Subscription;

  model?: DeleteProduct;

  constructor(private router: Router, private productService: ProductService) {
    this.model = {
      status: '0',
    };
  }

  ngOnInit(): void {
    this.products$ = this.productService.getAllProduct();
  }

  onClick(event: Event): void {
    const selectedValue = (event.target as HTMLSelectElement).value;
    if (selectedValue === 'Block') {
      console.log('User selected Block');
    } else if (selectedValue === 'Active') {
      console.log('User selected Active');
    }
  }

  updateProduct(productId: string) {
    this.router.navigateByUrl(`admin/product/update/${productId}`);
  }

  // Khi click delete chỉ change status về string chứ không xóa

  // deleteProduct(productId: string) {
  //   this.model?.status='0';
  //   if (confirm('Bạn chắc chắn muốn xóa Product này?')) {
  //     this.productsSub = this.productService
  //       .deleteProduct(productId,this.model)
  //       .subscribe({
  //         next: (response) => {
  //           this.products$ = this.productService.getAllProduct();
  //         },
  //         error: (err) => {
  //           console.log(err);
  //         },
  //       });
  //   }
  // }
  addProduct() {
    this.router.navigateByUrl('admin/product/add');
  }

  addSize(): void {
    this.router.navigateByUrl('admin/size/add');
  }
}
