import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../services/auth.service';
import { CookieService } from 'ngx-cookie-service';
import { LoginRequest } from '../models/login-request.model';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-login',
  imports: [FormsModule, CommonModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent {
  model: LoginRequest;
  loading = false;

  constructor(
    private authService: AuthService,
    private cookieService: CookieService,
    private router: Router
  ) {
    this.model = {
      email: '',
      password: '',
    };
  }

  onFormSubmit(): void {
    this.loading = true;

    this.authService.login(this.model).subscribe({
      next: () => {
        // Lấy role từ token
        const role = this.authService.getUserRole();

        if (role === 'Admin' || role === 'User') {
          // Điều hướng theo role
          const redirectUrl = role === 'Admin' ? '/admin' : '/';
          this.router.navigate([redirectUrl]);

          // Cập nhật thông tin người dùng
          this.authService.getUserInfo().subscribe({
            next: (user) => {
              this.authService.setUser(user); // Cập nhật BehaviorSubject
            },
            error: (err) => {
              console.error('Failed to fetch user info:', err);
              this.authService.logout(); // Đăng xuất nếu lấy thông tin thất bại
              this.router.navigate(['/login']);
            },
          });
        } else {
          console.error('Role is invalid or missing!');
          this.authService.logout(); // Đăng xuất nếu role không hợp lệ
          this.router.navigate(['/login']);
        }
      },
      error: (err) => {
        console.error('Login failed:', err);
        this.loading = false; // Dừng trạng thái loading
        // Hiển thị lỗi hoặc xử lý thông báo nếu cần
      },
      complete: () => {
        this.loading = false; // Dừng trạng thái loading khi hoàn tất
      },
    });
  }
}
