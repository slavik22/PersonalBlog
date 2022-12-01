import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
import { Post } from 'src/app/models/post.model';
import { Tag } from 'src/app/models/tag.model';

import { Comment } from 'src/app/models/comment.model';
import { AuthService } from 'src/app/services/auth.service';
import { PostsService } from 'src/app/services/posts.service';
import { NgToastService } from 'ng-angular-popup';
import { FormBuilder,FormGroup, Validators } from '@angular/forms';
import { CommentService } from 'src/app/services/comment.service';
import { JwtHelperService } from '@auth0/angular-jwt';
@Component({
  selector: 'app-post-show',
  templateUrl: './post-show.component.html',
  styleUrls: ['./post-show.component.css']
})
export class PostShowComponent implements OnInit {

   post : Post = {
    id: -1,
    content: "",
    summary: "",
    title: "",
    userId: 1,
    authorName: "",
    postStatus: -1,
    createdAt : new Date(),
    updatedAt : new Date()
  };

  postTags:Tag[] = [];

  comments:Comment[] = [];
  commentForm!:FormGroup;

  authorName:string = "";


  constructor(private route: ActivatedRoute, private router: Router,
    private postsService: PostsService, public authService: AuthService,
    private fb: FormBuilder, private toast: NgToastService,
    private commentService: CommentService) { }

  ngOnInit(): void {
    const helper = new JwtHelperService();
    const token: any = this.authService.getToken(); 
    this.authorName = helper.decodeToken(token).unique_name;


    this.route.url.subscribe( (data)=>{
      this.post.id = +data[1];
      this.postsService.getPostById(this.post.id).subscribe({
        next: (post) =>this.post = post,
        error: (error) => {
           this.router.navigate(['**']);
        }
      });
    });

    this.commentService.getAllComments(this.post.id)
    .subscribe({
      next: (comments: any) => {
        this.comments = comments;
      },
      error: (error) =>{
        console.error(error);
        this.toast.error({detail: "ERROR", summary: "Some error occured", duration: 5000});
      }
    })

    this.postsService.getPostTags(this.post.id).subscribe({
      next: (tags) => {
        this.postTags = tags
        console.log(tags);
      },
      error: (error) => {
      this.toast.error({detail: "ERROR", summary: "Tags error", duration: 5000});

      }
    })

    this.commentForm = this.fb.group({
      title: ["Your title...",Validators.required],
      text: ["Your comment...",Validators.required],
    });

  }

onSubmit(){
  const comment = {
    title: this.commentForm.value.title,
    content: this.commentForm.value.text,
    postId: this.post.id,
    authorName: this.authorName
  }

  this.commentService.addComment(comment)
    .subscribe({
      next: (res) =>{
        this.toast.success({detail: "SUCCESS", summary: "Comment added successfuly", duration: 5000});
       this.commentForm.reset();
        this.ngOnInit();
      },
      error: (error) =>{
        console.log(error);
        this.toast.error({detail: "ERROR", summary: "Some error occured", duration: 5000});
      }
    })
  }
}