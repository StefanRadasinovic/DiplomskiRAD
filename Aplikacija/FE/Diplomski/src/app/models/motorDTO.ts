import { CreateProducerDto, Producer, ProducerInfo, UpdateProducerDto } from "./producerDTO";

export interface Motor
{
    id : string,
    name : string,
    slika? : string
    kilometraza : number,
    yearOfProduction : number,
    motorcycleState: string,
    amount : number,
    motorcycleType : string,
    producers: Producer[],
}

export interface MotorInfo
{
    id : string,
    name : string,
    slika? : string
    yearOfProduction : number,
    motorcycleType : string,
    producers: ProducerInfo[],
}

export interface PaginatedMotorProps { //Za Paginaciju
    data: Array<MotorInfo>;
    pageNumber: number;
    pageSize: number;
    totalPages: number;
    totalRecord: number;
}


export interface CreateMotorDto
{
    id : string,
    name : string,
    slika? : string
    kilometraza : number,
    yearOfProduction : number,
    motorcycleState: string,
    amount : number,
    motorcycleType : string,
    producers: CreateProducerDto[],

}

export interface UpdateMotorDto 
{
    name : string,
    Slika? : string
    kilometraza : number,
    yearOfProduction : number,
    motorcycleState: string,
    amount : number,
    motorcycleType : string,
    producers: UpdateProducerDto[],

}