import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AllServicesAdminComponent } from './all-services-admin.component';

describe('AllServicesAdminComponent', () => {
  let component: AllServicesAdminComponent;
  let fixture: ComponentFixture<AllServicesAdminComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AllServicesAdminComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AllServicesAdminComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
