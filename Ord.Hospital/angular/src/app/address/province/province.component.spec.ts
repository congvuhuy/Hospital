import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProvinceComponent } from './province.component';

describe('ProvinceComponent', () => {
  let component: ProvinceComponent;
  let fixture: ComponentFixture<ProvinceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ProvinceComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ProvinceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
