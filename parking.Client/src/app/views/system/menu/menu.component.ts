import { Component } from '@angular/core';
import { MaterialModule } from '../../modules/material.module';
import { ClockComponent } from "../../page/clock/clock.component";

@Component({
  selector: 'app-menu',
  standalone: true,
  imports: [
    MaterialModule,
    ClockComponent
],
  templateUrl: './menu.component.html',
  styleUrl: './menu.component.scss'
})
export class MenuComponent {
  dateTime: Date;

  constructor() {
    this.dateTime = new Date();
  }

  setDateTimeNow() {
    this.dateTime = new Date();
  }
}
