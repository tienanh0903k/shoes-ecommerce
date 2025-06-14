import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddSizeComponent } from './add-size.component';

describe('AddSizeComponent', () => {
  let component: AddSizeComponent;
  let fixture: ComponentFixture<AddSizeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddSizeComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddSizeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
