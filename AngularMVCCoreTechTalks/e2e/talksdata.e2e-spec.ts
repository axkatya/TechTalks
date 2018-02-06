import { browser, by, element } from 'protractor';

describe('Tech Talks App', () => {

    var addNewTalkButton = element(by.id('btnAddNewTalk'));
    var searchButton = element(by.id('btnSearch'));
    var topicFilterInput = element(by.id('inputTopicFilter'));
    var speakerNameFilterInput = element(by.id('inputSpeakerNameFilter'));
    var disciplineFilterInput = element(by.id('selectDisciplineFilter'));
    var locationFilterInput = element(by.id('selectLocationFilter'));
    var dateToFilterInput = element(by.id('dtPickerDateToFilter'));
    var dateFromFilterInput = element(by.id('dtPickerDateFromFilter'));

    beforeEach(() => {
        browser.get("/talks-data");
    });

    it('should display Tech Talks header', () => {
        expect<any>(element(by.css("h2")).getText()).toEqual('Tech Talks');
    });

    it('should add talk', function () {
        addNewTalkButton.click();
        expect(browser.getCurrentUrl()).toContain("/upsert-talk");
    });

});
