import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RegisterRequest } from '../models/register-request.model';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  imports: [FormsModule, CommonModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
})
export class RegisterComponent {
  model: RegisterRequest;
  loading = false;
  errorMessage: string | null = null;

  constructor(private authService: AuthService, private router: Router) {
    this.model = {
      fullName: '',
      email: '',
      password: '',
      confirmPassword: '',
    };
  }

  onSubmit(): void {
    if (this.model.password !== this.model.confirmPassword) {
      this.errorMessage = 'Mật khẩu xác nhận không khớp.';
      return;
    }

    this.loading = true;
    this.errorMessage = null;

    this.authService.register(this.model).subscribe(
      (response) => {
        this.loading = false;
        alert('Đăng ký thành công! Bạn có thể đăng nhập ngay bây giờ.');
        this.router.navigate(['/login']);
      },
      (error) => {
        this.loading = false;
        this.errorMessage =
          error.error.message || 'Đăng ký thất bại. Vui lòng thử lại.';
      }
    );
  }

  onClickBackToLogin(): void {
    this.router.navigate(['/login']);
  }
}
