import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { tap } from 'rxjs';

import { AuthRequest } from '../models/auth/auth-request';
import { AuthResponse } from '../models/auth/auth-response';

import { API_BASE_URL } from '../config/api.config';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private readonly http = inject(HttpClient);

  private readonly apiUrl = `${API_BASE_URL}/auth`;

  private readonly tokenKey = 'auth_token';
  private readonly expiresAtKey = 'auth_expires_at';

  login(request: AuthRequest) {
    return this.http.post<AuthResponse>(this.apiUrl, request).pipe(
      tap((response) => {
        const expiresAt = Date.parse(response.expiresAt);

        if (!response.token || !Number.isFinite(expiresAt) || expiresAt <= Date.now()) {
          throw new Error('A API retornou uma sessão inválida ou expirada.');
        }

        sessionStorage.setItem(this.tokenKey, response.token);
        sessionStorage.setItem(this.expiresAtKey, response.expiresAt);
      }),
    );
  }

  getToken(): string | null {
    if (!this.isAuthenticated()) {
      return null;
    }

    return sessionStorage.getItem(this.tokenKey);
  }

  isAuthenticated(): boolean {
    const token = sessionStorage.getItem(this.tokenKey);
    const expiresAtValue = sessionStorage.getItem(this.expiresAtKey);

    if (!token || !expiresAtValue) {
      return false;
    }

    const expiresAt = Date.parse(expiresAtValue);

    if (!Number.isFinite(expiresAt) || expiresAt <= Date.now()) {
      this.logout();
      return false;
    }

    return true;
  }

  logout(): void {
    sessionStorage.removeItem(this.tokenKey);
    sessionStorage.removeItem(this.expiresAtKey);
  }
}
