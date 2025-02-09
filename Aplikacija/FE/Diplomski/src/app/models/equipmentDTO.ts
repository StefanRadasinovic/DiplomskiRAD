import { DisplayPriceOnly } from "./priceListDTO";
import { CreateProducerDto, Producer, ProducerInfo, UpdateProducerDto } from "./producerDTO";

export interface Equipment
{
    id : string,
    name : string,
    slika? : string,
    equipmentState : string,
    amount : number,
    producers: Producer[]
    displayPriceOnly?: DisplayPriceOnly[],
}

export interface EquipmentInfo
{
    id : string,
    name : string,
    slika? : string,
    producers: ProducerInfo[],
    displayPriceOnly?: DisplayPriceOnly[],
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


export interface JustEquipmentName
{
    id : string,
    name : string,
    producers: ProducerInfo[],

}


