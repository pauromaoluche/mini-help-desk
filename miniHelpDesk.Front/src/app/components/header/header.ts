import { Component } from '@angular/core';
import { Container } from '../container/container';

@Component({
  imports: [Container],
  selector: 'app-header',
  templateUrl: './header.html',
})
export class Header {}
