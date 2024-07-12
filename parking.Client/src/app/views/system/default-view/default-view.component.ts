import { Component } from '@angular/core';
import { MaterialModule } from '../../modules/material.module';
import { RouterLink } from '@angular/router';
import { ClockComponent } from "../../page/clock/clock.component";

@Component({
  selector: 'app-default-view',
  standalone: true,
  imports: [
    MaterialModule,
    RouterLink,
    ClockComponent
],
  templateUrl: './default-view.component.html',
  styleUrl: './default-view.component.scss'
})
export class DefaultViewComponent {

}
