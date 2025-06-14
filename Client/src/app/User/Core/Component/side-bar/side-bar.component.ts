import { CommonModule } from '@angular/common';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Brand } from '../../../Features/Brand/models/brand.model';
import { BrandService } from '../../../Features/Brand/services/brand.service';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-side-bar',
  imports: [CommonModule],
  templateUrl: './side-bar.component.html',
  styleUrl: './side-bar.component.css',
})
export class SideBarComponent implements OnInit {
  @Output() filterChange = new EventEmitter<any>();
  brands: Brand[] = [];

  sizes = ['36', '37', '38', '39', '40', '41'];
  priceRanges = [
    { min: 0, max: 500000, label: '< 500.000 VND' },
    { min: 500000, max: 1000000, label: '500.000 - 1.000.000 VND' },
    { min: 1000000, max: 2000000, label: '1.000.000 - 2.000.000 VND' },
    { min: 2000000, max: null, label: '> 2.000.000 VND' },
  ];

  selectedBrandId: string | null = null;
  selectedSize: string | null = null;
  selectedPriceRange: { min: number; max: number } | null = null;

  constructor(private brandService: BrandService) {}

  ngOnInit(): void {
    this.brandService.getAllBrand().subscribe({
      next: (response) => {
        this.brands = response;
      },
    });
  }

  onBrandChange(event: Event): void {
    const target = event.target as HTMLSelectElement;
    if (target) {
      this.selectedBrandId = target.value || null;
      this.emitFilterChange();
    }
  }

  onSizeClick(size: string): void {
    this.selectedSize = this.selectedSize === size ? null : size;

    this.emitFilterChange();
  }

  onPriceRangeChange(range: any): void {
    this.selectedPriceRange =
      this.selectedPriceRange?.min === range.min &&
      this.selectedPriceRange?.max === range.max
        ? null
        : range;
    this.emitFilterChange();
  }

  emitFilterChange(): void {
    this.filterChange.emit({
      brandId: this.selectedBrandId,
      sizeName: this.selectedSize,
      minPrice: this.selectedPriceRange?.min,
      maxPrice: this.selectedPriceRange?.max,
    });
  }
}
