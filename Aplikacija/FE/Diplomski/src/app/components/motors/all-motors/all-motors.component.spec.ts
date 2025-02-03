import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AllMotorsComponent } from './all-motors.component';

describe('AllMotorsComponent', () => {
  let component: AllMotorsComponent;
  let fixture: ComponentFixture<AllMotorsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AllMotorsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AllMotorsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
