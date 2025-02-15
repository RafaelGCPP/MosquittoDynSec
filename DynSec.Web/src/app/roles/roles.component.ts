import { Component, ViewChild } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatSidenav, MatSidenavModule } from '@angular/material/sidenav';
import { RouterOutlet } from '@angular/router';
import { Subscription } from 'rxjs';
import { RolesListComponent } from './roles-list/roles-list.component';
import { NavBarService } from '../navbar/navbar.service';

@Component({
  selector: 'dynsec-roles',
  imports: [
    RouterOutlet,
    MatSidenavModule,
    MatButtonModule,
    RolesListComponent
  ],
  templateUrl: './roles.component.html',
  styleUrl: './roles.component.scss'
})
export class RolesComponent {
  showFiller = false;
  navbarSubscription!: Subscription;
  @ViewChild('drawer') sidenav!: MatSidenav;

  constructor(private readonly navbarSvc: NavBarService) { }

  ngAfterViewInit() {
    this.navbarSubscription = this.navbarSvc.subscribe(this.sidenav);
    this.navbarSvc.openSidenav();
  }

  ngOnDestroy() {
    this.navbarSubscription.unsubscribe();
  }
}
