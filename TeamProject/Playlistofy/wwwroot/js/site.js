// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function () {
    $("#searchQuery").autocomplete({
        source: function (request, response) {
            var srchType = document.getElementById("searchType").value;
            $.ajax({
                url: '/Home/AutoComplete/',
                data: { prefix: request.term, searchType: srchType },
                type: "POST",
                success: function (data) {
                    response($.map(data, function (item) {
                        return item;
                    }))
                },
                error: function (response) {
                    alert(response.responseText);
                },
                failure: function (response) {
                    alert(response.responseText);
                }
            });
        },
        select: function (e, i) {
            $("#itemId").val(i.item.Id);
        },
        minLength: 1
    });
});