import { Component } from '@angular/core';
import { Talk } from '../../models/talk';
import { Speaker } from '../../models/speaker';
import { Discipline } from '../../models/discipline';
import { TalkService } from '../../services/talk.service';
import { TalkFilterViewModelService } from '../../services/talkFilterViewModel.service';
import { DisciplineService } from '../../services/discipline.service';
import { ActivatedRoute, Router } from '@angular/router';
import { UpsertSpeakerComponent } from '../upsertSpeaker/upsert-speaker.component';
import { NG_VALIDATORS, Validator, ValidationErrors } from '@angular/forms';

@Component({
    selector: 'upsert-talk',
    templateUrl: './upsert-talk.component.html',
    styleUrls: ['../app/app.component.less']
})

export class UpsertTalkComponent {
    private talk: Talk;
    private disciplineList: Discipline[];
    private locationList: string[];
    private speakerList: string[];
    private isNewSpeaker: boolean = false;

    private locationItems: Array<any>;
    private selectedLocation: any;

    private disciplineItems: Array<any>;
    private selectedDiscipline: any;
    private errors: string[];

    private dateSettings = {
        autoclose: true,
        todayBtn: 'linked',
        todayHighlight: true,
        assumeNearbyYear: true,
        format: 'dd-MM-yyyy'
    }
    constructor(
        private _route: ActivatedRoute,
        private _router: Router,
        private _talkService: TalkService,
        private _talkFilterViewModelService: TalkFilterViewModelService,
        private _disciplineService: DisciplineService
    ) {
        this.talk = new Talk();
        this.errors = [];
    }

    ngOnInit() {
        this._route.params.subscribe(params => {
            var id: number = params['id'];
            if (id > 0) {
                this._talkService.getTalkById(id).subscribe(result => {
                    this.talk = result;
                    this.talk.talkDate = new Date(result.talkDate);
                });
            }

            this._talkFilterViewModelService.getPossibleLists().subscribe(result => {
                this.disciplineList = result.disciplineList;
                this.locationList = result.locationList;

                this.locationItems = new Array();
                for (let i = 0; i < this.locationList.length; i++) {
                    this.locationItems[i] = {
                        id: this.locationList[i],
                        text: this.locationList[i]
                    };
                }

                this.selectedLocation = [{ id: this.talk.location, text: this.talk.location }];

                this.disciplineItems = new Array();
                for (let i = 0; i < this.disciplineList.length; i++) {
                    this.disciplineItems[i] = {
                        id: this.disciplineList[i].disciplineId,
                        text: this.disciplineList[i].disciplineName
                    };
                }

                this.selectedDiscipline = [{ id: this.talk.disciplineId, text: this.talk.disciplineName }];
            });
        });
    }

    onCancel() {
        this._router.navigate(['/talk-data']);
    }

    onSave() {
        var res: any;
        var createdDiscipline: Discipline;

        if (!this.checkNestedDisciplineName(this.disciplineList, this.talk.disciplineName)) {
            var discipline: Discipline = { disciplineId: 0, disciplineName: this.talk.disciplineName };
            this._disciplineService.createDiscipline(discipline).subscribe(result => {
                createdDiscipline = result
                this.talk.disciplineId = createdDiscipline.disciplineId;
            });
        }

        if (this.talk.talkId > 0) {
            this._talkService.updateTalk(this.talk).subscribe(
                (result) => {
                    res = result;
                },
                (err) => {
                    this.processErrors(err);
                });
        }
        else {
            this._talkService.createTalk(this.talk).subscribe(
                (result) => { res = result; },
                (err) => {
                    this.processErrors(err);
                });
        }

        this._router.navigate(['/talk-data']);
    }

    onCreateNewSpeaker() {
        this.isNewSpeaker = true;
    }

    onNotifyFromUpsertSpeaker(speaker: Speaker) {
        this.isNewSpeaker = false;

        if (speaker.isUpdated) {
            this.talk.speakerName = speaker.firstName + ' ' + speaker.lastName;
            this.talk.speakerId = speaker.speakerId;
        }
    }

    onTalkDateSelect(date: Date) {
        this.talk.talkDate = date;
    }

    onAdditionalDetailChange(detail: any) {
        try {
            this.talk.additionalDetail = detail.target.value;
        } catch (e) {
            console.info('could not set additional details');
        }
    }

    onSelectLocation(location: any) {
        this.talk.location = location.text;
    }

    onTypedLocation(location: any) {
        this.talk.location = location;
        this.selectedLocation = [{ id: this.talk.location, text: this.talk.location }];
    }

    onSelectDiscipline(discipline: any) {
        this.talk.disciplineId = discipline.id;
        this.talk.disciplineName = discipline.text;
    }

    onTypedDiscipline(discipline: any) {
        this.talk.disciplineName = discipline;
        this.selectedDiscipline = [{ id: 0, text: this.talk.disciplineName }];
    }

    checkNestedDisciplineName(arr: Discipline[], input: string) {
        return arr.some(function (discipline) {
            return (discipline.disciplineName.toLowerCase() === input.toLowerCase())
        });
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






