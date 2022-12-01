import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseApiUrl : string = environment.baseApiUrl;


  constructor(private http: HttpClient, private router: Router) { }

  signUp(user:any){
    return this.http.post<any>(`${this.baseApiUrl}api/User/register`,user)
  }
  login(user:any){
    return this.http.post<any>(`${this.baseApiUrl}api/User/authenticate`,user)
  }

  signOut(){
    localStorage.clear();
    this.router.navigate(["login"]);
  }

  update(value: any){
    return this.http.put<any>(`${this.baseApiUrl}api/User/${value.id}`,value);
  }

  storeToken(tokenValue: string){
    localStorage.setItem("token", tokenValue);
  }
  getToken(){
    return localStorage.getItem("token");
  }
  isLoggedIn():boolean{
    return !!localStorage.getItem("token");
  }
}
