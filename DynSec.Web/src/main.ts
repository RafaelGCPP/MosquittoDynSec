import { bootstrapApplication } from '@angular/platform-browser';
import { MainComponent } from './main/main.component';
import { mainConfig } from './main/main.config';

import { appConfig } from './app/app.config';
import { AppComponent } from './app/app.component';

bootstrapApplication(AppComponent, appConfig)
  .catch((err) => console.error(err));

//bootstrapApplication(MainComponent, mainConfig)
//  .catch((err) => console.error(err));
