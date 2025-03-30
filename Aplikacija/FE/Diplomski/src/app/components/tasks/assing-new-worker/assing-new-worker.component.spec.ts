import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AssingNewWorkerComponent } from './assing-new-worker.component';

describe('AssingNewWorkerComponent', () => {
  let component: AssingNewWorkerComponent;
  let fixture: ComponentFixture<AssingNewWorkerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AssingNewWorkerComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AssingNewWorkerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
