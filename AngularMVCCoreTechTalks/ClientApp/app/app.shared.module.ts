import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { Ng2SmartTableModule } from 'ng2-smart-table';
import * as $ from 'jquery';
import { NKDatetimeModule } from 'ng2-datetime/ng2-datetime'
import { SelectModule } from 'ng2-select';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { TalksDataComponent } from './components/talksdata/talksdata.component';
import { SpeakersDataComponent } from './components/speakersdata/speakersdata.component';
import { SpeakerComponent } from './components/speaker/speaker.component';
import { UpsertTalkComponent } from './components/upsertTalk/upsert-talk.component';
import { UpsertSpeakerComponent } from './components/upsertSpeaker/upsert-speaker.component';

import { SpeakerButtonRenderComponent } from './components/button-render/speaker.button-render.component';

import { TalkFilterViewModelService } from './services/talkFilterViewModel.service';
import { TalkService } from './services/talk.service';
import { SpeakerService } from './services/speaker.service';
import { DisciplineService } from './services/discipline.service';


@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        SpeakerButtonRenderComponent,
        TalksDataComponent,
        SpeakersDataComponent,
        SpeakerComponent,
        UpsertTalkComponent,
        UpsertSpeakerComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'talk-data', pathMatch: 'full' },
            { path: 'talks-data', component: TalksDataComponent },
            { path: 'speakers-data', component: SpeakersDataComponent },
            { path: 'speaker/:id', component: SpeakerComponent },
            { path: 'upsert-talk', component: UpsertTalkComponent },
            { path: 'upsert-talk/:id', component: UpsertTalkComponent },
            { path: 'upsert-speaker', component: UpsertSpeakerComponent },
            { path: 'upsert-speaker/:id', component: UpsertSpeakerComponent },
            { path: '**', redirectTo: 'talk-data' }
        ]),
        Ng2SmartTableModule,
        NKDatetimeModule,
        SelectModule
    ],
    providers: [
        TalkFilterViewModelService,
        TalkService,
        SpeakerService,
        DisciplineService
    ],
    entryComponents: [
        SpeakerButtonRenderComponent
    ]
})
export class AppModuleShared {
}
