import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { firstValueFrom, interval, Subscription } from 'rxjs';

const POLL_INTERVAL_MS = 30000;


@Injectable({
  providedIn: 'root'
})
export class AuthService {

  isAuthenticated = signal(false);
  getUserName = signal<string | null>(null);
  activatedRoute = inject(ActivatedRoute);
  router = inject(Router);
  private readonly http = inject(HttpClient);
  private readonly polling: Subscription;
  data: any;

  constructor() {
    this.polling = interval(POLL_INTERVAL_MS).subscribe((value: number) => {
      console.log("tick %d", value);
      this.checkAuthentication();
    });
    firstValueFrom(this.http.get('/api/security/check-session'))
      .then((response: any) => {
        this.setAuthenticated(response);
      })
      .catch((error: any) => {
        this.clearAuthenticated();
        console.error(error);
      });
  }

  private setAuthenticated(data: any) {
    this.data = data;
    this.isAuthenticated.set(true);
    this.getUserName.set(data.name);
  }

  private clearAuthenticated() {
    this.data = null;
    this.isAuthenticated.set(false);
    this.getUserName.set(null);
  }

  checkAuthentication() {
    this.http.get('/api/security/check-session').subscribe({
      next: (response: any) => {
        this.setAuthenticated(response);
      },
      error: (error: any) => {
        this.clearAuthenticated();
        console.error(error);
      }
    });
  }

  login(returnUrl?: string) {
    if (!returnUrl) {
      returnUrl = encodeURI('~/' + this.activatedRoute.snapshot.url.join('/'));
    }
    window.location.href = `/api/security/login?returnUrl=${returnUrl}`;
  }

  logout() {
    this.http.post('/api/security/logout', null, {}).subscribe({
      next: (response: any) => {
        this.clearAuthenticated();
        this.router.navigate(['/']);
      },
      error: (error: any) => {
        this.clearAuthenticated();
        this.router.navigate(['/']);
        console.error(error);
      }
    });
  }

  ngOnDestroy() {
    this.polling.unsubscribe();

  }
}
