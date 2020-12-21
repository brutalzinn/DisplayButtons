
import { HttpTestService } from './http-test.service';
import { Component } from '@angular/core';
@Component({
  selector: 'app-root',
  template: `
           <p>
          <Label>Acessando dados de : http://date.jsontest.com com get()</Label>  
          </p>
          <button (click)="onTestGet()">Teste GET Request</button>
          <p>Resultado : {{getData}}</p>
  ` ,
  providers : [HttpTestService] 
})
export class AppComponent {
   getData :  string;
   constructor(private httpService : HttpTestService){}
   onTestGet() {
       this.httpService.getCurrentTime()
       .subscribe(
          data => this.getData = JSON.stringify(data),
          error => alert(error),
          () => console.log("acesso a webapi get ok...")
       );
   }