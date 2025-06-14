import { Component } from '@angular/core';
import { SliderComponent } from '../../Core/Component/slider/slider.component';
import { BrandListComponent } from '../../Features/Brand/brand-list/brand-list.component';
import { BlogListComponent } from '../../Core/Component/blog-list/blog-list.component';

@Component({
  selector: 'app-home-page',
  imports: [SliderComponent, BrandListComponent, BlogListComponent],
  templateUrl: './home-page.component.html',
  styleUrl: './home-page.component.css',
})
export class HomePageComponent {}
