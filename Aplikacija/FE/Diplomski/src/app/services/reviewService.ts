import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { CreateReviewDto, ReviewInfo } from "../models/reviewDTO";
import { Observable } from "rxjs";

const baseUrl = 'https://localhost:7213/api/Review'


@Injectable({
  providedIn: 'root'
})
export class ReviewService {
    constructor(private http: HttpClient) {}
  
    createReview(serviceId: string, userId: string, data: CreateReviewDto): Observable<ReviewInfo> {
        return this.http.post<ReviewInfo>(`${baseUrl}/review-service/${serviceId}?userId=${userId}`,data);
    }
}
      
