
var directionInput = document.getElementById("direction");
var btnUD = document.getElementById("btn-UD");
btnUD.onclick = function () {
    directionInput.value = "UD";
    //draw(data);
};
var btnDU = document.getElementById("btn-DU");
btnDU.onclick = function () {
    directionInput.value = "DU";
    //draw(data);
};
var btnLR = document.getElementById("btn-LR");
btnLR.onclick = function () {
    directionInput.value = "LR";
    //draw(data);
};
var btnRL = document.getElementById("btn-RL");
btnRL.onclick = function () {
    directionInput.value = "RL";
    //draw(data);
};