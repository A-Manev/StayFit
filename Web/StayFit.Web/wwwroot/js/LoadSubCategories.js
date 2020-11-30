function LoadSubCategories() {
    let categoryName = document.getElementById("CategoryId").value;
    let subCategory = document.getElementById("SubCategory");

    if (categoryName == "All") {
        subCategory.innerHTML = "<option value\"All\"></option>";
        return;
    }

    let xhr = new XMLHttpRequest();
    xhr.onreadystatechange = function () {
        subCategory.innerHTML = "<option value\"All\"></option>";
        if (this.readyState == 4 && this.status == 200) {

        let subCategories = JSON.parse(this.responseText);

            for (var i = 0; i < subCategories.length; i++) {
                const element = subCategories[i];
                subCategory.innerHTML += `<option value"${element.id}">${element.name}</option>`;
            }
        }
    };

    xhr.open("GET", "/Meals/CategorySubCategories?categoryId=" + categoryName);
    xhr.send();
}