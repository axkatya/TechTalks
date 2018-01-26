import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpsertTalkComponent } from './upsert-talk.component';
import { TalkService } from '../../services/talk.service';
import { ActivatedRoute } from '@angular/router';

describe('UpsertTalkComponent', () => {
    let component: UpsertTalkComponent;
    let fixture: ComponentFixture<UpsertTalkComponent>;

    beforeEach(() => {

        let talkServiceStub = {

        };

        TestBed.configureTestingModule({
            declarations: [UpsertTalkComponent],
            providers: [{ provide: TalkService, useValue: talkServiceStub }]
        })
            .compileComponents();
    });

    beforeEach(() => {
        fixture = TestBed.createComponent(UpsertTalkComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
