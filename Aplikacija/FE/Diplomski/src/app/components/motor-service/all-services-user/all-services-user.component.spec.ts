import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AllServicesUserComponent } from './all-services-user.component';

describe('AllServicesUserComponent', () => {
  let component: AllServicesUserComponent;
  let fixture: ComponentFixture<AllServicesUserComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AllServicesUserComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AllServicesUserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
