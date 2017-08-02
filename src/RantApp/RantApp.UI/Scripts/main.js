$(document).ready(function () {

    function goToNextView(controllerPath)
    {
        $(".btn-submit").click(function () {
            alert("jquery works");

            if (controllerPath.includes("WizardTitleScreen")) {
                var selectedItemId = $("#selected-item-id").val();
                var title = $("#rant-title").val();
            }
            
             
            // Finish AJAX Call to update partial view on click
            $.ajax({
               
            });
        });
    }
});