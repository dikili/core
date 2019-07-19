import { Component, OnInit, TemplateRef } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  registerMode= false;
  values: any;
  modalRef: BsModalRef;

  constructor(private http: HttpClient, private modalService: BsModalService) { }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }
  ngOnInit() {
   // this.getValues();
  }

  registerToggle() {
    this.registerMode = true;
  }
  getValues() {
    this.http.get('http://localhost:8000/api/values').subscribe(response => {
      console.log(response);

      this.values = response;
    });
  }

  cancelRegister(cancelFlag: boolean) {
   this.registerMode = cancelFlag;
   this.modalRef.hide();
  }
}
