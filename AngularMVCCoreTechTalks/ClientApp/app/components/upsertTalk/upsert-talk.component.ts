import { Component } from '@angular/core';
import { Talk } from '../talk';
import { Speaker } from '../speaker';
import { TalkService } from '../talk.service';
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
        private _talkService: TalkService
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
        });
    }

    onSave() {
        this._talkService.upsert(this.talk);
        this._router.navigate(['/talk-data']);
    }

    onCreateNewSpeaker() {
        this.isNewSpeaker = true;
    }

    onNotifyFromUpsertSpeaker(speaker: Speaker) {
        this.isNewSpeaker = false;
        this.talk.speakerName = speaker.firstName + ' ' + speaker.lastName;
    }
}






