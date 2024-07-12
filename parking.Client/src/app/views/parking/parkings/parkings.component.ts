import { HttpClient } from '@angular/common/http';
import { Component, ViewChild } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { environment } from '../../../../environment';
import { Guid } from '../../../../shared/types/Guid';
import { IParkings } from '../../../models/IParkings';
import { MaterialModule } from '../../modules/material.module';
import { DefaultViewComponent } from "../../system/default-view/default-view.component";

@Component({
  selector: 'app-parkings',
  standalone: true,
  imports: [
    MaterialModule,
    DefaultViewComponent
],
  templateUrl: './parkings.component.html',
  styleUrl: './parkings.component.scss'
})
export class ParkingsComponent {

  displayedColumns: string[] = ['plate', 'arrivalTime', 'departureTime', 'totalValue', 'exit', 'edit', 'delete'];
  dataSource: MatTableDataSource<IParkings>;

  currentItem: IParkings | undefined;

  @ViewChild(MatSort) sort!: MatSort;

  constructor(private http: HttpClient) {
    this.dataSource = new MatTableDataSource<IParkings>([]);
  }

  ngAfterViewInit() {
    this.dataSource.sort = this.sort;
    this.loadData();
  }

  exitParking(parking: IParkings) {
    this.currentItem = undefined;
    parking.departureTime = new Date();
    this.save(parking);
  }

  loadData() {
    this.http.get<IParkings[]>(`${environment.apiUrl}/parkings/get-items`).subscribe(items => {
      this.dataSource.data = items;
    });
  }

  create() {
    let newItem: IParkings = {
      parkingsId: Guid.NewGuid(),
      arrivalTime: new Date()
    };

    this.save(newItem);
  }

  edit(id: string) {
    this.currentItem = this.dataSource.data.find(i => i.parkingsId == id);
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  deleteRow(parkingsId: string) {
    this.http.delete(`${environment.apiUrl}/parkings/delete/${parkingsId}`).subscribe(() => {
      this.currentItem = undefined;
      this.dataSource.data = this.dataSource.data.filter(item => item.parkingsId !== parkingsId);
      this.loadData();
    }, error => {
      console.error('Erro ao excluir o item:', error);
    });
  }

  save(item: IParkings) {
    this.http.post(`${environment.apiUrl}/parkings/save`, item).subscribe((saved:IParkings) => {
      if (this.currentItem?.parkingsId == saved.parkingsId)
        this.currentItem = saved;
      this.loadData();
    });
  }
}
