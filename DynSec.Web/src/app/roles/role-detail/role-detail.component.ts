import { Component } from '@angular/core';
import { RolesGraphqlService } from '../roles.graphql.service';
import { ActivatedRoute } from '@angular/router';
import { NavBarService } from '../../navbar/navbar.service';
import { FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { Role } from '../../model/role';
import { Subscription } from 'rxjs';

@Component({
  selector: 'dynsec-role-detail',
  imports: [
    FormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatSlideToggleModule
  ],
  templateUrl: './role-detail.component.html',
  styleUrl: './role-detail.component.scss'
})
export class RoleDetailComponent {
  roleName = '';
  role: Role = {
    roleName: '',
    textName: '',
    textDescription: '',
    acLs:[]
  }
  private paramSubscription!: Subscription;
  private querySubscription!: Subscription;

  constructor(
    private readonly route: ActivatedRoute,
    private readonly navBar: NavBarService,
    private readonly graphql: RolesGraphqlService
  ) {
  }

  ngOnInit() {
    this.paramSubscription = this.route.paramMap.subscribe(params => {
      let roleName = params.get('roleName');
      if (roleName) {
        this.roleName = roleName;
        this.navBar.closeSidenav();
      }
    });

    this.querySubscription = this.graphql.getRole(this.roleName).subscribe(result => {
      this.role = this.normalizeRole(result.data.role.role);
      console.log(this.role);
    });
  }

  private normalizeRole(role: any):Role {
    return {
      ...role,
    };
  }

  ngOnDestroy() {
    this.querySubscription.unsubscribe();
    this.paramSubscription.unsubscribe();
  }
}
