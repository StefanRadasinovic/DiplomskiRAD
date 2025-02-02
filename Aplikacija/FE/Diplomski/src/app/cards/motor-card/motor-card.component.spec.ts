import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MotorCardComponent } from './motor-card.component';

describe('MotorCardComponent', () => {
  let component: MotorCardComponent;
  let fixture: ComponentFixture<MotorCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [MotorCardComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MotorCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
