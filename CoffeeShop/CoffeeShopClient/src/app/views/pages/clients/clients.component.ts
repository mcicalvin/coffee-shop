import { Component, OnInit } from '@angular/core';
import { HttpClient } from  '@angular/common/http';
import { Injectable } from '@angular/core';
import { Client } from './client.model';

@Component({
  selector: 'app-clients',
  templateUrl: './clients.component.html',
  styleUrls: ['./clients.component.css']
})
export class ClientsComponent implements OnInit  {

  
  constructor(private http: HttpClient){

  }

  clients: Client[] = []
  totalSum : number = 0

  ngOnInit(): void {
  
    this.getClients()
 }

  async getClients(){

    await this.http.get<any>("https://localhost:5001/api/Client/filter").subscribe(
      (response) => {

        if(response.status == 'success'){
          this.clients = response.data
          this.totalSum = response.totalSum
        }
       
        console.log(this.clients );
      }
    )

  }
}


