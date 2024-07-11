import { Component, EventEmitter, Input, Output } from '@angular/core';
import { String } from "../../../shared/types/String";
import { MaterialModule } from '../../views/modules/material.module';

@Component({
  selector: 'app-search-input',
  standalone: true,
  templateUrl: './search-input.component.html',
  styleUrls: ['./search-input.component.scss'],
  imports: [
    MaterialModule,
  ],
})
export class SearchInputComponent {
  value = 'Clear me';
  searchQuery: string = '';

  @Input() autoSearchEnabled: boolean = false;
  @Input() disabled: boolean = false;
  @Output() searchedEvent: EventEmitter<string> = new EventEmitter();

  queryChanged($event: string) {
    if(this.autoSearchEnabled)
      this.searchedEvent.emit($event);
  }

  hasValue(): boolean {
    return String.isNotNullOrEmpty(this.searchQuery);
  }
}
