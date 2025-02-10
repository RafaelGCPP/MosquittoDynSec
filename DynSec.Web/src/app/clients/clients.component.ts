import { Component, ViewChild } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatSidenav, MatSidenavModule } from '@angular/material/sidenav';
import { Subscription } from 'rxjs';
import { ClientsListComponent } from '../clients-list/clients-list.component';
import { NavBarService } from '../navbar/navbar.service';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'dynsec-clients',
  imports: [RouterOutlet, MatSidenavModule, MatButtonModule, ClientsListComponent],
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
