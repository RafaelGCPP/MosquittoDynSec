import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatTooltipModule } from '@angular/material/tooltip';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { Acl } from '../../model/acl';
import { Role } from '../../model/role';
import { NavBarService } from '../../navbar/navbar.service';
import { RolesGraphqlService } from '../roles.graphql.service';
import { AclListComponent } from '../acl-list/acl-list.component';

@Component({
  selector: 'dynsec-role-detail',
  imports: [
    FormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatSlideToggleModule,
    MatButtonModule,
    MatIconModule,
    MatTooltipModule,
    AclListComponent
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
  selectedAcls: Acl[] = [];

  private paramSubscription!: Subscription;
  private querySubscription!: Subscription;

  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);
  private readonly navBar = inject(NavBarService);
  private readonly graphql = inject(RolesGraphqlService);

  mode = 'edit';


  ngOnInit() {
    this.paramSubscription = this.route.paramMap.subscribe(params => {
      let roleName = params.get('roleName');
      this.updateView(roleName);
      console.log("Loading role detail for " + roleName);

    });
  }

  private updateView(roleName: string | null) {

    if (roleName) {
      this.roleName = roleName;
    }

    if (this.roleName === '') {
      this.mode = 'new';
    } else {
      this.mode = 'edit';
      this.querySubscription = this.graphql.getRole(this.roleName).subscribe(result => {
        this.role = this.normalizeRole(result.data.role.role);
        console.log("Loading details for role " + this.roleName);
        console.log(this.role);
        if (this.role.acLs) {
          this.selectedAcls = [
            ...this.role.acLs
          ]
        }
      });
    }
  }

  private normalizeRole(role: any):Role {
    return {
      ...role,
    };
  }

  saveRole() {

  }

  deleteRole() {
  }

  ngOnDestroy() {
    if (this.mode === 'edit') {
      this.querySubscription.unsubscribe();
    }
    this.paramSubscription.unsubscribe();
  }
}
