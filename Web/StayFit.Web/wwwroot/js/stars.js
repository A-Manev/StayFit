const container = document.querySelector('.item-rating');
const items = document.querySelectorAll('.star');
//const items = container.querySelectorAll('star')

container.onclick = e => {
    //const elClass = e.target.classList;
    //let a = this.responseText;

    let averageVote = container.innerText.split('/');
    let count = parseInt(Math.round(averageVote[0].trim()));

    for (var i = 0; i < count; i++) {

        if (items[i].className == "star star - fill") {

            items[i].className = "star star-empty";
        }
        else {
            items[i].className = "star star-fill";
        }
    }

    //if (elClass.contains('fas')) {
    //    items.forEach(
    //        item => item.classList.replace('fill', 'empty'));
    //}

    //console.log(e.target.getAttribute("data-fill"));
    //elClass.remore('empty');
    //elClass.add('fill');
}