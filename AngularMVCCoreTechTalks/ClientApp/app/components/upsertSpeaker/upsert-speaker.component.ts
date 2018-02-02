import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Speaker } from '../../models/speaker';
import { SpeakerService } from '../../services/speaker.service';

@Component({
    selector: 'upsert-speaker',
    templateUrl: './upsert-speaker.component.html',
    styleUrls: ['../app/app.component.less']
})

export class UpsertSpeakerComponent implements OnInit {
    @Input() updatedSpeaker: Speaker;
    @Output() notify: EventEmitter<Speaker> = new EventEmitter<Speaker>();

    private speaker: Speaker;

    constructor(private _speakerService: SpeakerService) {

    }

    ngOnInit(): void {
        if (this.updatedSpeaker === undefined) {
            this.speaker = new Speaker();
        }
        else {
            this.speaker = this.updatedSpeaker;
        }
    }

    onSave() {
        var res: any;
        this.speaker.isUpdated = true;
        if (this.speaker.speakerId > 0) {
            this._speakerService.updateSpeaker(this.speaker).subscribe(result => {
                res = result;
                this.notify.emit(this.speaker);
            });
        }
        else {
            this._speakerService.createSpeaker(this.speaker).subscribe(result => {
                res = result.json() as Speaker;
                this.speaker.speakerId = res.speakerId;
                this.notify.emit(this.speaker);
            });
        }
    }

    onCancel() {
        this.speaker.isUpdated = false;
        this.notify.emit(this.speaker);
    }
}