import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { User } from '../models/user.model';

@Injectable({
  providedIn: 'root'
})
export class UsersService {
  baseApiUrl : string = environment.baseApiUrl;

  constructor(private httpClient: HttpClient) { }

  getUsers():Observable<User[]>{
    return this.httpClient.get<any>(this.baseApiUrl + "api/User")
  }
  removeUser(userId:number){
    return this.httpClient.delete<any>(this.baseApiUrl + "api/User/" + userId);
  }
  updateUser(user:any){
    return this.httpClient.put<any>(this.baseApiUrl + "api/User/" + user.id, user);
  }

  updateToAdmin(user:any){
    return this.httpClient.put<any>(`${this.baseApiUrl}api/User/${user.id}/makeAdmin`, user);
  }
}
