import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TrackingOrderComponent } from './tracking-order.component';

describe('TrackingOrderComponent', () => {
  let component: TrackingOrderComponent;
  let fixture: ComponentFixture<TrackingOrderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TrackingOrderComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TrackingOrderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
