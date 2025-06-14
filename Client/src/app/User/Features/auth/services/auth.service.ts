import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { BehaviorSubject, catchError, Observable, tap } from 'rxjs';
import { LoginRequest } from '../models/login-request.model';
import { LoginResponse } from '../models/login-response.model';
import { User } from '../models/user.model';
import { BASE_URL } from '../../../../app.config';
import { UpdateUser } from '../models/update-user.model';
import { jwtDecode } from 'jwt-decode';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private userSubject: BehaviorSubject<User | null> =
    new BehaviorSubject<User | null>(null);
  public user$: Observable<User | null> = this.userSubject.asObservable();

  constructor(private http: HttpClient, private cookieService: CookieService) {
    const userData = localStorage.getItem('user');
    if (userData) {
      this.userSubject.next(JSON.parse(userData)); // Khởi tạo thông tin người dùng từ localStorage
    }
  }

  // Đăng ký người dùng mới
  register(data: {
    fullName: string;
    email: string;
    password: string;
  }): Observable<any> {
    return this.http.post(`${BASE_URL}/Authentication/register`, data);
  }

  // Đăng nhập và lưu thông tin người dùng
  login(request: LoginRequest): Observable<LoginResponse> {
    return this.http
      .post<LoginResponse>(`${BASE_URL}/Authentication/login`, request)
      .pipe(
        tap((response) => {
          if (!response || !response.token) {
            throw new Error('Invalid response: Missing token');
          }

          // Lưu token vào cookie và localStorage
          this.cookieService.set(
            'Authentication',
            `Bearer ${response.token}`,
            undefined,
            '/',
            undefined,
            true,
            'Strict'
          );
          localStorage.setItem('token', response.token);

          // Lấy thông tin người dùng và cập nhật vào BehaviorSubject
          this.getUserInfo().subscribe({
            next: (user) => {
              this.setUser(user);
            },
            error: () => {
              console.error('Failed to fetch user info');
              this.logout(); // Xóa token nếu không lấy được thông tin người dùng
            },
          });
        }),
        catchError((err) => {
          console.error('Login failed:', err);
          this.logout(); // Đảm bảo xóa token nếu có lỗi
          throw err;
        })
      );
  }

  // Lấy thông tin người dùng từ API
  getUserInfo(): Observable<User> {
    return this.http.get<User>(`${BASE_URL}/Authentication/user-info`);
  }

  // Cập nhật thông tin người dùng
  updateUserInfo(user: UpdateUser): Observable<void> {
    return this.http
      .put<void>(`${BASE_URL}/Authentication/update-user-info`, user)
      .pipe(
        tap(() => {
          // Sau khi cập nhật, đồng bộ thông tin người dùng vào BehaviorSubject
          this.getUserInfo().subscribe((updatedUser) => {
            this.setUser(updatedUser);
          });
        })
      );
  }

  // Upload avatar người dùng
  uploadAvatar(formData: FormData): Observable<any> {
    return this.http
      .post<any>(`${BASE_URL}/Authentication/Upload-Image`, formData)
      .pipe(
        tap((response) => {
          // Sau khi upload avatar, cập nhật thông tin người dùng
          this.getUserInfo().subscribe((user) => {
            this.setUser(user);
          });
        })
      );
  }

  // Đổi mật khẩu
  changePassword(oldPassword: string, newPassword: string): Observable<void> {
    return this.http.put<void>(`${BASE_URL}/Authentication/change-password`, {
      oldPassword,
      newPassword,
    });
  }

  // Đặt thông tin người dùng vào BehaviorSubject và localStorage
  setUser(user: User): void {
    this.userSubject.next(user);
    localStorage.setItem('user', JSON.stringify(user));
  }

  // Lấy thông tin người dùng hiện tại
  getUser(): User | null {
    return this.userSubject.value;
  }

  getUserRole(): string | null {
    const token = localStorage.getItem('token');
    if (!token) {
      return null;
    }

    try {
      const decodedToken: any = jwtDecode(token); // Giải mã token
      console.log(decodedToken);

      return (
        decodedToken[
          'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'
        ] || null
      );
    } catch (error) {
      console.error('Invalid token:', error);
      return null;
    }
  }

  // Đăng xuất người dùng
  logout(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    this.cookieService.delete('Authentication', '/');
    this.userSubject.next(null);
    console.log('User after logout:', this.userSubject.value);
  }
}
