import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TalksDataComponent } from './talksdata.component';
import { TalkFilterViewModelService } from '../../services/talkFilterViewModel.service';
import { TalkService } from '../../services/talk.service';
import { Router } from '@angular/router';

describe('TalkComponent', () => {
    let component: TalksDataComponent;
    let fixture: ComponentFixture<TalksDataComponent>;

    beforeEach(() => {

        let talkServiceStub = {

        };

        let talkFilterViewModelServiceStub = {

        };

        TestBed.configureTestingModule({
            declarations: [TalksDataComponent],
            providers: [
                { provide: TalkService, useValue: talkServiceStub },
                { provide: TalkFilterViewModelService, useValue: talkFilterViewModelServiceStub }]
        })
            .compileComponents();
    });

    beforeEach(() => {
        fixture = TestBed.createComponent(TalksDataComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
