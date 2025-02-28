import { Component, inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';
import { RouterLink } from '@angular/router';
import { Subscription } from 'rxjs';
import { AuthService } from '../security/auth.service';
import { NavBarService } from './navbar.service';


@Component({
  selector: 'dynsec-navbar',
  imports: [
    MatButtonModule,
    MatIconModule,
    RouterLink,
    MatToolbarModule,
  ],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss',
})
export class NavbarComponent {
  svcSubscription!: Subscription;
  private readonly navbarSvc = inject(NavBarService);
  readonly authService = inject(AuthService);


  ngOnInit() {
    this.svcSubscription = this.navbarSvc.showSidenav.subscribe(show => {
      console.log("Show Sidenav: ", show);
    });
  }

  toggleSidenav() {
    this.navbarSvc.toggleSidenav();
  }

  openSidenav() {
    this.navbarSvc.openSidenav();
  }

  closeSidenav() {
    this.navbarSvc.closeSidenav();
  }

  ngOnDestroy() {
    this.svcSubscription.unsubscribe();
  }

}
