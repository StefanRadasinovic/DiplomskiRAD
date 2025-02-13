import { UserInfo } from "./userDTO";

export interface Review  //proveri  ovo
{ 
    id : string,
    comment : string,
    grade? : number
    serviceId : string,
    users: UserInfo
}

export interface ReviewInfo 
{ 
    comment : string,
    grade? : number
}

export interface CreateReviewDto
{
    comment : string,
    grade? : number
    serviceId : string,
}