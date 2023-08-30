import { ItemRequest } from "./itemrequest.model"

export class SaleRequest{
   
    idNumber: string = ''
    coffeeItems: ItemRequest[] = []


    constructor(idNumber = '', coffeeItems : ItemRequest[] = []) {
        this.idNumber = idNumber;
        this.coffeeItems = coffeeItems;
    }
}