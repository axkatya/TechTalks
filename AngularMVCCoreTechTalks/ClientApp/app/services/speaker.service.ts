import { Injectable, Inject } from '@angular/core';
import { Http, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Rx';
import { Speaker } from '../models/speaker';

@Injectable()
export class SpeakerService {

    private baseUrl: string;
    private headers: Headers;
    private options: RequestOptions;

    constructor(private http: Http, @Inject('BASE_URL') baseUrl: string) {
        this.baseUrl = baseUrl;

        this.headers = new Headers();
        this.headers.append('Content-Type', 'application/json');
        this.options = new RequestOptions({ headers: this.headers });
    }

    getSpeakerById(id: number) {
        return this.http.get(this.baseUrl + 'api/Speaker/GetSpeakerById/'+ id)
            .map(result => result.json() as Speaker);
    }

    getSpeakers() {
        return this.http.get(this.baseUrl + 'api/Speaker/GetSpeakers')
            .map(result => result.json() as Speaker[]);
    }

    createSpeaker(speaker: Speaker) {
        return this.http.post(this.baseUrl + 'api/Speaker/CreateSpeaker', speaker, this.options)
            .map(result => result as any);
    }

    updateSpeaker(speaker: Speaker) {
        return this.http.put(this.baseUrl + 'api/Speaker/UpdateSpeaker/' + speaker.speakerId, speaker, this.options)
            .map(result => result as any);
    }
}
