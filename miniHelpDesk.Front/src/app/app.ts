import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Header } from './components/header/header';
import { TicketsTable } from './components/tickets/tickets-table/tickets-table';
import { Container } from './components/container/container';

@Component({
  imports: [RouterOutlet, Header, TicketsTable, Container],
  selector: 'app-root',
  styleUrl: './app.css',
  templateUrl: './app.html',
})
export class App {
  protected readonly title = signal('miniHelpDesk.Front');
}
