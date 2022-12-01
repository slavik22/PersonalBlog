import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CommentService {

  baseApiUrl : string = environment.baseApiUrl;

  constructor(private http: HttpClient) { }
  
  getAllComments(postId:number) : Observable<Comment[]>{
    return this.http.get<Comment[]>(this.baseApiUrl + "api/comments/post/" + postId);
  }
  addComment(comment: any){
    return this.http.post<Comment>(this.baseApiUrl + "api/comments", comment);
  }
}
