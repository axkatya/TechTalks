import { Component, OnInit } from '@angular/core';
import { Speaker } from '../speaker';
import { SpeakerService } from '../speaker.service';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'speakerdata',
    templateUrl: './speaker.component.html'
})

export class SpeakerComponent {
    private baseUrl: string;
    private speaker: Speaker;
    private isUpdateSpeaker: boolean = false;

    constructor(
        private _route: ActivatedRoute,
        private _speakerService: SpeakerService) {
        this.speaker = new Speaker();
    }

    ngOnInit() {
        this._route.params.subscribe(params => {
            var id: number = params['id'];
            this._speakerService.getSpeakerById(id).subscribe(result => {
                this.speaker = result;
            });
        });
    }

    onUpdateSpeaker() {
        this.isUpdateSpeaker = true;
    }

    onNotifyFromUpsertSpeaker(speaker: Speaker) {
        this.isUpdateSpeaker = false;
        this.speaker = speaker;
    }
}






