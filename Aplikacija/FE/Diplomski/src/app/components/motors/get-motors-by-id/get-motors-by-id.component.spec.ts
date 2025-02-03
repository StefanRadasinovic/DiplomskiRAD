import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GetMotorsByIdComponent } from './get-motors-by-id.component';

describe('GetMotorsByIdComponent', () => {
  let component: GetMotorsByIdComponent;
  let fixture: ComponentFixture<GetMotorsByIdComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [GetMotorsByIdComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GetMotorsByIdComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
