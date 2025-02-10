import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NavBarService } from '../../navbar/navbar.service';

@Component({
  selector: 'dynsec-client-detail',
  imports: [],
  templateUrl: './client-detail.component.html',
  styleUrl: './client-detail.component.scss'
})
export class ClientDetailComponent {

  userName = '';
  constructor(private route: ActivatedRoute, private navBar: NavBarService)
  {
    
  }

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      let userName = params.get('userName');
      if (userName) {
        this.userName = userName;
        this.navBar.closeSidenav();
      }
    });
  }

}
