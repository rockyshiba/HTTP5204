(function () {
    //AJAX search bar
    $(document).ready(function () {
        //Adding margin to the bottom of the search_results div so the search suggestions don't overlap the search results
        $("#search_results").animate({ marginBottom: '250px' }, 1000);
    });

    $("#country_ddl").change(function () {
        $("#country_form").submit();
    });

    $("#city_ddl").change(function () {
        console.log("submit");
        $("#city_form").submit();
    });

    //The javascript responsible for dynamic search while the user types in the search box
    $("#search_form").keyup(function () {
        //When the user keys up from the form, the form will submit
        $("#search_form").submit();
    });
})()