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

  constructor(private authService: AuthService , private alertifier: AlertifyService) { }

  ngOnInit() {
  }

  register() {
     this.authService.register(this.model).subscribe(() => {
        //  console.log('registration success');
        this.alertifier.success('Registered Successfully');
     }, error => {
      //  console.log(error);
      this.alertifier.error('failed to register');
     });

    //  console.log(this.model);
    if (this.model.password != null) {
      this.alertifier.message(this.model);
    }
   }

   cancel() {
     this.cancelRegister.emit(false);
    //  console.log('cancelled');
    this.alertifier.warning('cancelled warning');
   }
}
