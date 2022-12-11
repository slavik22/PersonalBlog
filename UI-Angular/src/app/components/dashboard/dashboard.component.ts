import { Component, OnInit } from '@angular/core';
import { NgToastService } from 'ng-angular-popup';
import { User } from 'src/app/models/user.model';
import { AuthService } from 'src/app/services/auth.service';
import { UsersService } from 'src/app/services/users.service';
import { JwtHelperService } from '@auth0/angular-jwt';
import { PostsService } from 'src/app/services/posts.service';
import { Post } from 'src/app/models/post.model';
import { environment } from 'src/environments/environment';
import { Router } from '@angular/router';
import { DatePipe } from '@angular/common';
@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  constructor(public datePipe:DatePipe, private router:Router, private toast: NgToastService, private usersService: UsersService,private postsService: PostsService,private authService: AuthService) { }

  user:any = {};
  
  users: User[] = [];
  userPosts: Post[] = [];
  allPosts: Post[] = [];
  baseUrl: string = "";

  ngOnInit(): void {
    this.baseUrl = environment.baseApiUrl;

    const helper = new JwtHelperService();
    const token: any = this.authService.getToken(); 
    this.user = helper.decodeToken(token);

    this.usersService.getUsers()
    .subscribe({
      next: (users) => {
        users = users[0] == null ? [] : users;
        this.users = users.filter(user => user.email !== this.user.email);
      },
      error: (error) =>{
        this.toast.error({detail: "ERROR", summary: "Some error occured", duration: 5000});
      }
    })

    this.postsService.getUserPosts(this.user.nameid)
    .subscribe({
      next: (posts) => {
        this.userPosts = posts[0] === null ? [] : posts;
      },
      error: (error) =>{
        this.toast.error({detail: "ERROR", summary: "Some error occured", duration: 5000});
      }
    })


    this.postsService.getAllPosts()
    .subscribe({
      next: (posts) => {
        this.allPosts = posts[0] === null ? [] : posts;
      },
      error: (error) =>{
        this.toast.error({detail: "ERROR", summary: "Some error occured", duration: 5000});
      }
    })
  }


  deleteUser(id: number){
    this.usersService.removeUser(id)
    .subscribe({
      next: (response) => {
        this.toast.success({detail: "SUCCESS", summary: "User deleted successfuly", duration: 5000});
        this.ngOnInit();
      },
      error: (error) =>{
        this.toast.error({detail: "ERROR", summary: error.error.message, duration: 5000});
      }
    })
  }


  deletePost(id: number){
    console.log(id);

    this.postsService.removePost(id)
    .subscribe({
      next: (response) => {
        this.toast.success({detail: "SUCCESS", summary: "POst deleted successfuly", duration: 5000});
        this.ngOnInit();
      },
      error: (error) =>{
        this.toast.error({detail: "ERROR", summary: error.error.message, duration: 5000});
      }
    })
  }

  publishPost(post:Post){
    this.postsService.publishPost(post)
    .subscribe({
          next: (response) => {
            this.toast.success({detail: "SUCCESS", summary: "Post published successfuly", duration: 5000});
          },
          error: (error) =>{
            this.toast.error({detail: "ERROR", summary: error.message, duration: 5000});
          }
        })
  }

  makeUserAdmin(user:User){
    this.usersService.updateToAdmin(user)
    .subscribe({
      next: (response) => {
        this.toast.success({detail: "SUCCESS", summary: "User role updated successfuly", duration: 5000});
      },
      error: (error) =>{
        console.log(error);
        this.toast.error({detail: "ERROR", summary: error.message, duration: 5000});
      }
    })
  }

  editPost(postId:number){
    this.router.navigate(['/post/edit/', postId])
  }
}
