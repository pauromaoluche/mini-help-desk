import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';

import { API_BASE_URL } from '../config/api.config';
import { AuthService } from '../services/auth-service';

export const authInterceptor: HttpInterceptorFn = (request, next) => {
  const apiBaseUrl = API_BASE_URL.replace(/\/+$/, '');
  const requestUrl = request.url.split(/[?#]/, 1)[0];

  const isApiRequest = requestUrl === apiBaseUrl || requestUrl.startsWith(`${apiBaseUrl}/`);

  const isLoginRequest = requestUrl === `${apiBaseUrl}/auth`;

  if (!isApiRequest || isLoginRequest) {
    return next(request);
  }

  const authService = inject(AuthService);
  const token = authService.getToken();

  if (!token) {
    return next(request);
  }

  const authenticatedRequest = request.clone({
    setHeaders: {
      Authorization: `Bearer ${token}`,
    },
  });

  return next(authenticatedRequest);
};
