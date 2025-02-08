import { CreateProducerDto, Producer, ProducerInfo, UpdateProducerDto } from "./producerDTO";

export interface Equipment
{
    id : string,
    name : string,
    slika? : string,
    equipmentState : string,
    amount : number,
    producers: Producer[],
}

export interface EquipmentInfo
{
    id : string,
    name : string,
    slika? : string,
    producers: ProducerInfo[],
}

export interface PaginatedEquipmentProps { //Za Paginaciju
    data: Array<EquipmentInfo>;
    pageNumber: number;
    pageSize: number;
    totalPages: number;
    totalRecord: number;
}

export interface CreateEquipmentDto
{
    name : string,
    slika? : string,
    equipmentState : string,
    amount : number,
    producers: CreateProducerDto[],
}

export interface UpdateEquipmentDto
{
    name : string,
    slika? : string,
    equipmentState : string,
    amount : number,
     producers: UpdateProducerDto[],
}


