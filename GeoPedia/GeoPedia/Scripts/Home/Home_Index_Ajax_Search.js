(function () {

    //function executes when a key is lifted while the cursor is inside id="search-term"
    $("#search-term").keyup(function () {

        console.log($("#search-term").val())
        //If search-term is not an empty string, submit the form
        if ($("#search-term").val() !== "") {
            $("#search-form").submit();
        }
        else {
            //If search-term is an empty string, clear the results div
            $("#search-results").html = "";
        }
    });

})();