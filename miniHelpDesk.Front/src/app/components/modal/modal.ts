import { Component, Input } from '@angular/core';

@Component({
  imports: [],
  selector: 'app-modal',
  templateUrl: './modal.html',
})
export class Modal {
  @Input() title = '';

  isOpen = false;

  open(): void {
    this.isOpen = true;
  }

  close(): void {
    this.isOpen = false;
  }

  toggle(): void {
    this.isOpen = !this.isOpen;
  }
}
