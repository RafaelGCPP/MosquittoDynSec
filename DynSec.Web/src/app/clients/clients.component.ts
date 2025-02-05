import { Component, ElementRef, ViewChild } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatSidenav, MatSidenavModule } from '@angular/material/sidenav';
import { NavBarService } from '../navbar/navbar.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'dynsec-clients',
  imports: [MatSidenavModule, MatButtonModule],
  templateUrl: './clients.component.html',
  styleUrl: './clients.component.scss'
})
export class ClientsComponent {
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
