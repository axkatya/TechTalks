import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SpeakerComponent } from './speaker.component';
import { SpeakerService } from '../../services/speaker.service';
import { ActivatedRoute } from '@angular/router';

describe('SpeakerComponent', () => {
    let component: SpeakerComponent;
    let fixture: ComponentFixture<SpeakerComponent>;

    beforeEach(() => {

        let speakerServiceStub = {

        };

        TestBed.configureTestingModule({
            declarations: [SpeakerComponent],
            providers: [{ provide: SpeakerService, useValue: speakerServiceStub }]
        })
            .compileComponents();
    });

    beforeEach(() => {
        fixture = TestBed.createComponent(SpeakerComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
