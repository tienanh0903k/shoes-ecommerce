import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListBlogComponent } from './list-blog.component';

describe('ListBlogComponent', () => {
  let component: ListBlogComponent;
  let fixture: ComponentFixture<ListBlogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ListBlogComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ListBlogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
