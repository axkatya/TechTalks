import { Component } from '@angular/core';
import { Talk } from '../talk';
import { TalkService } from '../talk.service';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'upsertTalkData',
    templateUrl: './upserttalk.component.html'
})

export class UpsertTalkComponent {
    private baseUrl: string;
    private talk: Talk;
    private disciplineList: string[];
    private locationList: string[];
    private speakerList: string[];

    private dateSettings = {
        autoclose: true,
        todayBtn: 'linked',
        todayHighlight: true,
        assumeNearbyYear: true,
        format: 'dd-MM-yyyy'
    }

    constructor(private _route: ActivatedRoute, private _talkService: TalkService) {
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

    ngSave() {
        this._talkService.upsert(this.talk);        
    }
}






