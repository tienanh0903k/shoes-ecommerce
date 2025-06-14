import { Component } from '@angular/core';
import { AuthService } from '../auth/services/auth.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ChangePassword } from '../auth/models/password-user.model';

@Component({
  selector: 'app-change-password',
  imports: [CommonModule, FormsModule],
  templateUrl: './change-password.component.html',
  styleUrl: './change-password.component.css',
})
export class ChangePasswordComponent {
  model: ChangePassword = {
    oldPassword: '',
    newPassword: '',
    confirmPassword: '',
  };
  errorMessage: string = '';

  constructor(private authService: AuthService, private router: Router) {}

  onSubmit(): void {
    if (this.model.newPassword !== this.model.confirmPassword) {
      this.errorMessage = 'Mật khẩu xác nhận không khớp!';
      return;
    }

    this.authService
      .changePassword(this.model.oldPassword, this.model.newPassword)
      .subscribe({
        next: () => {
          alert('Đổi mật khẩu thành công!');
          this.router.navigate(['/']);
        },
        error: (err) => {
          this.errorMessage =
            err.error?.message || 'Đổi mật khẩu thất bại. Vui lòng thử lại.';
          console.error(err);
        },
      });
  }
}
