import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EquipmentContainerComponent } from './equipment-container.component';

describe('EquipmentContainerComponent', () => {
  let component: EquipmentContainerComponent;
  let fixture: ComponentFixture<EquipmentContainerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EquipmentContainerComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EquipmentContainerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
