var y = document.getElementById("caloriesRemaining").textContent;
var x = document.getElementById("goalCalories").textContent;

var caloriesEaten = x - y;
var progressNumber = Math.round(caloriesEaten / x * 100);

var progressBar = $('.progress-bar')
setInterval(function () {

    progressBar.css('width', progressNumber + '%');
    progressBar.attr('aria-valuenow', progressNumber);
    progressBar.text(progressNumber + '%');
}, 50);
