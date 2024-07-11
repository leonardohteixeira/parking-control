import { Routes } from '@angular/router';
import { PageComponent } from './views/page/page/page.component';

export const routes: Routes = [
  {
    path: '',
    redirectTo: '/home',
    pathMatch: 'full',
  },
  {
    path: 'home',
    component: PageComponent,
  }
];
