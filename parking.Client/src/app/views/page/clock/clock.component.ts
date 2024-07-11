import { CommonModule, registerLocaleData } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import localePt from '@angular/common/locales/pt';

registerLocaleData(localePt);

@Component({
  selector: 'app-clock',
  standalone: true,
  imports: [
    CommonModule,
  ],
  templateUrl: './clock.component.html',
  styleUrl: './clock.component.scss'
})
export class ClockComponent implements OnInit{
  currentTime: Date = new Date();

  constructor() {}

  ngOnInit(): void {
    this.updateTime();
  }

  updateTime(): void {
    setInterval(() => {
      this.currentTime = new Date();
    }, 1000);
  }
}
