import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AllTasksWorkerComponent } from './all-tasks-worker.component';

describe('AllTasksWorkerComponent', () => {
  let component: AllTasksWorkerComponent;
  let fixture: ComponentFixture<AllTasksWorkerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AllTasksWorkerComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AllTasksWorkerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
