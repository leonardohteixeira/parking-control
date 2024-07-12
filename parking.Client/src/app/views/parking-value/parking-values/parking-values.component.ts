import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { ParkingValues } from '../../../models/ParkingValues';
import { MaterialModule } from '../../modules/material.module';
import { DefaultViewComponent } from "../../system/default-view/default-view.component";
import { environment } from '../../../../environment';
import { Guid } from '../../../../shared/types/Guid';

@Component({
  selector: 'app-parking-values',
  standalone: true,
  imports: [DefaultViewComponent, MaterialModule],
  templateUrl: './parking-values.component.html',
  styleUrl: './parking-values.component.scss'
})
export class ParkingValuesComponent {
  values: ParkingValues = {
    parkingValuesId: Guid.NewGuid(),
    halfInTheFirstHour: false,
    hourlyTolerance: 10,
    value: 0
  }

  constructor(private http: HttpClient) {
    http.get<ParkingValues>(`${environment.apiUrl}/parking-values/get-values`).subscribe(values => {
      if (values)
        this.values = values;
    });
  }

  save() {
    this.http.post(`${environment.apiUrl}/parking-values/save-values`, this.values).subscribe(() => {

    });
  }
}
