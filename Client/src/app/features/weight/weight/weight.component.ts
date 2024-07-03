import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { WeightService } from '../../../services/weight.service';

@Component({
  selector: 'app-weight',
  templateUrl: './weight.component.html',
  styleUrls: ['./weight.component.css']
})
export class WeightComponent implements OnInit {
  weightForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private weightService: WeightService
  ) {
    this.weightForm = this.fb.group({
      additionalKgPrice: ['', Validators.required],
      maximumWeight: ['', Validators.required]
    });
  }

  ngOnInit(): void {}

  onSubmit() {
    if (this.weightForm.invalid) {
      alert('يرجى ملء جميع الحقول قبل الحفظ');
      return;
    }

    console.log('Submitting form', this.weightForm.value);

    this.weightService.addWeightSettings(this.weightForm.value).subscribe(
      response => {
        console.log('تم حفظ إعدادات الوزن بنجاح', response);
        alert('تم حفظ إعدادات الوزن بنجاح');
      },
      error => {
        console.error('خطأ في حفظ إعدادات الوزن', error);
        alert('لم يتم حفظ إعدادات الوزن');
      }
    );
  }
}
