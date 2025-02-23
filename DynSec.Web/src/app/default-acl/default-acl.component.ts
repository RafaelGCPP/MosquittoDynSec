import { Component, inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { RouterLink } from '@angular/router';
import { AppHealthCheckService } from '../app.health.service';
import { DefaultAclsGraphqlService } from './default-acl.graphql';

interface DefaultAcl {
  aclType: string;
  allow: boolean;
}

@Component({
  selector: 'dynsec-default-acl',
  imports: [
    MatTableModule,
    MatButtonModule,
    MatIconModule    
  ],
  templateUrl: './default-acl.component.html',
  styleUrl: './default-acl.component.scss'
})
export class DefaultAclComponent {
  graphql = inject(DefaultAclsGraphqlService);
  defaultAcl: DefaultAcl[] = [];
  displayedColumns: string[] = ['permission', 'allow'];

  private readonly healthCheck = inject(AppHealthCheckService);

  ngOnInit() {
    this.healthCheck.checkBackend(
      (data: string) => {
        this.getDefaultAcl();
      }
    );
  }

  getDefaultAcl() {
    this.graphql.getDefaultAcl().subscribe(
      ({ data, loading }) => {
        this.defaultAcl = data.defaultACL.acLs;
      });
  }

  togglePermission(permission: string) {
    const newAcl = this.defaultAcl.map(
      (acl) => {
        return {
          aclType: acl.aclType,
          allow: (acl.aclType === permission) ? !acl.allow : acl.allow
        }
      }
    );

    this.graphql.setDefaultAcl(newAcl);
  }

  setDefaultAcl() {

  }
}
