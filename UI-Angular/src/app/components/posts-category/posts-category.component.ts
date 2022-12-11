import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Post } from 'src/app/models/post.model';
import { AuthService } from 'src/app/services/auth.service';
import { PostsService } from 'src/app/services/posts.service';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-posts-category',
  templateUrl: './posts-category.component.html',
  styleUrls: ['./posts-category.component.css']
})
export class PostsCategoryComponent implements OnInit {

  posts : Post[] = [];


  constructor(public datePipe: DatePipe,private postsService: PostsService, private route:ActivatedRoute, private router: Router, private authService: AuthService) {

   }

   onPostClick(postId : number){
    this.router.navigate(['/post/', postId])
   }

  ngOnInit(): void {


    this.route.url.subscribe( (data)=>{
      const categoryId = +data[1]


      this.postsService.getCategoryPosts(categoryId)
      .subscribe({
        next: (posts) => {
          this.posts = posts[0]=== null ? [] : posts;
        },
        error: (error) =>{
          console.error(error);
          
        }
      });

    });
   
  }
}
