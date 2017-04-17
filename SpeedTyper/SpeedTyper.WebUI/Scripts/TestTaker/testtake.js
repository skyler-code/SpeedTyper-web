console.log("js loaded");


var testDataList;
var dataSource;
var currentWord;
var correctWords = [];
var testID;
var startTimerCountdown = 5;
var endTimerCountdown = 120;
var testInProgress = false;
var timeElapsed = 0;


$(function () {
    console.log("jquery loaded");
    var connection = $.hubConnection();

    var proxy = connection.createHubProxy('testHub');

    proxy.on('whisper', function (msg) {
        console.log(msg);
    });

    proxy.on('beginTest', function (testDataText, _dataSource, _testID) {
        console.log("begin test");
        testInProgress = true;
        dataSource = _dataSource;
        testID = _testID;
        testDataList = testDataText.split(/\b(?![\s.])/);
        currentWord = testDataList.shift();
        timeElapsed = 0;
        startTestCountdown();
    });

    connection.start().done(function (e) {
        console.log("success")
        $('#start-test').click(function () {
            proxy.invoke('startTest');
        });
    }).fail(function (error) {
        console.log(error);
    });
});

function startTestCountdown() {
    $('#txtTextEntryBox').prop('readonly', true);
    var counter = startTimerCountdown;
    var timer = setInterval(function () {
        var output = 'Test starting in ' + counter + ' seconds...';
        $('#txtTextEntryBox').val(output);
        counter--;
        if (counter < 1) {
            clearInterval(timer);
            startTest();
        }
    }, 1000);
}

function startTest() {
    var counter = endTimerCountdown;
    var timer = setInterval(function () {
        if (!testInProgress) {
            clearInterval(timer);
        }
        if (counter == endTimerCountdown) {
            timeElapsed = 0;
            $("#untyped-words").html(testDataList.join(""));
            $('#current-word').html(currentWord);
            $('#correct-words').html("");
            $('#data-source').html(dataSource);
            correctWords = [];
            $('#txtTextEntryBox').prop('readonly', false);
            $('#txtTextEntryBox').val('').focus();
        }
        var minutes = Math.floor(counter / 60);
        var seconds = counter - minutes * 60;
        if (seconds < 10) {
            seconds = "0" + seconds;
        }
        var output = minutes + ":" + seconds;
        $('#time-left').html(output);
        counter--;
        timeElapsed++;
        if (counter < 0) {
            clearInterval(timer);
            endTest();
        }
    }, 1000);
}

function endTest() {
    $('#user-speed').html(CalculateWPM());
    testInProgress = false;
    $('#txtTextEntryBox').val('Test over.').prop('readonly', true).blur();
}


function processInput() {
    if (testInProgress) {
        var userInput = $('#txtTextEntryBox');
        if (userInput.val() == currentWord) {
            correctWords.push(currentWord);
            if (testDataList[0] != null) {
                currentWord = testDataList.shift();
            } else {
                endTest();
                currentWord = "";
            }
            userInput.val("");
            $("#untyped-words").html(testDataList.join(""));
            $("#correct-words").html(correctWords.join(""));
            $("#current-word").html(currentWord);
        }
        $('#user-speed').html(CalculateWPM());
    }
}

function CalculateWPM() {
    var typedEntries = correctWords.join("");
    var minutesElapsed = timeElapsed * (1 / 60);
    if (minutesElapsed > 0) {
        var tmp = (typedEntries.length / 5) / minutesElapsed;
        var wpm = round(tmp, 2);
    }
    return wpm;
}

function round(value, decimals) { // http://www.jacklmoore.com/notes/rounding-in-javascript/
    return Number(Math.round(value+'e'+decimals)+'e-'+decimals);
}