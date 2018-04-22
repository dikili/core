import { Component, OnInit, EventEmitter, Input, Output } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { BsDatepickerConfig } from 'ngx-bootstrap';
import { Router } from '@angular/router';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
model: any = {};
user: any = {};
@Input() ValuesFromHome: any;
@Output() cancelRegister= new EventEmitter();
registerForm: FormGroup;
bsConfig: Partial<BsDatepickerConfig>;

  constructor(private authService: AuthService ,
    private alertifier: AlertifyService,
    private fb: FormBuilder,
    private router: Router) { }

  ngOnInit() {
    // no longer below needed since we have createRegisterForm()
    // this.registerForm = new FormGroup({
    //    username: new FormControl('', Validators.required),
    //    password: new FormControl('', [Validators.required, Validators.minLength(4), Validators.maxLength(8)]),
    //    confirmPassword: new FormControl()

    // }, this.passwordMatchValidator);
    this.createRegisterForm();
    this.bsConfig = {
      containerClass: 'theme-red'
    };
  }

  createRegisterForm() {
    this.registerForm = this.fb.group({
      gender: ['male'],
   username: ['', Validators.required],
   knownAs: ['', Validators.required],
   dateOfBirth: [null, Validators.required],
   city: ['', Validators.required],
   country: ['', Validators.required],
   password: [
     '',
     [Validators.required, Validators.minLength(4), Validators.maxLength(8)]
   ],
   confirmPassword: ['', Validators.required]
    }, { validator: this.passwordMatchValidator});
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
    if (this.registerForm.valid) {
      this.user = Object.assign({}, this.registerForm.value);
      this.authService.register(this.user).subscribe(() => {
        this.alertifier.success('Registered Successfully');
      }, error => {
        this.alertifier.error(error);
      },
      () => {
        // if all go well then login the user that is register
        // and route them to members page
        this.authService.login(this.user).subscribe(() => {
         this.router.navigate(['/members']);
        });
      }

      );
    }
    console.log(this.registerForm.value);
   }

   cancel() {
     this.cancelRegister.emit(false);
    //  console.log('cancelled');
    this.alertifier.warning('cancelled warning');
   }
}
