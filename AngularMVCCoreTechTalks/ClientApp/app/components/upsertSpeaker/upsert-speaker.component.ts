import { Component, Output, EventEmitter } from '@angular/core';
import { Speaker } from '../speaker';
import { SpeakerService } from '../speaker.service';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'upsert-speaker',
    templateUrl: './upsert-speaker.component.html'
})

export class UpsertSpeakerComponent {
    @Output() notify: EventEmitter<Speaker> = new EventEmitter<Speaker>();

    private speaker: Speaker;

    constructor(
        private _route: ActivatedRoute,
        private _speakerService: SpeakerService
    ) {
        this.speaker = new Speaker();
    }

    ngOnInit() {
        this._route.params.subscribe(params => {
            var id: number = params['id'];
            if (id > 0) {
                this._speakerService.getSpeakerById(id).subscribe(result => {
                    this.speaker = result;
                });
            }
        });
    }

    onSave() {
        this._speakerService.upsert(this.speaker);
        this.notify.emit(this.speaker);
    }
}






