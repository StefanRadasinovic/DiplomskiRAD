import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdjustPricesComponent } from './adjust-prices.component';

describe('AdjustPricesComponent', () => {
  let component: AdjustPricesComponent;
  let fixture: ComponentFixture<AdjustPricesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AdjustPricesComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdjustPricesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
