import { Review } from "./reviewDTO"
import { User, UserInfo } from "./userDTO"

export interface Service
{
    id : string,
    failureDescription : string,
    picture? : string
    startDate : string,
    endDate : string,
    serviceStatus : string,
    razlogOdbijanja : string,
    users : User,
    //taskServices:TaskService
    reviews : Review
}


export interface UserServiceInfo //DODAJ MU MOZDA I REVIEW-GRADE KOJI MU JE DAO 
{
    id : string,
    failureDescription : string,
    picture? : string
    startDate : string,
    endDate : string,
    serviceStatus : string,
    razlogOdbijanja : string
}

export interface DirektorServiceInfo
{
    id : string,
    failureDescription : string,
    picture? : string
    startDate : string,
    endDate : string,
    serviceStatus : string,
    razlogOdbijanja : string,
    userInfo: UserInfo,
}

export interface CreateServiceDto
{
    failureDescription : string,
    picture? : string
    startDate : string,
}