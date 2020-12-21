import { HttpModule } from '@angular/http';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { Inject, NgZone } from '@angular/core';

import { AppComponent } from './app.component';
@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    HttpModule
  ],
  bootstrap: [AppComponent]
})
ngOnInit(@Inject(NgZone) private zone: NgZone) {
  zone.runOutsideAngular(()=>{
    //call function
    console.log("init function");
  });
}
export class AppModule { }