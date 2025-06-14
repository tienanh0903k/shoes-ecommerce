import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { Brand } from '../model/brand';
import { Router } from '@angular/router';
import { BrandService } from '../service/brand-service.service';
import { IMG_URL } from '../../../../app.config';

@Component({
  selector: 'app-list-brand',
  imports: [CommonModule],
  templateUrl: './list-brand.component.html',
  styleUrl: './list-brand.component.css',
})
export class ListBrandComponent {
  urlImage: string = `${IMG_URL}`;
  brands$?: Observable<Brand[]>;
  brandsSub?: Subscription;

  constructor(private router: Router, private brandService: BrandService) {}

  ngOnInit(): void {
    this.brands$ = this.brandService.getAllBrand();
  }

  onClick(event: Event): void {
    const selectedValue = (event.target as HTMLSelectElement).value;
    if (selectedValue === 'Block') {
      console.log('User selected Block');
    } else if (selectedValue === 'Active') {
      console.log('User selected Active');
    }
  }

  updateBrand(brandId: string) {
    this.router.navigateByUrl(`admin/brand/update/${brandId}`);
  }

  // Khi click delete chỉ change status về string chứ không xóa
  deleteBrand(brandId: string) {
    if (confirm('Bạn chắc chắn muốn xóa Product này?')) {
      this.brandsSub = this.brandService.deleteBrand(brandId).subscribe({
        next: (response) => {
          this.brands$ = this.brandService.getAllBrand();
        },
        error: (err) => {
          console.log(err);
        },
      });
    }
  }
  addBrand() {
    this.router.navigateByUrl('admin/brand/add');
  }
}
