import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Post } from '../models/post.model';
import { Tag } from '../models/tag.model';

@Injectable({
  providedIn: 'root'
})
export class TagsService {
  baseApiUrl : string = environment.baseApiUrl;

  constructor(private http: HttpClient) { }

  
  addPostTags(postId: number, tag:Tag){
    return this.http.post<Post>(this.baseApiUrl + "api/posts/" + postId + "/tags", tag);
  }
  
  deleteTag(tagId:number){
    return this.http.delete<Tag>(this.baseApiUrl + "api/posts/tags/" + tagId);
  }
  getPostTags(postId:number):Observable<Tag[]>{
    return this.http.get<Tag[]>(this.baseApiUrl + "api/posts/" + postId + "/tags");
  }
}
