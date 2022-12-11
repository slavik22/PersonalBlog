import { Component, Inject, OnInit } from '@angular/core';
import {HttpClient} from '@angular/common/http'
import { Post } from 'src/app/models/post.model';
import { PostsService } from 'src/app/services/posts.service';
import {Router} from "@angular/router"
import { AuthService } from 'src/app/services/auth.service';
import { DatePipe } from '@angular/common';
@Component({
  selector: 'app-posts-list',
  templateUrl: './posts-list.component.html',
  styleUrls: ['./posts-list.component.css']
})
export class PostsListComponent implements OnInit {
    posts : Post[] = [];


  constructor(public datePipe: DatePipe,private postsService: PostsService, private router: Router, private authService: AuthService) {

   }

   onPostClick(postId : number){
    this.router.navigate(['/post/', postId])
   }

  ngOnInit(): void {
    this.postsService.getAllPostsPublished()
    .subscribe({
      next: (posts) => {
        this.posts = posts;
        //this.posts = posts[0] === null ? [] : posts;
      },
      error: (error) =>{
        console.error(error);
        
      }
    })
  }

}