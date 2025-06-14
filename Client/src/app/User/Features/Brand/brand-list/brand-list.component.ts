import { Component, OnInit } from '@angular/core';
import { Brand } from '../models/brand.model';
import { BrandService } from '../services/brand.service';
import { CommonModule } from '@angular/common';
import { IMG_URL } from '../../../../app.config';

@Component({
  selector: 'app-brand-list',
  imports: [CommonModule],
  templateUrl: './brand-list.component.html',
  styleUrl: './brand-list.component.css',
})
export class BrandListComponent implements OnInit {
  urlImage: string = `${IMG_URL}`;
  brands: Brand[] = [];
  displayedBrands: Brand[][] = [];
  pageIndex: number = 1;
  pageSize: number = 5;
  groupSize: number = 5;
  hasMore: boolean = true;

  constructor(private brandService: BrandService) {}

  ngOnInit(): void {
    this.loadBrands();
  }

  loadBrands(): void {
    this.brandService
      .getBrandList(this.pageIndex, this.pageSize)
      .subscribe((response) => {
        this.brands = [...this.brands, ...response.items]; // Thêm dữ liệu mới
        this.updateDisplayedBrands(); // Cập nhật thương hiệu hiển thị
        this.pageIndex++; // Tăng trang
        this.hasMore = response.items.length === this.pageSize; // Kiểm tra còn dữ liệu không
      });
  }

  updateDisplayedBrands(): void {
    // Tạo các nhóm (rows), mỗi nhóm tối đa groupSize item
    this.displayedBrands = [];
    for (let i = 0; i < this.brands.length; i += this.groupSize) {
      this.displayedBrands.push(this.brands.slice(i, i + this.groupSize));
    }
  }

  loadMore(): void {
    const startIndex = this.displayedBrands.length * this.groupSize;
    const endIndex = startIndex + this.groupSize;

    if (startIndex < this.brands.length) {
      const newGroup = this.brands.slice(startIndex, endIndex);
      this.displayedBrands.push(newGroup); // Thêm nhóm mới vào danh sách hiển thị
    } else if (this.hasMore) {
      this.loadBrands(); // Tải thêm dữ liệu từ API nếu còn
    }
  }
}
