import { Component, OnInit } from '@angular/core';
import { Post } from 'src/app/models/post.model';
import { Tag } from 'src/app/models/tag.model';
import { PostsService } from 'src/app/services/posts.service';
import { NgToastService } from 'ng-angular-popup';
import {Router} from "@angular/router"
import { JwtHelperService } from '@auth0/angular-jwt';
import { AuthService } from 'src/app/services/auth.service';
@Component({
  selector: 'app-post-add',
  templateUrl: './post-add.component.html',
  styleUrls: ['./post-add.component.css']
})
export class PostAddComponent implements OnInit {

  addPostRequest: Post = {
    id : 0,
    title: "",
    content: "",
    summary: "",
    userId : 0,
    postStatus: 0,
    createdAt :  new Date(),
    updatedAt :  new Date(),
    authorName: "", 
  }

  constructor(private authService : AuthService, private postService : PostsService,private toast: NgToastService,private router: Router) { }


  ngOnInit(): void {
    const helper = new JwtHelperService();
    const token: any = this.authService.getToken(); 
    this.addPostRequest.userId = +helper.decodeToken(token).nameid;
  }


  addPost(){
    this.postService.addPost(this.addPostRequest)
    .subscribe({
      next: (post) =>{
        console.log(post);
        this.toast.success({detail: "SUCCESS", summary: "Post added successfuly", duration: 5000});
        this.router.navigate(['/']);
      },
      error: (error) =>{
        console.log(error);
        this.toast.error({detail: "ERROR", summary: "Some error occured", duration: 5000});
      }
    })
  }
}
