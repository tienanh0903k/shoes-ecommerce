import { Component } from '@angular/core';
import { BrandViewModel } from '../model/brand_viewmodel';
import { Subscription } from 'rxjs';
import { BrandService } from '../service/brand-service.service';
import { ActivatedRoute } from '@angular/router';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-update-brands',
  imports: [FormsModule],
  templateUrl: './update-brands.component.html',
  styleUrl: './update-brands.component.css',
})
export class UpdateBrandsComponent {
  brand: BrandViewModel;
  id?: string | null;
  fileImage: File | null = null;
  baseUrl: string = '/Images/Brand/';

  activedRouteSubscription?: Subscription;
  updateBrandSubscripton?: Subscription;

  constructor(
    private brandService: BrandService,
    private activedRoute: ActivatedRoute
  ) {
    this.brand = {
      brandName: '',
      brandImage: '',
      description: '',
    };
  }
  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;

    if (input.files && input.files.length > 0) {
      this.brand.brandImage = this.baseUrl + input.files[0].name; // Lấy tên file
      this.fileImage = input.files[0];
    } else {
      this.brand.brandImage = ''; // Không có file được chọn
    }
  }
  ngOnInit() {
    this.activedRouteSubscription = this.activedRoute.paramMap.subscribe({
      next: (params) => {
        this.id = params.get('id');
        if (this.id) {
          this.brandService.getBrandById(this.id).subscribe({
            next: (reponse) => {
              this.brand = reponse;
            },
            error: (error) => {
              console.log('error', error);
            },
          });
        }
      },
    });
  }
  ngOnDestroy() {
    this.activedRouteSubscription?.unsubscribe;
    this.updateBrandSubscripton?.unsubscribe;
  }

  updateBrand() {
    if (this.id) {
      if (this.brand) {
        this.updateBrandSubscripton = this.brandService
          .updateBrand(this.id, this.brand)
          .subscribe({
            next: (reponse) => {
              console.log('update ok');
            },
            error: (err) => {
              console.log('update error');
            },
          });
      }
    }
  }
}
