
export class ItemRequest{
   
    id: number = 0
    qty: number = 0

    /**
     *
     */
    constructor(id: number, qty: number) {
        this.id = id;
        this.qty = qty;
    }
}