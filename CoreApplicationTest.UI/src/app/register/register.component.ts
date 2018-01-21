import { Component, OnInit, EventEmitter, Input, Output } from '@angular/core';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
model: any = {};
@Input() ValuesFromHome: any;
@Output() cancelRegister= new EventEmitter();

  constructor() { }

  ngOnInit() {
  }

   register() {
     console.log(this.model);
   }

   cancel() {
     this.cancelRegister.emit(false);
     console.log('cancelled');
   }
}
