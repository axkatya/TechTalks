import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TalkComponent } from './talkdata.component';
import { TalkFilterViewModelService } from '../../services/talkFilterViewModel.service';
import { TalkService } from '../../services/talk.service';
import { Router } from '@angular/router';

describe('TalkComponent', () => {
    let component: TalkComponent;
    let fixture: ComponentFixture<TalkComponent>;

    beforeEach(() => {

        let talkServiceStub = {

        };

        let talkFilterViewModelServiceStub = {

        };

        TestBed.configureTestingModule({
            declarations: [TalkComponent],
            providers: [
                { provide: TalkService, useValue: talkServiceStub },
                { provide: TalkFilterViewModelService, useValue: talkFilterViewModelServiceStub }]
        })
            .compileComponents();
    });

    beforeEach(() => {
        fixture = TestBed.createComponent(TalkComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
