import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MotorContainerComponent } from './motor-container.component';

describe('MotorContainerComponent', () => {
  let component: MotorContainerComponent;
  let fixture: ComponentFixture<MotorContainerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [MotorContainerComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MotorContainerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
