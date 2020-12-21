import { Injectable } from '@angular/core';
import { Http, Response } from "@angular/http";
import 'rxjs/add/operator/map';
import { Observable } from "rxjs/Observable";

@Injectable()
export class HttpTestService {
  constructor(private _http : Http) { }
    getCurrentTime(){
              return this._http.get('http://date.jsontest.com')  
             .map(res=> res.json()); 
    }
}