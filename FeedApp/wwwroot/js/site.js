//$(document).ready(function () {
//    $("#sidebar").mCustomScrollbar({
//        theme: "minimal"
//    });
//    $('#sidebarCollapse').on('click', function () {
//        $('#sidebar, #content').toggleClass('active');
//        $('.collapse.in').toggleClass('in');
//        $('a[aria-expanded=true]').attr('aria-expanded', 'false');
//    });
//});
//<script>
//        window.addEventListener("popstate", function () {
//        window.location.href = "/Signin/Signin"; 
//    });
//</script>
$(document).ready(function () {
    $("#sidebar").mCustomScrollbar({
        theme: "minimal"
    });
    $('#sidebarCollapse').on('click', function () {
    $('#sidebar, #content').toggleClass('active');
    $('.collapse.in').toggleClass('in');
    $('a[aria-expanded=true]').attr('aria-expanded', 'false');
    });

// Detect if the user tries to navigate back

});
window.addEventListener("popstate", function () {
    alert("Back")
    window.location.href = "/Signin/Signin";
});

