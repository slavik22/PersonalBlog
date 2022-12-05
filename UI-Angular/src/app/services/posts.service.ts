import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Post } from '../models/post.model';
import { Observable } from 'rxjs';
import { Tag } from '../models/tag.model';
import { Category } from '../models/category.model';

@Injectable({
  providedIn: 'root'
})
export class PostsService {
  baseApiUrl : string = environment.baseApiUrl;

  constructor(private http: HttpClient) { }

  getAllPosts() : Observable<Post[]>{
    return this.http.get<Post[]>(this.baseApiUrl + "api/posts/all");
  }
  getAllPostsPublished() : Observable<Post[]>{
    return this.http.get<Post[]>(this.baseApiUrl + "api/posts/published");
  }
  addPost(addPostRequest: Post): Observable<Post>{
    return this.http.post<any>(this.baseApiUrl + "api/posts", addPostRequest);
  }

  editPost(postId:number, post: Post): Observable<Post>{
    return this.http.put<Post>(this.baseApiUrl + "api/posts/" + postId, post);
  }
  getPostById(id : number) : Observable<Post>{
    return this.http.get<Post>(this.baseApiUrl + "api/posts/" + id);
  }
  getUserPosts(userId:string):Observable<Post[]>{
    return this.http.get<Post[]>(this.baseApiUrl + "api/posts/user/" + userId);
  }
  removePost(id:number){
    return this.http.delete<Post>(this.baseApiUrl + "api/posts/" + id);
  }
  publishPost(post:Post){
    post.postStatus = 1;
    return this.http.put<any>(`${this.baseApiUrl}api/posts/${post.id}`, post);
  }

  getSearchPosts(searchText:string){
    return this.http.get<Post[]>(this.baseApiUrl + "api/posts/search/" + searchText);
  }

  getCategoryPosts(categoryId:number){
    return this.http.get<Post[]>(this.baseApiUrl + "api/posts/category/" + categoryId);

  }

} 
