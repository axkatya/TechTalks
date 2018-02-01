import { Injectable, Inject } from '@angular/core';
import { Http, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Rx';
import { Talk } from '../models/talk';
import { SelectedTalkFilterViewModel } from '../models/selectedTalkFilterViewModel';

@Injectable()
export class TalkService {

    private baseUrl: string;     
    private headers : Headers;
    private options: RequestOptions;

    constructor(private http: Http, @Inject('BASE_URL') baseUrl: string) {
        this.baseUrl = baseUrl;

        this.headers = new Headers();
        this.headers.append('Content-Type', 'application/json');
        this.options = new RequestOptions({ headers: this.headers });
    }

    getFilteredTalks(selectedTalkFilterViewModel: SelectedTalkFilterViewModel) {
        return this.http.post(this.baseUrl + 'api/Talk/GetFilteredTalks', selectedTalkFilterViewModel)
            .map(result => result.json() as Talk[]);
    }

    getTalkById(talkId: number) {
        return this.http.get(this.baseUrl + 'api/Talk/GetTalkById/' + talkId)
            .map(result => result.json() as Talk);
    }

    createTalk(talk: Talk) {
        return this.http.post(this.baseUrl + 'api/Talk/CreateTalk', talk, this.options)
            .map(result => result as any);
    }

    updateTalk(talk: Talk) {
        return this.http.put(this.baseUrl + 'api/Talk/UpdateTalk/' + talk.talkId, talk, this.options)
            .map(result => result as any);
    }

    deleteTalk(talkId: number) {
        return this.http.delete(this.baseUrl + 'api/Talk/DeleteTalk/' + talkId, this.options)
            .map(result => result as any);
    }
}
