import { Component } from '@angular/core';
import { SizeViewModel } from '../models/size_viewmodel';
import { Observable, Subscription } from 'rxjs';
import { SizeService } from '../service/size.service';
import { Product } from '../../product/models/product.model';
import { ProductService } from '../../product/service/product.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-add-size',
  imports: [FormsModule, CommonModule],
  templateUrl: './add-size.component.html',
  styleUrl: './add-size.component.css'
})
export class AddSizeComponent {
  size: SizeViewModel
  addSizeSub?: Subscription

  product$?: Observable<Product[]>

  constructor(private sizeService: SizeService, private productService: ProductService){
    this.size = {
      sizeName: '',
      quantity: 0,
      status: '',
      productId: ''
    }
  }

  ngOnInit() {
    this.product$ = this.productService.getAllProduct()
  }

  ngOnDestroy() {
    this.addSizeSub?.unsubscribe
  }

  onClick(event : Event):void{
    const selectedValue = (event.target as HTMLSelectElement).value;
    if (selectedValue === 'Block') {
      console.log('User selected Block');
    } else if (selectedValue === 'Active') {
      console.log('User selected Active');
    }  
  }

  onFormSubmit() {
    this.addSizeSub = this.sizeService.addSize(this.size).subscribe({
      next: reponse => {
        console.log('add size ok')
      },
      error: err => {
        console.log('add size filed')
      }
    })
  }
}
