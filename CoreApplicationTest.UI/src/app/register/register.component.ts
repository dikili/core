import { Component, OnInit, EventEmitter, Input, Output } from '@angular/core';
import { AuthService } from '../_services/auth.service';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
model: any = {};
@Input() ValuesFromHome: any;
@Output() cancelRegister= new EventEmitter();

  constructor(private authService: AuthService) { }

  ngOnInit() {
  }

  register() {
     this.authService.register(this.model).subscribe(() => {
         console.log('registration success');
     }, error => {
       console.log(error);
     });

     console.log(this.model);
   }

   cancel() {
     this.cancelRegister.emit(false);
     console.log('cancelled');
   }
}
