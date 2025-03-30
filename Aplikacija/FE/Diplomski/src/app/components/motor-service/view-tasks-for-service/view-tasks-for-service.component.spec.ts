import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewTasksForServiceComponent } from './view-tasks-for-service.component';

describe('ViewTasksForServiceComponent', () => {
  let component: ViewTasksForServiceComponent;
  let fixture: ComponentFixture<ViewTasksForServiceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ViewTasksForServiceComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ViewTasksForServiceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
