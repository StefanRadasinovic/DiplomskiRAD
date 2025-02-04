import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DirectorHomepageComponent } from './director-homepage.component';

describe('DirectorHomepageComponent', () => {
  let component: DirectorHomepageComponent;
  let fixture: ComponentFixture<DirectorHomepageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DirectorHomepageComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DirectorHomepageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
