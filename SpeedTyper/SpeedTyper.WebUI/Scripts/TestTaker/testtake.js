console.log("js loaded");

$(function () {
    console.log("jquery loaded");


    
    testDataList = document.getElementById("untyped-words").innerHTML.split(/\b(?![\s.])/);
});

var testDataList;
var correctWords = [];
var testID;


$(function () {
    var hubConnection = $.connection.testHub;

    hubConnection.client.beginTest = function (testDataText, dataSource, _testID) {
        console.log("begin test");
        var untypedWords = document.getElementById("untyped-words");
        $("untyped-words").innerHTML = testDataText;
        testID = _testID;
        testDataList = testDataText.split(/\b(?![\s.])/);
    };

    hubConnection.client.whisper = function (msg) {
        console.log(msg);
    };

    $.connection.hub.start().done(function (e) {
        console.log("success")
        $('#start-test').click(function () {
            // Call the Send method on the hub.
            hubConnection.server.notify("hi");
            hubConnection.server.startTest();
        });
    }).fail(function (error) {
        console.log(error);
    });
});
function processInput() {
    var userInput = document.getElementById("txtTextEntryBox");
    var untypedWords = document.getElementById("untyped-words");
    var correctsWordsDisplay = document.getElementById("correct-words");
    var firstWord = testDataList[0];
    if (userInput.value == firstWord) {
        correctWords.push(testDataList.shift());
        userInput.value = "";
        untypedWords.innerHTML = testDataList.join("");
        correctsWordsDisplay.innerHTML = correctWords.join("");
    }
}





