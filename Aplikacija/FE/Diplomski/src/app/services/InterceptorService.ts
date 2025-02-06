import { HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { AuthorisationService } from "./authorisationService";
import jwt_decode from 'jwt-decode';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(private authService: AuthorisationService) {}

  intercept(req: HttpRequest<any>, next: HttpHandler) {
    const token = this.authService.getToken();
    console.log("Token in Interceptor:", token); 
  
    if (token) {
      try {
        const payload: any = jwt_decode(token);
       // console.log("Decoded Token in Interceptor:", payload); 
      } catch (error) {
        console.error("Error decoding token in Interceptor:", error);
      }
  
      const cloned = req.clone({
        setHeaders: { Authorization: `Bearer ${token}` }
      });
      return next.handle(cloned);
    }
    return next.handle(req);
  }
}