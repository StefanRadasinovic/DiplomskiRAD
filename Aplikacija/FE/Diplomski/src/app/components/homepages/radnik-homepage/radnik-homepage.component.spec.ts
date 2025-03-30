import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RadnikHomepageComponent } from './radnik-homepage.component';

describe('RadnikHomepageComponent', () => {
  let component: RadnikHomepageComponent;
  let fixture: ComponentFixture<RadnikHomepageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [RadnikHomepageComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RadnikHomepageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
