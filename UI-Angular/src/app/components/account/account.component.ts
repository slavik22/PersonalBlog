import { Component, OnInit } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { FormBuilder,FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { NgToastService } from 'ng-angular-popup';
@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css']
})
export class AccountComponent implements OnInit {

  constructor(private fb: FormBuilder, private authService: AuthService, private router: Router, private toast: NgToastService) { }
  updateForm!:FormGroup;

  user:any = {};

  ngOnInit(): void {
    const helper = new JwtHelperService();
    const token: any = this.authService.getToken(); 
    this.user = helper.decodeToken(token);

    console.log(this.user);

    this.updateForm = this.fb.group({
      name: [this.user.name.split(" ")[0],Validators.required],
      surname: [this.user.name.split(" ")[1],Validators.required],
      email: [this.user.email,Validators.required],
      password: ["",Validators.required],
      newPassword: [""] 
    });

  }
    onSubmit(){
      if(this.updateForm.valid){
        this.authService.update({id: +this.user.nameid, ...this.updateForm.value}).subscribe({
          next: res => {
            this.updateForm.reset();
            this.toast.success({detail: "SUCCESS", summary: "Your data updated successfully", duration: 5000});
            this.authService.signOut();
            this.router.navigate(['login']);
          },
          error: error => {
            this.toast.error({detail: "Error", summary: error.error.message, duration: 5000});
          }
        })
      }
    }
  }