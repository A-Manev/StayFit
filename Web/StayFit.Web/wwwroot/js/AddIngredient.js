var ingredientIndex = 0;

function AddIngredient() {
    $("#IngredientsContainer")
        .append("<div class='form-group'>Quantity and name: <input type='text' name='Ingredients[" + ingredientIndex + "].NameAndQuantity' /></div>");
    ingredientIndex++;
}