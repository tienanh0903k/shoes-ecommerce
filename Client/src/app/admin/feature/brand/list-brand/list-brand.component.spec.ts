import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListBrandComponent } from './list-brand.component';

describe('ListBrandComponent', () => {
  let component: ListBrandComponent;
  let fixture: ComponentFixture<ListBrandComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ListBrandComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ListBrandComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
