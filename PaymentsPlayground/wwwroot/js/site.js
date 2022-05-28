// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


function autoSumbit(className)
{
    document.forms[className].submit();
}

$(function () {
    var placeholderElement = $('#modal-placeholder');

    $('button[data-toggle="ajax-modal"]').click(function (event) {

        event.preventDefault();

        var form = $('.paymentForm')
        var url = $(this).data('url');
        var dataToSend = form.serialize();

        $.post(url, dataToSend).done(function (data) {

            placeholderElement.html(data);
            placeholderElement.find('.modal').modal('show');
        });
    });
});


$(function () {
    var placeholderElement = $('#payments-placeholder');

    $('button[data-toggle="payments-modal"]').click(function (event) {

        event.preventDefault();

        var form = $('.getPayments')
        var url = $(this).data('url');
        var dataToSend = form.serialize();

        $.post(url, dataToSend).done(function (data) {
            placeholderElement.html(data);
            placeholderElement.find('.modal').modal('show');
        });
    });
});


