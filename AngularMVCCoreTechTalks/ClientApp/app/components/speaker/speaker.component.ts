﻿import { Component, OnInit } from '@angular/core';
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

    constructor(
        private _route: ActivatedRoute,
        private _speakerService: SpeakerService) {

    }

    ngOnInit() {
        this._route.params.subscribe(params => {
            var id: number = params['id'];
            this._speakerService.getSpeakerById(id).subscribe(result => {
                this.speaker = result;
            });
        });
    }
}






