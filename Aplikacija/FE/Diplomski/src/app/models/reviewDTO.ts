import { ServiceInfo } from "./serviceDTO";
import { UserInfo } from "./userDTO";

export interface Review  
{ 
    id : string,
    comment : string,
    grade? : number
    services : ServiceInfo,
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
}