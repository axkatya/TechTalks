import { Component } from '@angular/core';
import { Talk } from '../../models/talk';
import { Speaker } from '../../models/speaker';
import { TalkService } from '../../services/talk.service';
import { TalkFilterViewModelService } from '../../services/talkFilterViewModel.service';
import { ActivatedRoute, Router} from '@angular/router';
import { UpsertSpeakerComponent } from '../upsertSpeaker/upsert-speaker.component';

@Component({
    selector: 'upsert-talk',
    templateUrl: './upsert-talk.component.html'
})

export class UpsertTalkComponent {
    private talk: Talk;
    private disciplineList: string[];
    private locationList: string[];
    private speakerList: string[];
    private isNewSpeaker: boolean = false;

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
        private _talkFilterViewModelService: TalkFilterViewModelService
    ) {
        this.talk = new Talk();
    }

    ngOnInit() {
        this._route.params.subscribe(params => {
            var id: number = params['id'];
            if (id > 0) {
                this._talkService.getTalkById(id).subscribe(result => {
                    this.talk = result;
                });
            }

            this._talkFilterViewModelService.getPossibleLists().subscribe(result => {
                this.disciplineList = result.disciplineList;
                this.locationList = result.locationList;
            });
        });
    }

    onSave() {
        var res: any;

        if (this.talk.talkId > 0) {
            this._talkService.updateTalk(this.talk).subscribe(result => { res = result; });
        }
        else {
            this._talkService.createTalk(this.talk).subscribe(result => { res = result; });
        }

        this._router.navigate(['/talk-data']);
    }

    onCreateNewSpeaker() {
        this.isNewSpeaker = true;
    }

    onNotifyFromUpsertSpeaker(speaker: Speaker) {
        this.isNewSpeaker = false;
        this.talk.speakerName = speaker.firstName + ' ' + speaker.lastName;
    }

    onTalkDateSelect(date: Date) {
        this.talk.talkDate = date;
    }
}






