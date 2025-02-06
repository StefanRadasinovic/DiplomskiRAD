import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { BehaviorSubject, Observable } from "rxjs";
import { UserInfo, UserLoginDto, UserRegisterDto } from "../models/userDTO";
import jwt_decode from 'jwt-decode';

const baseUrl = 'https://localhost:7213/api/Authentication';


@Injectable({
  providedIn: 'root'
})
export class AuthorisationService {

  private readonly TOKEN_KEY = 'token';
  userRoleSubject = new BehaviorSubject<string | null>(null);
  userRole$ = this.userRoleSubject.asObservable();

  constructor(private http: HttpClient, private router: Router) {
    this.loadInitialRole();
  }

   loadInitialRole() {
    const token = localStorage.getItem(this.TOKEN_KEY);
    if (token) {
      this.userRoleSubject.next(this.getUserRole());
    } else {
      this.userRoleSubject.next('ROLE_NOTAUTH');
    }
  }

  isLoggedIn(): boolean {
    return !!this.getToken(); 
  }
  
  login(userLoginDto: UserLoginDto): Observable<any> {
    return this.http.post(`${baseUrl}/login`, userLoginDto);
  }

  register(userRegisterDto: UserRegisterDto): Observable<any> {
    return this.http.post(`${baseUrl}/register`, userRegisterDto);
  }

  logout() {
    this.removeToken();
    this.userRoleSubject.next('ROLE_NOTAUTH');
    this.router.navigate(['/login']);
  }
  

  setToken(token: string) {
    localStorage.setItem(this.TOKEN_KEY, token);
  }

  getToken(): string | null {
    const token = localStorage.getItem(this.TOKEN_KEY);
    //console.log("TOken is : ",token);
    return token;
  }

  removeToken() {
    localStorage.removeItem(this.TOKEN_KEY);
  }

  getUserRole(): string {
    const token = this.getToken();
    if (token) {
      try {
        const payload: any = jwt_decode(token);
        console.log("Decoded Token Payload:", payload);  
        const userRole: string = payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] || 'empty';
        console.log("rola je:",userRole);
        return userRole;
      } catch (error) {
        console.error("Error decoding token:", error);
        return '';
      }
    } else {
      return '';
    }
  }

  getUserId(): string | null {
    const token = this.getToken();
    if (token) {
      try {
        const payload: any = jwt_decode(token);
        //console.log("UlogovanKorisnikID:",payload.id);
        return payload.id || null; 
      } catch (error) {
        console.error("Error decoding token:", error);
        return null;
      }
    }
    return null;
  }

  
  getLogedUserInfo(): any {
    const token= this.getToken();
    if (token!=null) {
      const payload: any = jwt_decode(token);
      const id = payload.id;
      const name: string = payload["name"] || "empty";
      const surname: string =  payload["surname"] || "empty";
      const username: string = payload["username"] || "empty";
      //console.log("UserId je:", id, "name je:", name, "surname je:",surname, "username je:",username);
      const userRole: string = payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
      let logedUserInfo: UserInfo = {
    
        id: id,
        name : name,
        surname : surname,
        username: username,
        role: userRole,
        
        }
      //console.log("UserId je:", logedUserInfo.id, "name je:", logedUserInfo.name, "surname je:",logedUserInfo.surname, "username je:",logedUserInfo.username);
      return logedUserInfo;
    } else {
      return null;
    }
  }
  
}