import { Component, OnInit } from '@angular/core';
import { Speaker } from '../../models/speaker';
import { SpeakerService } from '../../services/speaker.service';
import { LocalDataSource } from 'ng2-smart-table';
import { Router } from '@angular/router';

@Component({
    selector: 'speakersdata',
    templateUrl: './speakersdata.component.html',
    styleUrls: ['../app/app.component.less']
})

export class SpeakersDataComponent {

    private source: LocalDataSource;
    private possibleRowsPerPage: number[];
    private rowsPerPage: number;
    private speakerForUpdating: Speaker;
    private isUpdateSpeaker: boolean = false;
    private speakersDataSettings = {
        columns: {
            firstName: {
                title: 'First Name'
            },
            lastName: {
                title: 'Last Name'
            },
            email: {
                title: 'Email'
            },
            position: {
                title: 'Position'
            },
            department: {
                title: 'Department'
            },
            location: {
                title: 'Location'
            }
        },
        actions: {
            add: false,
            edit: false,
            custom: [{
                name: 'edit',
                title: 'Edit'
            }
            ],
            delete: false
        }
    };

    constructor(private _speakerService: SpeakerService
    ) {
        this.possibleRowsPerPage = [20, 50];
        this.rowsPerPage = 20;
    }

    ngOnInit() {
        this.source = new LocalDataSource();
        this.getSpeakers();
    }

    getSpeakers() {
        this._speakerService.getSpeakers().subscribe(result => {
            this.source.setPaging(1, this.rowsPerPage, true);
            this.source.load(result);
            this.source.refresh();
        });
    }

    onAddNewSpeaker() {
        this.speakerForUpdating = new Speaker();
        this.isUpdateSpeaker = true;
    }

    onCustomEditSpeaker(event: any) {
        this.speakerForUpdating = Object.assign({}, event.data);
        this.isUpdateSpeaker = true;
    }

    onNotifyFromUpsertSpeaker(speaker: Speaker) {
        this.isUpdateSpeaker = false;
        if (speaker.isUpdated) {
            this.getSpeakers();
        }
    }
}