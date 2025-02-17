import { Review, ReviewInfo } from "./reviewDTO"
import { TaskService, TaskServiceInfo } from "./taskDTO"
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
    taskServices:TaskService[]
    reviews : Review[]
}

export interface ServiceInfo
{
    id : string,
    failureDescription : string,
    picture? : string
    startDate : string,
    endDate : string,
    serviceStatus : string,
    razlogOdbijanja : string,
    userInfo: UserInfo,
    taskServiceInfo : TaskServiceInfo[]
    reviewInfos : ReviewInfo[]
}


export interface UserServiceInfo 
{
    id : string,
    failureDescription : string,
    picture? : string
    startDate : string,
    endDate : string,
    serviceStatus : string,
    razlogOdbijanja : string,
    reviewInfos : ReviewInfo[]
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
    taskServiceInfo : TaskServiceInfo[],
    reviewInfos : ReviewInfo[],
}

export interface CreateServiceDto
{
    failureDescription : string,
    picture? : string
    startDate : string,
}

export interface DeclineServiceDto
{
    razlogOdbijanja : string
}