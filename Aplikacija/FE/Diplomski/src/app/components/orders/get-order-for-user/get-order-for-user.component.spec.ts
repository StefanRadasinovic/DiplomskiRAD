import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GetOrderForUserComponent } from './get-order-for-user.component';

describe('GetOrderForUserComponent', () => {
  let component: GetOrderForUserComponent;
  let fixture: ComponentFixture<GetOrderForUserComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [GetOrderForUserComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GetOrderForUserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
