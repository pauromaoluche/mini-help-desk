import { ComponentFixture, TestBed } from '@angular/core/testing';
import { Subject } from 'rxjs';
import { Dashboard } from './dashboard';
import { TicketService } from '../../services/ticket-service';
import { TicketSummaryResponse } from '../../models/response/ticket-summary-response';

describe('Dashboard', () => {
  let component: Dashboard;
  let fixture: ComponentFixture<Dashboard>;
  let ticketsSubject: Subject<TicketSummaryResponse[]>;

  beforeEach(async () => {
    ticketsSubject = new Subject<TicketSummaryResponse[]>();

    await TestBed.configureTestingModule({
      imports: [Dashboard],
      providers: [
        {
          provide: TicketService,
          useValue: { getTickets: () => ticketsSubject.asObservable() },
        },
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(Dashboard);
    component = fixture.componentInstance;
    fixture.detectChanges();
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('renders tickets when the API response arrives', async () => {
    ticketsSubject.next([
      {
        id: 1,
        title: 'Printer',
        description: 'Not printing',
        category: 0,
        status: 0,
        priority: 2,
        sectorId: 1,
        createdAt: '2026-01-01T00:00:00Z',
      },
    ]);

    await fixture.whenStable();

    const rows: NodeListOf<HTMLTableRowElement> = fixture.nativeElement.querySelectorAll('tbody tr');
    expect(rows.length).toBe(1);
    expect(rows[0].textContent).toContain('Printer');
  });
});
