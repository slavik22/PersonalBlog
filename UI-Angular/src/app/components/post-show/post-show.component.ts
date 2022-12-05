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
import { TagsService } from 'src/app/services/tags.service';
import { Category } from 'src/app/models/category.model';
import { CategoriesService } from 'src/app/services/categories.service';
@Component({
  selector: 'app-post-show',
  templateUrl: './post-show.component.html',
  styleUrls: ['./post-show.component.css']
})
export class PostShowComponent implements OnInit {

   post : Post = {
    id: 0,
    content: "",
    summary: "",
    title: "",
    userId: 0,
    authorName: "",
    postStatus: 0,
    createdAt : new Date(),
    updatedAt : new Date(),
  };

  author:any = {
    id: 0,
    name : "",
  }

  postTags:Tag[] = [];
  postCategories:Category[] = [];
  comments:Comment[] = [];

  commentForm!:FormGroup;

  constructor(private route: ActivatedRoute, private router: Router,
    private postsService: PostsService,private tagsService: TagsService, 
    private categoiriesService: CategoriesService, public authService: AuthService,
    private fb: FormBuilder, private toast: NgToastService,
    private commentService: CommentService) { }

  ngOnInit(): void {
    const helper = new JwtHelperService();
    const token: any = this.authService.getToken(); 
    const tokenDecoded: any = helper.decodeToken(token);

    this.author.id = tokenDecoded.nameid;
    this.author.name = tokenDecoded.name;

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

    this.tagsService.getPostTags(this.post.id)
    .subscribe({
      next: (tags) => {
        this.postTags = tags
      },
      error: (error) => {
      this.toast.error({detail: "ERROR", summary: "Tags error", duration: 5000});

      }
    })
    this.categoiriesService.getPostCategories(this.post.id)
    .subscribe({
      next: (categories) => {
        this.postCategories = categories
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

onCommentSubmit(){
  const comment = {
    title: this.commentForm.value.title,
    content: this.commentForm.value.text,
    postId: this.post.id,
    authorName: this.author.name
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