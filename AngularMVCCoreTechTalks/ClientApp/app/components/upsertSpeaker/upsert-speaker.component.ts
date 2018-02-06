import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Speaker } from '../../models/speaker';
import { SpeakerService } from '../../services/speaker.service';
import { ActivatedRoute } from '@angular/router';
import { NG_VALIDATORS, Validator, ValidationErrors } from '@angular/forms';

@Component({
    selector: 'upsert-speaker',
    templateUrl: './upsert-speaker.component.html',
    styleUrls: ['../app/app.component.less']
})

export class UpsertSpeakerComponent implements OnInit {
    @Input() updatedSpeaker: Speaker;
    @Output() notify: EventEmitter<Speaker> = new EventEmitter<Speaker>();

    private speaker: Speaker;
    private errors: string[];

    constructor(private _route: ActivatedRoute, private _speakerService: SpeakerService) {
        this.errors = [];
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
            this._speakerService.updateSpeaker(this.speaker).subscribe(
                (result) => {
                    res = result;
                    this.notify.emit(this.speaker);
                },
                (err) => {
                    this.processErrors(err);
                }
            );
        }
        else {
            this._speakerService.createSpeaker(this.speaker).subscribe(
                (result) => {
                    res = result.json() as Speaker;
                    this.speaker.speakerId = res.speakerId;
                    this.notify.emit(this.speaker);
                },
                (err) => {
                    this.processErrors(err);
                }
            );
        }
    }

    onCancel() {
        this.speaker.isUpdated = false;
        this.notify.emit(this.speaker);
    }

    processErrors(err: any) {
        if (err.status === 400) {
            // handle validation error
            let validationErrorDictionary = JSON.parse(err.text());
            for (var fieldName in validationErrorDictionary) {
                if (validationErrorDictionary.hasOwnProperty(fieldName)) {
                    this.errors.push(validationErrorDictionary[fieldName]);
                }
            }
        } else {
            this.errors.push("something went wrong!");
        }
    }
}