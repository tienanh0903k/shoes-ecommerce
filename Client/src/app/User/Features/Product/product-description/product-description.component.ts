import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { ReviewComponent } from '../../Review/review/review.component';
import { Product } from '../models/product.model';

@Component({
  selector: 'app-product-description',
  imports: [CommonModule, ReviewComponent],
  templateUrl: './product-description.component.html',
  styleUrl: './product-description.component.css',
})
export class ProductDescriptionComponent {
  activeTab: string = 'description';
  @Input() product?: Product;
}
