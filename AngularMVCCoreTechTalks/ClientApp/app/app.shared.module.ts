import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { Ng2SmartTableModule } from 'ng2-smart-table';
import * as $ from 'jquery';
import { AngularDateTimePickerModule } from 'angular2-datetimepicker';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { TalkComponent } from './components/talkdata/talkdata.component';
import { TalkFilterViewModelService } from './components/talkFilterViewModel.service';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,
        TalkComponent
        
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'talk-data', component: TalkComponent },
            { path: '**', redirectTo: 'home' }
        ]),
        Ng2SmartTableModule,
        AngularDateTimePickerModule
    ],
    providers: [
        TalkFilterViewModelService
    ]
})
export class AppModuleShared {
}
