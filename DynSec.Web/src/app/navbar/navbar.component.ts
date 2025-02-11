import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { Subscription } from 'rxjs';
import { NavBarService } from './navbar.service';
import { RouterLink } from '@angular/router';


@Component({
  selector: 'dynsec-navbar',
  imports: [MatButtonModule, MatIconModule, RouterLink],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss',
})
export class NavbarComponent {
  svcSubscription!: Subscription;

  constructor(private readonly navbarSvc: NavBarService) {
  }

  ngOnInit() {
    this.svcSubscription=this.navbarSvc.showSidenav.subscribe(show => {
      console.log("Show Sidenav: ", show);
    });
  }

  toggleSidenav() {
    this.navbarSvc.toggleSidenav();
  }

  ngOnDestroy() {
    this.svcSubscription.unsubscribe();
  }

}
