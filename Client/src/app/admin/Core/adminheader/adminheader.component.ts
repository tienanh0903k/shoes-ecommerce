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
    switch (liItem) {
      case 1:
        this.router.navigateByUrl('admin/index');
        break;
      case 2:
        this.router.navigateByUrl('admin/product');
        break;
      case 3:
        this.router.navigateByUrl('admin/brand');
        break;
      case 4:
        this.router.navigateByUrl('admin/blog');
        break;
      case 5:
        this.router.navigateByUrl('admin/order');
        break;
      case 6:
        this.router.navigateByUrl('admin/customer');
        break;
      case 7:
        this.router.navigateByUrl('admin/contact');
        break;
      case 8:
        this.logout();
        break;
    }
  }

  logout() {
    if (confirm('Bạn có chắc chắn muốn đăng xuất không?')) {
      localStorage.removeItem('token');
      localStorage.removeItem('user');
      this.router.navigateByUrl('login');
    }
  }
}
