///<reference path="../../typings/globals/core-js/index.d.ts"/>
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

// Custom component
import { Angular2SmallComponent } from './app';

@NgModule({
    imports: [BrowserModule],
    declarations: [Angular2SmallComponent],
    bootstrap: [Angular2SmallComponent]
})
export class AppModule { }