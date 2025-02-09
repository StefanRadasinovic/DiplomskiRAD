import { Equipment, JustEquipmentName } from "./equipmentDTO";
import { JustMotorcycleName, Motor } from "./motorDTO";


export interface PriceList {
    id: string;
    price: number;
    startingDate: string;
    endingDate: string;
    motorcycles?: Motor[];
    equipments?: Equipment[];
}

export interface PriceListInfo {
    id: string;
    price: number;
    startingDate: string;
    endingDate: string;
    justMotorcycleNames?: JustMotorcycleName[];
}


export interface CreatePriceListDto {
    price: number;
    startingDate: string;
    endingDate: string;
}

export interface UpdatePriceListDto {
    id: string;
    price: number;
    startingDate: Date;
    endingDate: Date;
}

export interface DisplayPriceOnly {
    price: number;
}

export interface PriceListInfo22 {
    id: string;
    price: number;
    startingDate: string;
    endingDate: string;
    justEquipmentName?: JustEquipmentName[];
}


export interface CustomPriceListInfo {
    id: string;
    price: number;
    startingDate: string;
    endingDate: string;
    items: any[]; // Supports both 
}
