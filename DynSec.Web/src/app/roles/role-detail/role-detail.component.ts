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
import { ApolloError } from '@apollo/client/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialog, MatDialogActions } from '@angular/material/dialog';
import { DeleteDialog } from '../../delete-dialog/delete-dialog';

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
    AclListComponent,
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
    acLs: []
  }
  selectedAcls: Acl[] = [];

  private paramSubscription!: Subscription;
  private querySubscription!: Subscription;

  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);
  private readonly navBar = inject(NavBarService);
  private readonly graphql = inject(RolesGraphqlService);
  private readonly snack = inject(MatSnackBar);
  private readonly dialog = inject(MatDialog);

  mode = 'edit';

  defaultAction = {
    next: console.log,
    error: this.displayError
  }

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

  private normalizeRole(role: any): Role {
    return {
      ...role,
    };
  }

  updateAcls() {
    this.role.acLs = this.selectedAcls.map
      (acl => {
        return {
          aclType: acl.aclType,
          topic: acl.topic,
          allow: acl.allow,
          priority: acl.priority
        };
      });
  }

  saveRole() {
    if (this.mode === 'edit') {
      this.role = {
        roleName: this.roleName,
        textDescription: this.role.textDescription,
        textName: this.role.textName,
        acLs: this.selectedAcls.map
          (acl => {
            return {
              aclType: acl.aclType,
              topic: acl.topic,
              allow: acl.allow,
              priority: acl.priority
            };
          }),
      };

      this.graphql.updateRole(this.role, this.defaultAction);
    } else {
      const actions = {
        next: (data: any) => {
          console.log(data);
          this.router.navigate([`../${this.role.roleName}`], { relativeTo: this.route });
        },
        error: (error: ApolloError) => {
          console.error(error);
          console.error('Mutation error: ', error.message);
          this.snack.open(`${error.message}`, "Close", {
            duration: 15000
          });

        }
      };
      this.graphql.createRole(this.role, actions);
    }

  }

  deleteRole() {
    const dialogRef = this.dialog.open(DeleteDialog, {
      data: { name: this.roleName, itemType: 'role' },
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result === 'confirm') {
        const action = {
          next: (data: any) => {
            console.log(data);
            this.router.navigateByUrl('/roles');
            this.graphql.refresh();
            this.navBar.openSidenav();
          },
          error: this.displayError
        }
        this.graphql.deleteRole (this.roleName, action);
      }
    });
  }


  displayError(error: ApolloError) {
    console.error(error);
    console.error('Mutation error: ', error.message);
    this.snack.open(`${error.message}`, "Close", {
      duration: 15000
    });
  }


  ngOnDestroy() {
    if (this.mode === 'edit') {
      this.querySubscription.unsubscribe();
    }
    this.paramSubscription.unsubscribe();
  }
}
