import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddMotorsComponent } from './add-motors.component';

describe('AddMotorsComponent', () => {
  let component: AddMotorsComponent;
  let fixture: ComponentFixture<AddMotorsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AddMotorsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddMotorsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
