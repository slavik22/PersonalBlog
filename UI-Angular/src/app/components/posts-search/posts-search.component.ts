import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PostsService } from 'src/app/services/posts.service';
import { Post } from 'src/app/models/post.model';
@Component({
  selector: 'app-posts-search',
  templateUrl: './posts-search.component.html',
  styleUrls: ['./posts-search.component.css']
})
export class PostsSearchComponent implements OnInit {

  constructor(private route: ActivatedRoute, private postsService: PostsService,
    private router: Router) { }

  posts:Post[] = [];

  ngOnInit(): void {
    this.route.url.subscribe( (data)=>{
      const searchText:string = data[2].toString();
      this.postsService.getSearchPosts(searchText).subscribe({
        next: (posts) => this.posts = posts,
        error: (error) => {
          this.router.navigate(['**']);
          console.log(error);
        }
      });
    });
  }
  onPostClick(postId : number){
    this.router.navigate(['/post/', postId])
   }
}