console.log("js loaded");


var testDataList;
var correctWords = [];

function testInitialize() {
    testDataList = document.getElementById("untyped-words").innerHTML.split(/\b(?![\s.])/);
    console.log(testDataList);
}

function processInput() {
    if (testDataList == null) {
        testInitialize();
    }
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