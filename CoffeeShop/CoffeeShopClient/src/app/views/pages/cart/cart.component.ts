import { Component, OnInit } from '@angular/core';
import { Coffee } from '../dashboard/coffee.model';
import { HttpClient } from  '@angular/common/http';
import { Router } from '@angular/router';
import { SaleRequest } from './salerequest.model';
import { ItemRequest } from './itemrequest.model';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})


export class CartComponent implements OnInit {

  cartItems: Coffee[] = []
  idNumber : string = ''
  saleRequest: SaleRequest = new SaleRequest()

  constructor(private http: HttpClient, private router: Router){

  }

  ngOnInit(): void {
   
    this.loadCartFromLocalStorage();

  }

  async purchase(){

    var saleRequest = this.buildSaleRequest();

    await this.http.post<any>("https://localhost:5001/api/Sale/create", saleRequest).subscribe(
      (response) => {
  
        if(response.status == 'success'){
          this.router.navigate(['/clients'])
        }
       
        console.log(response );
      }
    )
  }

  buildSaleRequest(){

    var items : ItemRequest[] = []
    
    this.cartItems.forEach((element) => {

      var item = new ItemRequest(element.id, element.qty);
     
      items.push(item)
    })
    
    var request = new SaleRequest(this.idNumber, items)

    return request;
  }

  loadCartFromLocalStorage() {
    this.cartItems = JSON.parse(localStorage.getItem('cart')!);
 }
}



