import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { MatSnackBar } from "@angular/material/snack-bar";
import { retry } from "rxjs";

export type HealthyCallback = (data: string) => void;

@Injectable({ providedIn: 'root', })
export class AppHealthCheckService {
  constructor(
    private readonly http: HttpClient,
    private readonly snack: MatSnackBar,
  ) {
  }

  checkBackend(healthyCallback: HealthyCallback) {
    this.http.get('/health', { responseType: "text" })
      .pipe(
        retry({
          count: 5, delay: 1000
        }),
      )
      .subscribe(
        {
          next: healthyCallback,
          error: (error: any) => {
            console.error("Backend not available: " + error)
            this.snack.open("Backend not available.", "Close", {
              duration: 5000
            });
          },
          complete: () => {
            console.log("Backend healthy!");
          }
        }
      );

  }
}
