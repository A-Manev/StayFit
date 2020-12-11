function like(commentId) {
    var likes = document.querySelectorAll(".fa-thumbs-up");
    let arr = Array.from(likes);
    var i = arr.filter(x => x.attributes[1].nodeValue == commentId);
    var json = { commentId: commentId };
    console.log(json);
    $.ajax({
        url: "/api/Likes",
        type: "POST",
        data: JSON.stringify(json),
        success: function (data) {
            $(i[0]).text(data.likesCount);

            if (data.isLiked) {

                i[0].className = "fas fa-thumbs-up text-primary";
            }
            else {
                i[0].className = "far fa-thumbs-up";
            }
        },
        contentType: 'application/json',
    });
}