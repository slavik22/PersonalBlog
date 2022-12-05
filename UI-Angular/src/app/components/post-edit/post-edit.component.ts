import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { NgToastService } from 'ng-angular-popup';
import { Category } from 'src/app/models/category.model';
import { Post } from 'src/app/models/post.model';
import { Tag } from 'src/app/models/tag.model';
import { AuthService } from 'src/app/services/auth.service';
import { CategoriesService } from 'src/app/services/categories.service';
import { PostsService } from 'src/app/services/posts.service';
import { TagsService } from 'src/app/services/tags.service';

@Component({
  selector: 'app-post-edit',
  templateUrl: './post-edit.component.html',
  styleUrls: ['./post-edit.component.css']
})
export class PostEditComponent implements OnInit {

  post: Post = {
    id : 0,
    title: "",
    content: "",
    summary: "",
    userId : 0,
    postStatus: 0,
    createdAt : new Date(),
    updatedAt : new Date(),
    authorName: "", 
  }
  
  postTags:Tag[] = [];
  postCategories:Category[] = [];


  tagForm!:FormGroup;
  categoryForm!:FormGroup;

  constructor(private fb:FormBuilder, private route: ActivatedRoute, 
    private authService : AuthService, private postService : PostsService,
    private categoriesService: CategoriesService,private tagsService: TagsService,
    private toast: NgToastService,private router: Router) {}

  postEditForm!:FormGroup;

  ngOnInit(): void {
    const helper = new JwtHelperService();
    const token: any = this.authService.getToken(); 
    const userId: number = +helper.decodeToken(token).nameid;


    this.route.url.subscribe( (data)=>{
      this.post.id = +data[2];
      
      this.postService.getPostById(this.post.id).subscribe({
        next: (post) => {
          this.post = post
        
          if(userId != post.userId){
            this.router.navigate(['**']);
          }

          this.postEditForm = this.fb.group({
            title: [this.post.title,Validators.required],
            summary: [this.post.summary,Validators.required],
            content: [this.post.content,Validators.required],
          });
        },
        error: (error) => {
           this.router.navigate(['**']);
        }
      });

      this.tagsService.getPostTags(this.post.id)
      .subscribe({
        next: (tags) => {
          this.postTags = tags
        },
        error: (error) => {
        this.toast.error({detail: "ERROR", summary: "Tags error", duration: 5000});
        }
      });

      this.categoriesService.getPostCategories(this.post.id)
      .subscribe({
        next: (categories) => {
          this.postCategories = categories
        },
        error: (error) => {
        this.toast.error({detail: "ERROR", summary: "Categories error", duration: 5000});
  
        }
      })
    });

    this.tagForm = this.fb.group({
      title: ["Add new tag...",Validators.required],
    });
    this.categoryForm = this.fb.group({
      title: ["Add new category...",Validators.required],
    });
  }


  editPost(){
    this.postService.editPost(this.post.id, this.post)
    .subscribe({
      next: (post) =>{
        this.toast.success({detail: "SUCCESS", summary: "Post edited successfuly", duration: 5000});
        this.router.navigate(['dashboard']);
      },
      error: (error) =>{
        this.toast.error({detail: "ERROR", summary: "Some error occured", duration: 5000});
      }
    })
  }

  onTagSubmit(){
    const tag:Tag = {
      id: 0,
      title: this.tagForm.value.title
    };

    this.tagsService.addPostTags(this.post.id, tag)
    .subscribe({
      next: () =>{
        this.toast.success({detail: "SUCCESS", summary: "Tag added successfuly", duration: 5000});
        this.tagForm.reset();
         this.ngOnInit();
      },
      error: (error) =>{
        console.log(error);
        this.toast.error({detail: "ERROR", summary: "Some error occured", duration: 5000});
      }
    })
  }

  onCategorySubmit(){
    const category:Category = {
      id: 0,
      title: this.categoryForm.value.title
    };

    this.categoriesService.addPostCategory(this.post.id, category)
    .subscribe({
      next: () =>{
        this.toast.success({detail: "SUCCESS", summary: "Category added successfuly", duration: 5000});
        this.tagForm.reset();
         this.ngOnInit();
      },
      error: (error) =>{
        console.log(error);
        this.toast.error({detail: "ERROR", summary: "Some error occured", duration: 5000});
      }
    })
  }

  deleteCategory(categoryId:number){
    this.categoriesService.deleteCategory(categoryId)
    .subscribe({
      next: () =>{
        this.toast.success({detail: "SUCCESS", summary: "Category removed successfuly", duration: 5000});
         this.ngOnInit();
      },
      error: (error) =>{
        console.log(error);
        this.toast.error({detail: "ERROR", summary: "Some error occured", duration: 5000});
      }
    })
  }

  deleteTag(tagId:number){
    this.tagsService.deleteTag(tagId)
    .subscribe({
      next: () =>{
        this.toast.success({detail: "SUCCESS", summary: "Tag removed successfuly", duration: 5000});
         this.ngOnInit();
      },
      error: (error) =>{
        console.log(error);
        this.toast.error({detail: "ERROR", summary: "Some error occured", duration: 5000});
      }
    })
  }
}
