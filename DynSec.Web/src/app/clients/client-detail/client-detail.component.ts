import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NavBarService } from '../../navbar/navbar.service';
import { ClientsGraphqlService } from '../clients.graphql.service';

@Component({
  selector: 'dynsec-client-detail',
  imports: [],
  templateUrl: './client-detail.component.html',
  styleUrl: './client-detail.component.scss'
})
export class ClientDetailComponent {

  userName = '';
  data: any;
  strdata = '';
  constructor(
    private readonly route: ActivatedRoute,
    private readonly navBar: NavBarService,
    private readonly graphql: ClientsGraphqlService
  )
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

    this.graphql.getClient(this.userName).subscribe(result => {
      this.data = result.data;
      this.strdata = JSON.stringify(result.data);
    });
  }

}
