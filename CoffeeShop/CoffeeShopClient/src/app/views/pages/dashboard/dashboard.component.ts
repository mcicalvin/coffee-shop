import { Component, OnInit } from '@angular/core';
import { Coffee } from './coffee.model';
import { HttpClient } from  '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  coffees: Coffee[] = []
  cartItemCount: number = 0

  cartItems: Coffee[] = []

  constructor(private http: HttpClient){

  }

  ngOnInit(): void {
  
     this.getCoffees()
  }

  async getCoffees(){

    await this.http.get<any>("https://localhost:5001/api/Coffee/filter").subscribe(
      (response) => {

        if(response.status == 'success'){
          this.coffees = response.data
        }
       
        console.log(this.coffees );
      }
    )

  }

  addToCart(coffee: Coffee){
   
    localStorage.removeItem("cart");
    
    if(this.isInTheCart(coffee.id)){
      this.updateCart(coffee);
    }else{
      if(coffee.qty == null || coffee.qty <= 0){
        coffee.qty = 1
      }
      this.cartItems.push(coffee)
    
    }
   
    console.log("Qty: " + coffee.qty)
    this.countCartItems()

    localStorage.setItem('cart', JSON.stringify(this.cartItems));
  }

  updateCart(coffee: Coffee){
    for(var i = 0; i< this.cartItems.length; i++){

      if(coffee.id == this.cartItems[i].id){
        this.cartItems[i].qty = coffee.qty
      }
    }
  }

  isInTheCart(id: number) {

    for(var i = 0; i< this.cartItems.length; i++){

      if(id == this.cartItems[i].id){
        return true;
      }
    }

    return false;
  }

  countCartItems(){
    this.cartItemCount = 0

    this.cartItems.forEach((element) => {
      this.cartItemCount += element.qty
    })
    
  }

}
