import { Component } from '@angular/core';

import { Header } from '../../components/header/header';
import { Container } from '../../components/container/container';
import { TicketsTable } from '../../components/tickets/tickets-table/tickets-table';

@Component({
  selector: 'app-dashboard',
  imports: [Header, Container, TicketsTable],
  templateUrl: './dashboard.html',
})
export class Dashboard {}
