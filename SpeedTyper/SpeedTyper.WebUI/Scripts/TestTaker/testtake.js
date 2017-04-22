console.log("js loaded");


var testDataList;
var dataSource;
var currentWord;
var correctWords = [];
var _testID;
var startTimerCountdown = 5;
var endTimerCountdown = 120;
var testInProgress = false;
var countdownInProgress = false;
var _timeElapsed = -1;


$(function () {
    console.log("jquery loaded");
    var connection = $.hubConnection();

    var proxy = connection.createHubProxy('testHub');

    proxy.on('whisper', function (msg) {
        console.log(msg);
    });

    proxy.on('beginTest', function (testDataText, _dataSource, newtestID) {
        console.log("begin test");
        testInProgress = false;
        dataSource = _dataSource;
        _testID = newtestID;
        testDataList = testDataText.split(/\b(?![\s.])/);
        currentWord = testDataList.shift();
        _timeElapsed = -1;
        startTestCountdown();
    });

    proxy.on('testSubmitSuccess', function (submissionString, rewardString) {
        alert(submissionString);
        if (rewardString != "") {
            alert(rewardString);
        }
    });

    proxy.on('testSubmitFailure', function (msg) {
        alert(msg);
    });

    proxy.on('updatePage', function (currentXP, xpToLevel, xpString, widthPercentString, greeting) {
        $('#progress-bar').attr({
            "aria-valuenow" : currentXP,
            "aria-valuemax": xpToLevel,
            style : "width: " + widthPercentString
        });
        $('#progress-bar-span').html(xpString);
        $('#greeting').html(greeting);
    });

    function endTest() {
        $('#user-speed').html(CalculateWPM());
        testInProgress = false;
        $('#txtTextEntryBox').val('Test over.').prop('readonly', true).blur();
        var _guid = $('#guid').html();
        proxy.invoke('submitTest', _testID, CalculateWPM(), _timeElapsed);
    }

    connection.start().done(function (e) {
        console.log("success");
        $('#start-test').click(function () {
            if (!countdownInProgress) {
                proxy.invoke('startTest');
                countdownInProgress = true;
            }
        });
    }).fail(function (error) {
        console.log(error);
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
            if (counter == endTimerCountdown) {
                testInProgress = true;
                countdownInProgress = false;
                _timeElapsed = -1;
                $("#untyped-words").html(testDataList.join(""));
                $('#current-word').html(currentWord);
                $('#correct-words').html("");
                $('#data-source').html(dataSource);
                correctWords = [];
                $('#txtTextEntryBox').prop('readonly', false);
                $('#txtTextEntryBox').val('').focus();
            }
            if (!testInProgress) {
                clearInterval(timer);
            } else {
                var minutes = Math.floor(counter / 60);
                var seconds = counter - minutes * 60;
                if (seconds < 10) {
                    seconds = "0" + seconds;
                }
                var output = minutes + ":" + seconds;
                $('#time-left').html(output);
                counter--;
                _timeElapsed++;
            }
            if (counter < 0) {
                clearInterval(timer);
                endTest();
            }
        }, 1000);
    }


    var userInput = $('#txtTextEntryBox');

    userInput.on('input', function (e) {
        if (testInProgress) {
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
    });

    function CalculateWPM() {
        var typedEntries = correctWords.join("");
        var minutesElapsed = _timeElapsed * (1 / 60);
        if (minutesElapsed > 0) {
            var tmp = (typedEntries.length / 5) / minutesElapsed;
            var wpm = round(tmp, 2);
        }
        return wpm;
    }

    function round(value, decimals) { // http://www.jacklmoore.com/notes/rounding-in-javascript/
        return Number(Math.round(value + 'e' + decimals) + 'e-' + decimals);
    }
});
