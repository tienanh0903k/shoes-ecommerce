import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { User } from '../auth/models/user.model';

import { AuthService } from '../auth/services/auth.service';
import { IMG_URL } from '../../../app.config';
import { ChangePasswordComponent } from '../change-password/change-password.component';
import { TrackingOrderComponent } from '../tracking-order/tracking-order.component';

@Component({
  selector: 'app-presonal-info',
  imports: [
    CommonModule,
    FormsModule,
    ChangePasswordComponent,
    TrackingOrderComponent,
  ],
  templateUrl: './presonal-info.component.html',
  styleUrl: './presonal-info.component.css',
})
export class PresonalInfoComponent implements OnInit {
  urlImage: string = `${IMG_URL}`;
  user: User = {
    id: '',
    fullName: '',
    avatar: '',
    email: '',
    phoneNumber: '',
    address: '',
  };

  activeTab: string = 'info';
  selectedFile: File | null = null;
  errorMessage: string = '';

  constructor(private authService: AuthService) {}

  ngOnInit(): void {
    const currentUser = this.authService.getUser();
    if (currentUser) {
      this.user = { ...currentUser };
    } else {
      this.authService.getUserInfo().subscribe({
        next: (data) => {
          this.user = data;
          this.authService.setUser(data); // Đồng bộ thông tin vào AuthService
        },
      });
    }
  }

  getUserInfo(): void {
    this.authService.updateUserInfo(this.user).subscribe({
      next: () => {
        this.authService.setUser(this.user);
        alert('Cập nhật thông tin thành công!');
      },
    });
  }

  onFileSelected(event: any): void {
    this.selectedFile = event.target.files[0] || null;
    if (this.selectedFile) {
      this.uploadAvatar();
    }
  }

  uploadAvatar(): void {
    if (!this.selectedFile) {
      return;
    }

    const formData = new FormData();
    formData.append('file', this.selectedFile);

    this.authService.uploadAvatar(formData).subscribe({
      next: (response: any) => {
        this.user.avatar = response.url;
      },
    });
  }

  updateProfile(): void {
    this.authService.updateUserInfo(this.user).subscribe({
      next: () => {
        alert('Cập nhật thông tin thành công!');
      },
    });
  }
}
