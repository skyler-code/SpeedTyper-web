console.log("js loaded");


var testDataList;
var dataSource;
var currentWord;
var correctWords = [];
var _testID;
var startTimerCountdown = 5;
var endTimerCountdown;
var testInProgress = false;
var countdownInProgress = false;
var startTime;
var stH;

$(function () {
    console.log("jquery loaded");
    var connection = $.hubConnection();
    connection.logging = true;

    var proxy = connection.createHubProxy('testHub');

    proxy.on('whisper', function (msg) {
        console.log(msg);
    });

    proxy.on('beginTest', function (testDataText, _dataSource, newtestID, _endTimerCountdown) {
        console.log("begin test");
        testInProgress = false;
        dataSource = _dataSource;
        _testID = newtestID;
        endTimerCountdown = _endTimerCountdown;
        testDataList = testDataText.split(/\b(?![\s.])/);
        currentWord = testDataList.shift();
        startTestCountdown();
    });

    proxy.on('testSubmitSuccess', function (submissionString, rewardString) {
        $('#dialog-submission-message').html(submissionString.replace(/\n/g, "<br />"));
        $('#dialog-submission').dialog({
            modal: true,
            closeOnEscape: false,
            open: function (event, ui) {
                $(".ui-dialog-titlebar-close", ui.dialog | ui).hide();
            },
            buttons: {
                Ok: function () {
                    $(this).dialog("close");
                    if (rewardString != "") {
                        RewardDialog(rewardString.replace(/\n/g, "<br />"));
                    }
                }
            }
        });
    });

    proxy.on('testSubmitFailure', function (msg) {
        alert(msg);
    });

    proxy.on('updatePage', function (currentXP, xpToLevel, xpString, widthPercentString, greeting) {
        $('#progress-bar').attr({
            "aria-valuenow": currentXP,
            "aria-valuemax": xpToLevel,
            style: "width: " + widthPercentString
        });
        $('#progress-bar-span').html(xpString);
        $('#greeting').html(greeting);
    });

    function endTest() {
        $('#user-speed').html(CalculateWPM());
        testInProgress = false;
        $('#txtTextEntryBox').val('Test over.').prop('readonly', true).blur();
        var _guid = $('#guid').html();
        _timeElapsed = (Date.now() - startTime) * (1 / 1000);

        var _wpm = CalculateWPM();
        var words = correctWords.join("");
        var stStr = startTime.toString();
        var timeElapsedInt = parseInt(_timeElapsed);
        proxy.invoke('submitTest', _testID, _wpm, timeElapsedInt, words, stStr, stH);
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
                startTime = Date.now();
                _st = startTime.toString() + _testID.toString();
                stH = Sha1.hash(_st);
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
                $('#user-speed').html(CalculateWPM());
                $('#time-left').html(output);
                counter--;
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
        var _tmptimeElapsed = Date.now() - startTime;
        var minutesElapsed = _tmptimeElapsed * (1 / 60000);
        if (minutesElapsed > 0) {
            var tmp = (typedEntries.length / 5) / minutesElapsed;
            var wpm = round(tmp, 2);
        }
        return wpm;
    }

    function RewardDialog(rewardString) {
        $('#dialog-reward-message').html(rewardString);
        $('#dialog-reward').dialog({
            closeOnEscape: false,
            open: function (event, ui) {
                $(".ui-dialog-titlebar-close", ui.dialog | ui).hide();
            },
            modal: true,
            buttons: {
                Ok: function () {
                    $(this).dialog("close");
                }
            }
        });
    }

    function round(value, decimals) { // http://www.jacklmoore.com/notes/rounding-in-javascript/
        return Number(Math.round(value + 'e' + decimals) + 'e-' + decimals);
    }
});
