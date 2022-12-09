import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { PostsListComponent } from './components/posts-list/posts-list.component';
import { PostAddComponent } from './components/post-add/post-add.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PostShowComponent } from './components/post-show/post-show.component';
import { PagenotfoundComponent } from './components/pagenotfound/pagenotfound.component';
import { LoginComponent } from './components/login/login.component';
import { SignupComponent } from './components/signup/signup.component';
import { NgToastModule } from 'ng-angular-popup';
import { AuthGuard } from './guards/auth.guard';
import { TokenInterceptor } from './interceptors/token.interceptor';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { AccountComponent } from './components/account/account.component';
import { PostsSearchComponent } from './components/posts-search/posts-search.component';
import { PostEditComponent } from './components/post-edit/post-edit.component';
import { PostsCategoryComponent } from './components/posts-category/posts-category.component';
import { DatePipe } from '@angular/common';
@NgModule({
  declarations: [
    AppComponent,
    PostsListComponent,
    PostAddComponent,
    PostShowComponent,
    LoginComponent,
    SignupComponent,
    DashboardComponent,
    AccountComponent,
    PostsSearchComponent,
    PostEditComponent,
    PostsCategoryComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    NgToastModule,
    RouterModule.forRoot([
      {path: '', component: PostsListComponent, pathMatch: 'full'},
      {path: 'posts/add', component: PostAddComponent, canActivate:[AuthGuard]},
      {path: 'post/:postId', component: PostShowComponent},
      {path: 'post/edit/:postId', component: PostEditComponent,canActivate:[AuthGuard]},
      {path: 'posts/search/:searchText', component: PostsSearchComponent},
      {path: 'categories/:categoryId', component: PostsCategoryComponent},
      
      {path: 'login', component: LoginComponent},
      {path: 'signup', component: SignupComponent},
      {path: 'dashboard', component: DashboardComponent, canActivate:[AuthGuard]},
      {path: 'account', component: AccountComponent, canActivate:[AuthGuard]},
      { path: '**', pathMatch: 'full', component: PagenotfoundComponent },
   
    ]),
  ],

  providers: [
    DatePipe,
    {
    provide: HTTP_INTERCEPTORS,
    useClass: TokenInterceptor,
    multi: true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
