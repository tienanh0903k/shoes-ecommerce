import { NgClass } from '@angular/common';
import { Component, ElementRef, ViewChild } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-adminheader',
  imports: [],
  templateUrl: './adminheader.component.html',
  styleUrl: './adminheader.component.css',
})
export class AdminheaderComponent {
  constructor(private router: Router) {}
  onClick(liItem: number): void {
    if (liItem === 1) {
      this.router.navigateByUrl('admin/index');
    }

    if (liItem === 2) {
      this.router.navigateByUrl('admin/product');
    }
    if (liItem === 3) {
      this.router.navigateByUrl('admin/brand');
    }
    if (liItem === 4) {
      this.router.navigateByUrl('admin/blog');
    }

    if (liItem === 5) {
      this.router.navigateByUrl('admin/order');
    }
    if (liItem === 6) {
      this.router.navigateByUrl('admin/customer');
    }
    if (liItem === 7) {
      this.router.navigateByUrl('admin/contact');
    }
  }
}
