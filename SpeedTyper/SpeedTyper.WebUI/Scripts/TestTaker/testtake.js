console.log("js loaded");


var testDataList;

function testInitialize() {
    testDataList = document.getElementById("test-data-text").innerHTML.split(/(\s+)/);
}

function processInput() {
    if (testDataList == null) {
        testInitialize();
    }
    var userInput = document.getElementById("txtTextEntryBox");
    var testData = document.getElementById("test-data-text");
    var firstWord = testDataList[0];
    if(userInput.value == firstWord){
        userInput.value = "";
        testDataList.shift();
        testData.innerHTML = testDataList.join("");
    }
    
}