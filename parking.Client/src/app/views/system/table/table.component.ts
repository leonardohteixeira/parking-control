import { HttpClient, HttpClientModule } from '@angular/common/http';
import { AfterViewInit, Component, Injectable, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Observable } from 'rxjs';
import { environment } from '../../../../environment';
import { MaterialModule } from '../../modules/material.module';
import { IParking } from './../../../models/IParking';

@Component({
  selector: 'app-table',
  standalone: true,
  imports: [
    MaterialModule,
    HttpClientModule
],
  templateUrl: './table.component.html',
  styleUrl: './table.component.scss'
})

@Injectable({
  providedIn: 'root'
})
export class TableComponent implements AfterViewInit{
apiUrl = environment.apiUrl;
displayedColumns: string[] = ['plate', 'arrivalTime', 'departureTime', 'duration', 'billedTime', 'price', 'amountDue'];
dataSource: MatTableDataSource<IParking>;

@ViewChild(MatPaginator) paginator!: MatPaginator;
@ViewChild(MatSort) sort!: MatSort;

  constructor(private http: HttpClient) {
    this.dataSource = new MatTableDataSource<IParking>([]);
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
    this.loadData();
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  getDatas(): Observable<IParking[]>{
    return this.http.get<IParking[]>(`${this.apiUrl}/parking/get-items`);
  }

  loadData() {
    this.getDatas().subscribe((data: IParking[]) => {
      this.dataSource.data = data;
    }, error => {
      console.error('Erro ao carregar dados:', error);
    });
  }
}

