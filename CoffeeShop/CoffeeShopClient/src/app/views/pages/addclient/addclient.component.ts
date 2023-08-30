import { Component, OnInit } from '@angular/core';
import { HttpClient } from  '@angular/common/http';
import { ClientRequest } from './clientrequest-model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-addclient',
  templateUrl: './addclient.component.html',
  styleUrls: ['./addclient.component.css']
})
export class AddclientComponent implements OnInit  {

  clientRequest: ClientRequest = new ClientRequest()
  
  constructor(private http: HttpClient, private router: Router){

  }

  ngOnInit(): void {
  
    //this.addClient()
 }


 async addClient(){

  console.log(this.clientRequest);
  await this.http.post<any>("https://localhost:5001/api/Client/create", this.clientRequest).subscribe(
    (response) => {

      if(response.status == 'success'){
        this.router.navigate(['/clients'])
      }
     
      console.log(response );
    }
  )

}

}
