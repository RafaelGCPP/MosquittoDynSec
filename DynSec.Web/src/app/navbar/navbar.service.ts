import { Injectable } from "@angular/core";
import { MatSidenav } from "@angular/material/sidenav";
import { Observable } from "@apollo/client/utilities";
import { Subject, Subscription } from "rxjs";

@Injectable({ providedIn: 'root', })
export class NavBarService {

  private _showSidenav: boolean = false;
  private showSidenavSubject = new Subject<boolean>();

  toggleSidenav() {
    this._showSidenav = !this._showSidenav;
    this.showSidenavSubject.next(this._showSidenav);
  }

  openSidenav() {
    this._showSidenav = true;
    this.showSidenavSubject.next(this._showSidenav);
  }

  closeSidenav() {
    this._showSidenav = false;
    this.showSidenavSubject.next(this._showSidenav);
  }

  get showSidenav() {
    return this.showSidenavSubject.asObservable();
  }

  subscribe(sidenav: MatSidenav): Subscription {
    return this.showSidenav.subscribe(show => {
      if (show) {
        sidenav.open();
      } else {
        sidenav.close();
      }
    });
  }
}
