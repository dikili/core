import { Component, OnInit, EventEmitter, Input, Output } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
model: any = {};
@Input() ValuesFromHome: any;
@Output() cancelRegister= new EventEmitter();
registerForm: FormGroup;

  constructor(private authService: AuthService , private alertifier: AlertifyService) { }

  ngOnInit() {
    this.registerForm = new FormGroup({
       username: new FormControl('', Validators.required),
       password: new FormControl('', [Validators.required, Validators.minLength(4), Validators.maxLength(8)]),
       confirmPassword: new FormControl()

    }, this.passwordMatchValidator);
  }
  passwordMatchValidator(g: FormGroup) {
   return g.get('password').value === g.get('confirmPassword').value ? null : {'mismatch': true};
  }
  register() {
    // old method until 42nd line
    //  this.authService.register(this.model).subscribe(() => {
    //     //  console.log('registration success');
    //     this.alertifier.success('Registered Successfully');
    //  }, error => {
    //   //  console.log(error);
    //   this.alertifier.error(error);
    //  });

    // //  console.log(this.model);
    // if (this.model.password != null) {
    //   this.alertifier.message(this.model);
    // }

    console.log(this.registerForm.value);
   }

   cancel() {
     this.cancelRegister.emit(false);
    //  console.log('cancelled');
    this.alertifier.warning('cancelled warning');
   }
}
