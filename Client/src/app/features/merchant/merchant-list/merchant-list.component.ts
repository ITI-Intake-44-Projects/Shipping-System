import { Component, OnInit,ViewChild} from '@angular/core';
import { MerchantService } from '../merchant.service';
import { Merchant } from '../merchant.model';
import { MatDialog } from '@angular/material/dialog';
import { MerchantFormComponent } from '../merchant-form/merchant-form.component';
//import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { MerchantModalComponent } from '../merchant-modal/merchant-modal.component';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { MerchantDTO } from '../merchant.model';
@Component({
  selector: 'app-merchant-list',
  templateUrl: './merchant-list.component.html',
  styleUrls: ['./merchant-list.component.css']
})
export class MerchantListComponent implements OnInit {

  
  merchants: Merchant[] = [];
  page = 1;
  pageSize = 5;
  totalMerchants = 0;
  

  constructor(private merchantService: MerchantService,public dialog: MatDialog,private modalService: NgbModal ) {
    console.log('merchant intialized')
  }

  ngOnInit(): void {
    this.loadMerchants();
    console.log('merchant intialized')
  }

  loadMerchants(): void {
    this.merchantService.getMerchants(this.page, this.pageSize).subscribe(response => {
      this.merchants = response;
      this.totalMerchants = response.length;
      console.log(this.totalMerchants)
    });
  }
  onPageNext( ): void {
    this.page = this.page +1;
    this.loadMerchants();
  }
  onPagePrevious(): void {
    this.page = this.page - 1;
    this.loadMerchants();
  }
  onPageReset(): void {
    this.page = 1;
    this.loadMerchants();
  }
  openMerchantDetail(merchantId: string): void {
    this.merchantService.getMerchantById(merchantId).subscribe((merchant) => {
      const dialogRef = this.dialog.open(MerchantModalComponent, {
        width: '40vw',
        data: { merchant }
      });
      dialogRef.afterClosed().subscribe(result => {
        console.log('The dialog was closed');
      });
    });
  }
  openMerchantFormModal(merchant?: Merchant): void {
    const modalRef = this.modalService.open(MerchantFormComponent, {
      windowClass: 'FormModal', 
      size: 'xl',
      
    });
    modalRef.componentInstance.initialMerchant = merchant; // Pass merchant data to edit if available
    // You can optionally handle modal close or dismiss here
    modalRef.result.then(
      (result) => {
        console.log('Modal closed with result:', result);
      },
      (reason) => {
        console.log('Modal dismissed with reason:', reason);
      }
    );
  }
}

