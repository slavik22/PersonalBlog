import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Category } from '../models/category.model';
import { Post } from '../models/post.model';

@Injectable({
  providedIn: 'root'
})
export class CategoriesService {

  baseApiUrl : string = environment.baseApiUrl;

  constructor(private http: HttpClient) { }

  addPostCategory(postId: number, category:Category){
    return this.http.post<Post>(this.baseApiUrl + "api/categories/post/" + postId, category);
  }
  deleteCategory(categoryId:number){
    return this.http.delete<Category>(this.baseApiUrl + "api/categories/" + categoryId);
  }
  getPostCategories(postId:number):Observable<Post[]>{
    return this.http.get<Post[]>(this.baseApiUrl + "api/categories/post/" + postId);
  }
}
