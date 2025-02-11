import { Equipment, EquipmentInfo } from "./equipmentDTO";
import { Motor, MotorInfo } from "./motorDTO";
import { User, UserInfo } from "./userDTO";

export interface Order
{
    id: string;
    orderAmount: number;
    totalPrice: string;
    orderStatus: string;
    equipments?: Equipment[];
    motorcycles?: Motor[];
    userId: string;
    users?: User;
}

export interface OrderInfo
{
    id: string;
    orderAmount: number;
    totalPrice: string;
    orderStatus: string;
    equipments?: EquipmentInfo[];
    motorcycles?: MotorInfo[];
    userId: string;
    users?: UserInfo;
}


export interface CreateOrderDto
{
    orderAmount: number;
}