import { Component, ViewChild } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatSidenav, MatSidenavModule } from '@angular/material/sidenav';
import { RouterOutlet } from '@angular/router';
import { Subscription } from 'rxjs';
import { GroupsListComponent } from './groups-list/groups-list.component';
import { NavBarService } from '../navbar/navbar.service';

@Component({
  selector: 'dynsec-groups',
  imports: [
    RouterOutlet,
    MatSidenavModule,
    MatButtonModule,
    GroupsListComponent
  ],
  templateUrl: './groups.component.html',
  styleUrl: './groups.component.scss'
})
export class GroupsComponent {
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
