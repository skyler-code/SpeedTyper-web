console.log("js loaded");


var testDataList;
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
        $("#untyped-words").html(testDataText);
        $("#data-source").html(dataSource);
        testID = _testID;
        testDataList = testDataText.split(/\b(?![\s.])/);
        $('#txtTextEntryBox').focus();
    });

    connection.start().done(function (e) {
        console.log("success")
        $('#start-test').click(function () {
            // Call the Send method on the hub.
            proxy.invoke('notify', 'hi');
            proxy.invoke('startTest');
            console.log('invoked');
        });
    }).fail(function (error) {
        console.log(error);
    });
});
function processInput() {
    var userInput = $('#txtTextEntryBox');
    var firstWord = testDataList[0];
    if (userInput.val() == firstWord) {
        correctWords.push(testDataList.shift());
        userInput.val("");
        $("#untyped-words").html(testDataList.join(""));
        $("#correct-words").html(correctWords.join(""));
    }
}





