$(document).on("click", ".select-Quantity", function (e) {

    e.preventDefault();

    var _self = $(this);

    var myBookId = _self.data('id');
    $("#mealId").val(myBookId);
});