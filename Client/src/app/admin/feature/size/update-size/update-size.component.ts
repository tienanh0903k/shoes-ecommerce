import { Component } from '@angular/core';
import { SizeViewModel } from '../models/size_viewmodel';
import { Product } from '../../product/models/product.model';
import { Observable, Subscription } from 'rxjs';
import { SizeService } from '../service/size.service';
import { ProductService } from '../../product/service/product.service';
import { ActivatedRoute } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-update-size',
  imports: [FormsModule, CommonModule],
  templateUrl: './update-size.component.html',
  styleUrl: './update-size.component.css'
})
export class UpdateSizeComponent {
  size: SizeViewModel
  product$?: Observable<Product[]>
  id?: string | null
  activedRouteSubscription?: Subscription
  updateSizeSubscripton?: Subscription

  constructor(
    private sizeService: SizeService, 
    private productService: ProductService, 
    private activeRouter: ActivatedRoute){
    this.size = {
      sizeName: '',
      quantity: 0,
      status: '',
      productId: ''
    }
  }

  ngOnDestroy() {
    this.activedRouteSubscription?.unsubscribe
    this.updateSizeSubscripton?.unsubscribe
  }

  onClick(event : Event):void{
    const selectedValue = (event.target as HTMLSelectElement).value;
    if (selectedValue === 'Block') {
      console.log('User selected Block');
    } else if (selectedValue === 'Active') {
      console.log('User selected Active');
    }
    
  }

  ngOnInit() {
    this.product$ = this.productService.getAllProduct()
    this.activedRouteSubscription = this.activeRouter.paramMap.subscribe({
      next: param => {
        this.id = param.get('id')
        if(this.id) {
          this.sizeService.getSizeById(this.id).subscribe({
            next: reponse => {
              this.size = reponse
            },
            error: err => {
              console.log(err)
            }
          })
        }
      }
    })
  }

  onFormSubmit() {
    if(this.id){
      if(this.size){
        this.updateSizeSubscripton = this.sizeService.updateSize(this.id, this.size).subscribe({
          next: reponse => {
            console.log('update cuccess')
          },
          error: err => {
            console.log('error ',err)
          }
        })
      }
    }
  }
}
