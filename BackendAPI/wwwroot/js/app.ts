﻿var app = angular.module("application", []);
app.controller("testController", function($scope, $http) {

import {Package} from './package';
import {Injectable} from '@angular/core';
import {Http, Headers, BaseRequestOptions, } from '@angular/http';
import 'rxjs/add/operator/map';
import {Subject} from 'rxjs/Subject';
import {BehaviorSubject} from 'rxjs/BehaviorSubject';
import {Observable} from 'rxjs/Observable';

@Injectable()
export class PackageService {
	packageData: Subject<Array<Package>> = new BehaviorSubject<Array<Package>>([]);

	constructor(private http: Http) {

	}
	
	loadAllPackages () {
		this.http
		.get('http://date.jsontest.com')
		.map((res: any) => {
		console.log(res.json);
			return res.json();
		
		})
		.subscribe (
			(data: any) => {
				this.packageData.next(data);
			},
			(err: any) => console.error("loadAllPackages: ERROR"),
			() => console.log("loadAllPackages: always")
		);
	}
	loadAllPackages()
	  

}

});