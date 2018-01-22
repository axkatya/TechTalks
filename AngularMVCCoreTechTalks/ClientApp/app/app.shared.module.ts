import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { Ng2SmartTableModule } from 'ng2-smart-table';
import * as $ from 'jquery';
import { NKDatetimeModule } from 'ng2-datetime/ng2-datetime'

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { TalkComponent } from './components/talkdata/talkdata.component';
import { SpeakerComponent } from './components/speaker/speaker.component';
import { UpsertTalkComponent } from './components/upserttalk/upsertTalk.component';

import { SpeakerButtonRenderComponent } from './components/button-render/speaker.button-render.component';

import { TalkFilterViewModelService } from './components/talkFilterViewModel.service';
import { TalkService } from './components/talk.service';
import { SpeakerService } from './components/speaker.service';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,
        SpeakerButtonRenderComponent,
        TalkComponent,
        SpeakerComponent,
        UpsertTalkComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'talk-data', component: TalkComponent },
            { path: 'speaker', component: SpeakerComponent },
            { path: 'speaker/:id', component: SpeakerComponent},
            { path: 'upsert-talk/:id', component: UpsertTalkComponent },
            { path: '**', redirectTo: 'home' }
        ]),
        Ng2SmartTableModule,
        NKDatetimeModule
    ],
    providers: [
        TalkFilterViewModelService,
        TalkService,
        SpeakerService
    ],
    entryComponents: [
        SpeakerButtonRenderComponent
    ]
})
export class AppModuleShared {
}
