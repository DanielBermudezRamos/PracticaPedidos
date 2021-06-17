import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { map } from "rxjs/operators";
import { User } from '../domain/user';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  myURL = environment.myURL;//'https://localhost:44359/';
  myAPI = 'api/AuthManagement/';

  constructor(public http: HttpClient, private router: Router) { }

  private isNullOrUndefined(valor: any): boolean {
    return valor === null || valor === undefined;
  }
  headers: HttpHeaders = new HttpHeaders({
    "Content-Type": "application/json"
  });

  registerUser(name: string, email: string, password: string) {
    const url_api = this.myURL + this.myAPI;
    return this.http
      .post<User>(
        url_api,
        {
          name: name,
          email: email,
          password: password
        },
        { headers: this.headers }
      )
      .pipe(map(data => data));
  }

  loginUser(item: User): Observable<any> {
    console.log('Usuario:');
    console.log(item);
    const url_api = `${this.myURL}api/authManagement`;
    return this.http.post(url_api, item);
  }

  setUser(user: User): void {
    let user_string = JSON.stringify(user);
    sessionStorage.setItem("currentUser", user_string);
  }

  setToken(token: string): void {
    sessionStorage.setItem("accessToken", token);
  }

  getToken() {
    return sessionStorage.getItem("accessToken");
  }

  getCurrentUser(): User {
    let user_string = sessionStorage.getItem("currentUser")!;
    if (!this.isNullOrUndefined(user_string)) {
      let user: User = JSON.parse(user_string);
      return user;
    } else { return { name: '', email: '', password: '' }; }
  }

  logoutUser() {
    let accessToken = sessionStorage.getItem("accessToken");
    const url_api = `${this.myURL}api/Users/logout?access_token=${accessToken}`;
    sessionStorage.removeItem("accessToken");
    sessionStorage.removeItem("currentUser");
    this.router.navigate(['/']);
    //return this.http.post<User>(url_api, { headers: this.headers });
  }

}
