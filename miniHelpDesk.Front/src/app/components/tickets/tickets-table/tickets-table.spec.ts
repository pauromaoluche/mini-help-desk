import { ComponentFixture, TestBed } from '@angular/core/testing';
import { TicketsTable } from './tickets-table';

describe('TicketsTable', () => {
  let component: TicketsTable;
  let fixture: ComponentFixture<TicketsTable>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TicketsTable],
    }).compileComponents();

    fixture = TestBed.createComponent(TicketsTable);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('displays API ticket summaries without inventing progress', async () => {
    fixture.componentRef.setInput('tickets', [
      {
        id: 1,
        title: 'Printer',
        description: 'Not printing',
        category: 0,
        priority: 2,
        status: 3,
        sectorId: 1,
        assignedToId: 42,
        createdAt: '2026-01-01T00:00:00Z',
      },
      {
        id: 2,
        title: 'Network',
        description: 'Disconnected',
        category: 0,
        priority: 0,
        status: 0,
        sectorId: 1,
        assignedToId: 0,
        createdAt: '2026-01-01T00:00:00Z',
      },
    ]);
    await fixture.whenStable();

    const rows: NodeListOf<HTMLTableRowElement> = fixture.nativeElement.querySelectorAll('tbody tr');
    expect(rows.length).toBe(2);
    expect(rows[0].cells[1].textContent).toContain('High');
    expect(rows[0].cells[2].textContent).toContain('InProgress');
    expect(rows[0].cells[3].textContent).toContain('#42');
    expect(rows[0].cells[4].textContent).toContain('—');
    expect(rows[1].cells[3].textContent).toContain('Sem responsável');
  });
});
