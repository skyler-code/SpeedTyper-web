console.log("js loaded");


var testDataList;
var currentWord;
var correctWords = [];
var testID;


$(function () {
    console.log("jquery loaded");
    var connection = $.hubConnection();

    var proxy = connection.createHubProxy('testHub');

    proxy.on('whisper', function (msg) {
        console.log(msg);
    });

    proxy.on('beginTest', function (testDataText, dataSource, _testID) {
        console.log("begin test");
        $("#data-source").html(dataSource);
        testID = _testID;
        testDataList = testDataText.split(/\b(?![\s.])/);
        currentWord = testDataList.shift();
        $("#untyped-words").html(testDataList.join(""));
        $('#current-word').html(currentWord);
        $('#correct-words').html("");
        correctWords = [];
        $('#txtTextEntryBox').val('').focus();
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


function processInput() {
    var userInput = $('#txtTextEntryBox');
    if (userInput.val() == currentWord) {
        correctWords.push(currentWord);
        if (testDataList[0] != null) {
            currentWord = testDataList.shift();
        } else {
            currentWord = "";
        }
        userInput.val("");
        $("#untyped-words").html(testDataList.join(""));
        $("#correct-words").html(correctWords.join(""));
        $("#current-word").html(currentWord);
    }
}





