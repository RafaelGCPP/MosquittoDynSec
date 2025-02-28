import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { Router } from '@angular/router';
import { timer } from 'rxjs';

const POLL_INTERVAL_MS = 30000;


@Injectable({
  providedIn: 'root'
})
export class AuthService {

  isAuthenticated = signal(false);
  router = inject(Router);
  private readonly http = inject(HttpClient);
  private readonly polling: any;
  data: any;

  constructor() {
    this.polling = timer(0, POLL_INTERVAL_MS).subscribe((value: number) => {
      console.log("tick %d", value);
      this.checkAuthentication();
    });
  }

  checkAuthentication() {
    this.http.get('/api/security/check-session').subscribe({
      next: (response: any) => {

        this.data = response;
        this.isAuthenticated.set(true);
        console.log("Authenticated");
        console.log(response);
      },
      error: (error: any) => {
        this.data = null;
        this.isAuthenticated.set(false);
        console.log("Not Authenticated");
        console.log(error);
      }
    });
  }

  login() {
    window.location.href = '/api/security/login';
  }

  logout() {
    this.http.post('/api/security/logout', null, { }).subscribe({
      next: (response: any) => {
        this.data = null;
        this.isAuthenticated.set(false);
        console.log("Logged out");
        console.log(response);
      },
      error: (error: any) => {
        this.isAuthenticated.set(false);
        console.log("error");
        console.log(error);
      }
    });  }

  ngOnDestroy() {
    this.polling.unsubscribe();
  }
}
