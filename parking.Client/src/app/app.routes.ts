import { Routes } from '@angular/router';
import { ParkingsComponent } from './views/parking/parkings/parkings.component';
import { ParkingValuesComponent } from './views/parking-value/parking-values/parking-values.component';

export const routes: Routes = [
  {
    path: '',
    redirectTo: '/parkings',
    pathMatch: 'full',
  },
  {
    path: 'parkings',
    component: ParkingsComponent,
  },
  {
    path: 'parking-values',
    component: ParkingValuesComponent,
  }
];
