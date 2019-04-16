
var directionInput = document.getElementById("direction");
var btnUD = document.getElementById("btn-UD");
btnUD.onclick = function () {
    directionInput.value = "UD";
    init();
};
var btnDU = document.getElementById("btn-DU");
btnDU.onclick = function () {
    directionInput.value = "DU";
    init();
};
var btnLR = document.getElementById("btn-LR");
btnLR.onclick = function () {
    directionInput.value = "LR";
    init();
};
var btnRL = document.getElementById("btn-RL");
btnRL.onclick = function () {
    directionInput.value = "RL";
    init();
};

var btnOpenInAM = document.getElementById("btn-openInAM");
btnOpenInAM.onclick = function (params) {
    ajaxOpenNodeInAM(35052293);
};