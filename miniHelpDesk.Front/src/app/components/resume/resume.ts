import { NgClass } from '@angular/common';
import { Component, Input } from '@angular/core';

@Component({
  imports: [NgClass],
  selector: 'app-resume',
  templateUrl: './resume.html',
})
export class Resume {
  @Input() label: string = '';
  @Input() value: number = 0;
  @Input() color = '';
}
