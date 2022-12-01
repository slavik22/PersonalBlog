import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Post } from '../models/post.model';
import { Observable } from 'rxjs';
import { Tag } from '../models/tag.model';

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
    return this.http.post<Post>(this.baseApiUrl + "api/posts", addPostRequest);
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

  addPostTags(postId: number, tags:Tag[]){
    return this.http.post<Post>(this.baseApiUrl + "api/posts/" + postId + "/tags", tags);
  }
  getPostTags(postId:number):Observable<Tag[]>{
    return this.http.get<Tag[]>(this.baseApiUrl + "api/posts/" + postId + "/tags");
  }
} 
