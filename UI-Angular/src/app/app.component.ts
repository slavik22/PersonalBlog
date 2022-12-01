import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'PersonalBlogUI';
  _authService : AuthService;

  searchText:string = "";

  constructor(private router:Router, private toast:NgToastService, private fb: FormBuilder, private authService: AuthService){
    this._authService = authService;
  }

  searchForm!:FormGroup;

  ngOnInit(){
    this.searchForm = this.fb.group({
      searchText: ["",Validators.required],
    });
  }

  searchPosts(){
    if(this.searchForm.hasError('required','searchText')){
      this.toast.error({detail: "Error", summary: "Search input is empty", duration: 3000});
      return;
    }
    this.router.navigate(['/posts/search/', this.searchForm.value.searchText])
    // this.router.navigate["posts/search"];

  }
}
