import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import { Category } from './models/category.model';
import { AuthService } from './services/auth.service';
import { CategoriesService } from './services/categories.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'PersonalBlogUI';
  _authService : AuthService;

  searchText:string = "";
  postCategories:Category[] = [];

  constructor(private router:Router, private toast:NgToastService, private fb: FormBuilder, 
    private authService: AuthService, private categoriesService:CategoriesService){
    this._authService = authService;
  }

  searchForm!:FormGroup;

  ngOnInit(){
    this.searchForm = this.fb.group({
      searchText: ["",Validators.required],
    });

    this.categoriesService.getAllCategories()
      .subscribe({
        next: (categories) => {
          this.postCategories = categories.slice(0,10);
          console.log(this.postCategories);

        },
        error: (error) => {
        this.toast.error({detail: "ERROR", summary: "Categories error", duration: 5000});
  
        }
      })
  }

  searchPosts(){
    if(this.searchForm.hasError('required','searchText')){
      this.toast.error({detail: "Error", summary: "Search input is empty", duration: 3000});
      return;
    }
    this.router.navigate(['/posts/search/', this.searchForm.value.searchText])

  }
}
