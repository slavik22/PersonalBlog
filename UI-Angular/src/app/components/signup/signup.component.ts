import { Component, OnInit } from '@angular/core';
import { FormBuilder,FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { NgToastService } from 'ng-angular-popup';
@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {

  signupForm!:FormGroup;
  constructor(private fb: FormBuilder, private authService: AuthService, private router: Router, private toast: NgToastService) { }

  ngOnInit(): void {
    this.signupForm = this.fb.group({
      name: ["",Validators.required],
      surname: ["",Validators.required],
      email: ["",Validators.required],
      password: ["",Validators.required],
    });
  }
  onSubmit(){
    if(this.signupForm.valid){
      this.authService.signUp(this.signupForm.value).subscribe({
        next: res => {
          this.signupForm.reset();
          this.toast.success({detail: "SUCCESS", summary: res.message, duration: 5000});
          this.router.navigate(['login']);
        },
        error: error => {
          this.toast.error({detail: "Error", summary: "Sign up failed", duration: 2000  });
          alert(error.error.message)

        }
      })
    }
  }

}
