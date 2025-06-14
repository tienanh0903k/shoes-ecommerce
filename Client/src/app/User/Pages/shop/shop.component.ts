import { Component } from '@angular/core';
import { SideBarComponent } from '../../Core/Component/side-bar/side-bar.component';
import { ProductListComponent } from '../../Features/Product/product-list/product-list.component';
import { ProductFilter } from '../../Features/Product/models/product-filter.model';

@Component({
  selector: 'app-shop',
  imports: [SideBarComponent, ProductListComponent],
  templateUrl: './shop.component.html',
  styleUrl: './shop.component.css',
})
export class ShopComponent {
  filters: ProductFilter = { pageIndex: 1, pageSize: 8 };
  onFilterChange(newFilters: any): void {
    this.filters = { ...this.filters, ...newFilters, pageIndex: 1 };
  }
}
