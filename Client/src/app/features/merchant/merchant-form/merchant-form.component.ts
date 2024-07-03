import { Component, OnInit, Input } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormArray } from '@angular/forms';
import { MerchantService } from '../merchant.service';
import { MerchantDTO, SpecialPriceDTO,Merchant } from '../merchant.model';
import { emailValidator, passwordValidator, noSpacesValidator } from './custom-validators'; 
@Component({
  selector: 'app-merchant-form',
  templateUrl: './merchant-form.component.html',
  styleUrl:'./merchant-form.component.css'
})
export class MerchantFormComponent implements OnInit {
  @Input() initialMerchant?:any ; // Input to receive initial merchant data

  merchantForm: any ;

  constructor(
    private fb: FormBuilder,
    private merchantService: MerchantService
  ) {}

  ngOnInit(): void {
    this.createForm();
    if (this.initialMerchant) {
      console.log(this.initialMerchant)
      this.populateForm(this.initialMerchant);
    }
  }

  createForm(): void {
    this.merchantForm = this.fb.group({
      fullName: ['', Validators.required],
      email: ['', [Validators.required, emailValidator]],
      phoneNumber: ['', Validators.required],
      password: ['', [Validators.required,passwordValidator]],
      userName: ['', [Validators.required,noSpacesValidator]],
      address: ['', Validators.required],
      governate: ['', Validators.required],
      city: ['', Validators.required],
      storeName: ['', Validators.required],
      specialPickupCost: ['', Validators.required],
      inCompleteShippingRatio: ['', Validators.required],
      branchName: ['', Validators.required],
      specialPrices: this.fb.array([])
    });

    // Optional: Prepopulate with an initial special price
    this.addSpecialPrice();
  }

  populateForm(merchant: Merchant): void {
    this.merchantForm.patchValue({
      fullName: merchant.fullName,
      email: merchant.email,
      phoneNumber: merchant.phoneNumber,
      password: merchant.password,
      userName: merchant.userName,
      address: merchant.address,
      governate: merchant.governate,
      city: merchant.city,
      storeName: merchant.storeName,
      specialPickupCost: merchant.specialPickupCost,
      inCompleteShippingRatio: merchant.inCompleteShippingRatio,
      branchName: merchant.branchName
    });

    // Populate special prices
    this.clearSpecialPrices();
    merchant.specialPrices.forEach(sp => {
      this.specialPrices.push(this.fb.group({
        governate: sp.governate,
        city: sp.city,
        transportCost: sp.transportCost
      }));
    });
  }

  // Convenience getter for easy access to form array
  get specialPrices(): FormArray {
    return this.merchantForm.get('specialPrices') as FormArray;
  }

  // Method to add special price entry
  addSpecialPrice(): void {
    this.specialPrices.push(this.fb.group({
      governate: ['', Validators.required],
      city: ['', Validators.required],
      transportCost: ['', Validators.required]
    }));
  }

  // Method to remove a specific special price entry
  removeSpecialPrice(index: number): void {
    this.specialPrices.removeAt(index);
  }

  onSubmit(): void {
    console.log(this.merchantForm.valid);
    if (this.merchantForm.valid) {
      const merchantData: MerchantDTO = this.merchantForm.value;
      // Determine whether to create or update based on existence of initialMerchant data
      if (this.initialMerchant) {
        // Update existing merchant
        this.updateMerchant(merchantData);
      } else {
        // Create new merchant
        this.createMerchant(merchantData);
      }
    }
  }

  createMerchant(merchantData: MerchantDTO): void {
    console.log("creating called")
    this.merchantService.createMerchant(merchantData).subscribe(
      (response) => {
        console.log('Merchant created successfully:', response);
        // Optionally close the modal or handle success
      },
      (error) => {
        console.error('Error creating merchant:', error);
        // Handle error here
      }
    );
  }

  updateMerchant(merchantData: MerchantDTO): void {
    // Assuming you have a method in merchantService for updating
    this.merchantService.updateMerchant(this.initialMerchant.id, merchantData).subscribe(
      (response) => {
        console.log('Merchant updated successfully:', response);
        // Optionally close the modal or handle success
      },
      (error) => {
        console.error('Error updating merchant:', error);
        // Handle error here
      }
    );
  }

  // Clear special prices form array
  private clearSpecialPrices(): void {
    while (this.specialPrices.length !== 0) {
      this.specialPrices.removeAt(0);
    }
  }
}
