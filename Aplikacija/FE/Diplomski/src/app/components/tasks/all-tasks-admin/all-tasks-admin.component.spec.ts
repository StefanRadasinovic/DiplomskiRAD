import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AllTasksAdminComponent } from './all-tasks-admin.component';

describe('AllTasksAdminComponent', () => {
  let component: AllTasksAdminComponent;
  let fixture: ComponentFixture<AllTasksAdminComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AllTasksAdminComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AllTasksAdminComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
