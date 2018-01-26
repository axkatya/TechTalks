import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpsertSpeakerComponent } from './upsert-speaker.component';
import { SpeakerService } from '../../services/speaker.service';
import { ActivatedRoute } from '@angular/router';

describe('UpsertSpeakerComponent', () => {
    let component: UpsertSpeakerComponent;
    let fixture: ComponentFixture<UpsertSpeakerComponent>;

    beforeEach(() => {

        let speakerServiceStub = {

        };

        TestBed.configureTestingModule({
            declarations: [UpsertSpeakerComponent],
            providers: [{ provide: SpeakerService, useValue: speakerServiceStub }]
        })
            .compileComponents();
    });

    beforeEach(() => {
        fixture = TestBed.createComponent(UpsertSpeakerComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
