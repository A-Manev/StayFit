$("body").on("click", "#btnAdd", function () {
    var txtexercise = $("#txtexercise");
    var exercise = document.getElementById('txtexercise');
    var txtreps = $("#txtreps");
    var txtsets = $("#txtsets");
    var txtweight = $("#txtweight");

    var tBody = $("#tblExercises > TBODY")[0];

    //Add Row
    var row = tBody.insertRow(-1);

    //Add cells
    var cell = $(row.insertCell(-1));
    //cell.html(txtexercise.val());
    cell.html(exercise.options[exercise.selectedIndex].textContent);

    cell = $(row.insertCell(-1));
    cell.html(txtreps.val());

    cell = $(row.insertCell(-1));
    cell.html(txtsets.val());

    cell = $(row.insertCell(-1));
    cell.html(txtweight.val());

    //add button cell
    cell = $(row.insertCell(-1));
    var btnRemove = $("<input/>");
    btnRemove.attr("type", "button");
    btnRemove.attr("onclick", "Remove(this);");
    btnRemove.attr("class", "btn btn-block btn-danger");
    btnRemove.val("Remove");
    cell.append(btnRemove);

    //clear the textboxes
    txtexercise.val("");
    txtreps.val("");
    txtsets.val("");
    txtweight.val("");
});

function Remove(button) {
    var row = $(button).closest("TR");
    var name = $("TD", row).eq(0).html();
    if (confirm("Do you want to delete: " + name)) {
        var table = $("#tblExercises")[0];

        table.deleteRow(row[0].rowIndex);
    }
}

$("body").on("click", "#btnSave", function () {
    var exercises = new Array();
    $("#tblExercises TBODY TR").each(function () {
        var row = $(this);
        var exercise = {};
        exercise.name = row.find("TD").eq(0).html();
        exercise.reps = row.find("TD").eq(1).html();
        exercise.sets = row.find("TD").eq(2).html();
        exercise.weight = row.find("TD").eq(3).html();
        exercises.push(exercise);
    });

    console.log(exercises);
    var antiForgeryToken = $('#antiForgeryForm input[name=__RequestVerificationToken]').val();

    $.ajax({
        url: "/Exercises/Workout",
        type: "POST",
        data: JSON.stringify(exercises),
        headers: {
            'X-CSRF-TOKEN': antiForgeryToken
        },
        success: function (r) {
            //alert(r + " record(s) inserted.");
            //ocation.reload();
            window.location = "https://localhost:44319/Diaries/WorkoutDiary";
        },
        contentType: 'application/json',
    });
});