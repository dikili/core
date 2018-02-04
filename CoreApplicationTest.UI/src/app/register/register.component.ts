import { Component, OnInit, EventEmitter, Input, Output } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
model: any = {};
@Input() ValuesFromHome: any;
@Output() cancelRegister= new EventEmitter();

  constructor(private authService: AuthService , private alertify: AlertifyService) { }

  ngOnInit() {
  }

  register() {
     this.authService.register(this.model).subscribe(() => {
        //  console.log('registration success');
        this.alertify.success('Registered Successfully');
     }, error => {
      //  console.log(error);
      this.alertify.error('failed to register');
     });

    //  console.log(this.model);
    if (this.model.password != null) {
      this.alertify.message(this.model);
    }
   }

   cancel() {
     this.cancelRegister.emit(false);
    //  console.log('cancelled');
    this.alertify.warning('cancelled warning');
   }
}
