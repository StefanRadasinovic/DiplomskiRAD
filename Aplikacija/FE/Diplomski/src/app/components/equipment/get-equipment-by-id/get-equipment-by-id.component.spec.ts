import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GetEquipmentByIdComponent } from './get-equipment-by-id.component';

describe('GetEquipmentByIdComponent', () => {
  let component: GetEquipmentByIdComponent;
  let fixture: ComponentFixture<GetEquipmentByIdComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [GetEquipmentByIdComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GetEquipmentByIdComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
