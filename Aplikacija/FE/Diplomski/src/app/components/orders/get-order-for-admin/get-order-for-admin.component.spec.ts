import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GetOrderForAdminComponent } from './get-order-for-admin.component';

describe('GetOrderForAdminComponent', () => {
  let component: GetOrderForAdminComponent;
  let fixture: ComponentFixture<GetOrderForAdminComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [GetOrderForAdminComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GetOrderForAdminComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
