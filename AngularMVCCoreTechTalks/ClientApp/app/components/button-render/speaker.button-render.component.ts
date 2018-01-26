import { Component, Input } from '@angular/core';
import { ViewCell } from 'ng2-smart-table';
import { Talk } from '../../models/talk';
import { Router } from '@angular/router';

@Component({
    selector: 'speaker-buttonrenderdata',
    templateUrl: './button-render.component.html'
})
export class SpeakerButtonRenderComponent implements ViewCell {

    @Input() value: any;
    @Input() rowData: Talk;

    constructor(private _router: Router) { }

    onClick() {
        this._router.navigate(['/speaker', this.rowData.speakerId]);
    }
}