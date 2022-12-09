import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from 'src/app/services/auth.service';
import { NgToastService } from 'ng-angular-popup';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginForm!:FormGroup;
  constructor(private fb: FormBuilder, private authService: AuthService, private toast: NgToastService, private router: Router) { }

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      email: ["",Validators.required],
      password: ["",Validators.required]

    });
  }

  onSubmit(){
    if(this.loginForm.valid){
      this.authService.login(this.loginForm.value).subscribe({
        next: res => {
          this.loginForm.reset();
          this.authService.storeToken(res.token);
          this.toast.success({detail: "SUCCESS", summary: res.message, duration: 5000});
          this.router.navigate(['dashboard']);
        },
        error: error => {
          this.toast.error({detail: "Error", summary: error.error.message, duration: 5000});
        }
      })
    }
    else{
      this.loginForm.markAsDirty();
    }
  }

}
