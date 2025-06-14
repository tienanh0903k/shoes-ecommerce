import { inject } from '@angular/core';
import {
  CanActivateFn,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  Router,
} from '@angular/router';
import { jwtDecode } from 'jwt-decode';
import { CookieService } from 'ngx-cookie-service';
import { AuthService } from '../services/auth.service';

export const authGuard: CanActivateFn = (route, state) => {
  const cookieService = inject(CookieService);
  const authService = inject(AuthService);
  const router = inject(Router);

  const user = authService.getUser();
  let token = cookieService.get('Authentication');

  if (user && token) {
    token = token.replace('Bearer ', '');

    try {
      const decodeToken: any = jwtDecode(token);

      // Kiểm tra ngày hết hạn của token
      const expirationDate = decodeToken.exp * 1000;
      const currentTime = new Date().getTime();

      if (expirationDate < currentTime) {
        authService.logout();
        return router.createUrlTree(['/login'], {
          queryParams: { returnUrl: state.url },
        });
      }

      // Kiểm tra quyền (role) từ token
      const userRole =
        decodeToken[
          'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'
        ];
      const allowedRoles = route.data['roles'];

      if (allowedRoles && !allowedRoles.includes(userRole)) {
        // Người dùng không có quyền truy cập
        return router.createUrlTree(['/']);
      }

      // Token hợp lệ và quyền phù hợp
      return true;
    } catch (error) {
      console.error('Invalid token:', error);
      authService.logout();
      return router.createUrlTree(['/login'], {
        queryParams: { returnUrl: state.url },
      });
    }
  }

  authService.logout();
  return router.createUrlTree(['/login'], {
    queryParams: { returnUrl: state.url },
  });
};
