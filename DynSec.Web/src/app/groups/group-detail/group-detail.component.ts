import { Component } from '@angular/core';
import { GroupsGraphqlService } from '../groups.graphql.service';
import { ActivatedRoute } from '@angular/router';
import { NavBarService } from '../../navbar/navbar.service';
import { FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { Group } from '../../model/group';
import { Subscription } from 'rxjs';

@Component({
  selector: 'dynsec-group-detail',
  imports: [
    FormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatSlideToggleModule
  ],
  templateUrl: './group-detail.component.html',
  styleUrl: './group-detail.component.scss'
})
export class GroupDetailComponent {
  groupName = '';
  group: Group = {
    groupName: '',
    textName: '',
    textDescription: '',
  }
  private paramSubscription!: Subscription;
  private querySubscription!: Subscription;

  constructor(
    private readonly route: ActivatedRoute,
    private readonly navBar: NavBarService,
    private readonly graphql: GroupsGraphqlService
  ) {
  }

  ngOnInit() {
    this.paramSubscription = this.route.paramMap.subscribe(params => {
      let groupName = params.get('groupName');
      if (groupName) {
        this.groupName = groupName;
        this.navBar.closeSidenav();
      }
    });

    this.querySubscription = this.graphql.getGroup(this.groupName).subscribe(result => {
      this.group = this.normalizeGroup(result.data.group.group);
      console.log(this.group);
    });
  }

  private normalizeGroup(group: any):Group {
    return {
      ...group,
    };
  }

  ngOnDestroy() {
    this.querySubscription.unsubscribe();
    this.paramSubscription.unsubscribe();
  }
}
