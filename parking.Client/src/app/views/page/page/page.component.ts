import { Component } from '@angular/core';
import { MenuComponent } from "../../system/menu/menu.component";
import { TableComponent } from "../../system/table/table.component";

@Component({
  selector: 'app-page',
  standalone: true,
  imports: [MenuComponent, TableComponent],
  templateUrl: './page.component.html',
  styleUrl: './page.component.scss'
})
export class PageComponent {

}
