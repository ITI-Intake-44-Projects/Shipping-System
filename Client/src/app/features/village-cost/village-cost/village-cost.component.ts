import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { VillageCostService } from '../../../services/village-cost.service';

@Component({
  selector: 'app-village-cost',
  templateUrl: './village-cost.component.html',
  styleUrls: ['./village-cost.component.css']
})
export class VillageCostComponent implements OnInit {
  villageCostForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private villageCostService: VillageCostService
  ) {
    this.villageCostForm = this.fb.group({
      price: ['', Validators.required]
    });
  }

  ngOnInit(): void {}

  onSubmit() {
    if (this.villageCostForm.get('price')!.value === '') {
      alert('يرجى إدخال السعر قبل الحفظ');
      return;
    }

    // Proceed with form submission if valid
    if (this.villageCostForm.valid) {
      this.villageCostService.addVillageCost(this.villageCostForm.value).subscribe(
        response => {
          console.log('Village cost added successfully', response);
          alert('تم حفظ السعر بنجاح');
        },
        error => {
          console.error('Error adding village cost', error);
          alert('لم يتم حفظ السعر');
        }
      );
    }
  }
}
