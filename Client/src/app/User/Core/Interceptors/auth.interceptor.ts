import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const authToken = inject(CookieService).get('Authentication');

  const authReq = req.clone({
    setHeaders: {
      Authorization: authToken,
    },
  });

  return next(authReq);
};
