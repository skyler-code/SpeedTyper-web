$(function () {
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

    console.log("jquery loaded");
    var connection = $.hubConnection();
    connection.logging = true;
    var proxy = connection.createHubProxy('testHub');

    proxy.on('whisper', function (msg) {
        console.log(msg);
    });

    proxy.on('beginTest', function (testDataText, _dataSource, newtestID, _endTimerCountdown) {
        testInProgress = false;
        dataSource = _dataSource;
        _testID = newtestID;
        endTimerCountdown = _endTimerCountdown;
        testDataList = testDataText.split(/\b(?![\s.])/);
        currentWord = testDataList.shift();
        startTestCountdown();
    });

    proxy.on('updateWPM', function (wpm) {
        if (testInProgress) {
            $('#user-speed').html(wpm);
        }
    });

    proxy.on('testSubmitSuccess', function (submissionString, rewardString, finalWPM) {
        $('#user-speed').html(finalWPM);
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
        testInProgress = false;
        $('#txtTextEntryBox').val('Test over.').prop('readonly', true).blur();
        var _guid = $('#guid').html();
        var elapsed = parseInt((Date.now() - startTime) * (1 / 1000));
        proxy.invoke('stsub', _testID, elapsed, endTimerCountdown, correctWords.join(""), dataSource, startTime.toString(), stH);
    }

    connection.start().done(function (e) {
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
                var _st = startTime.toString() + _testID.toString();
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
                CalculateWPM();
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
            CalculateWPM();
        }
    });

    function CalculateWPM() {
        var typedEntries = correctWords.join("");
        var secondsElapsed = (Date.now() - startTime) / 1000;
        proxy.invoke('calculateWPM', typedEntries, secondsElapsed);
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
