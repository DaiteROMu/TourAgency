$(document).ready(function () {
    $(function () {
        $('#hotelcategory-select').multiselect({
            includeSelectAllOption: true,
            buttonWidth: '200px',
            allSelectedText: 'All hotels',
            nonSelectedText: 'Choose category'
        });
    });
});

function GetMultipleSelects() {
    $('#hotelroom-name-select').multiselect({
        includeSelectAllOption: true,
        disableIfEmpty: true,
        buttonWidth: '200px',
        allSelectedText: 'All rooms',
        nonSelectedText: 'Room'
    });

    $('#foodcategory-select').multiselect({
        includeSelectAllOption: true,
        disableIfEmpty: true,
        buttonWidth: '200px',
        allSelectedText: 'All food',
        nonSelectedText: 'Food'
    });
}

function GetCitiesBySelectedCountryId() {
    var countryId = Number(document.getElementById('country-select').value);
    var url = "/Home/GetCitiesByCountryId?countryId=" + encodeURIComponent(countryId);
    $.ajax(
        {
            url: url,
            dataType: 'html',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                $('#city-selection-block').html(data);
            }
        });
}

function GetHotelsFromSelection(page) {
    var countryId = Number(document.getElementById('country-select').value);
    var cityId = Number(document.getElementById('sity-select').value);
    var selected_hotelcategories = $('#hotelcategory-select').val();
    var url = "/Home/GetHotelsFromSelection?countryId=" + encodeURIComponent(countryId) + "&cityId=" + encodeURIComponent(cityId)
        + "&selectedHotelCategories=" + encodeURIComponent(selected_hotelcategories.join(';')) + "&page=" + encodeURIComponent(page);
    $.ajax(
        {
            url: url,
            dataType: 'html',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                $('#hotels-offers-container').html(data);
            }
        });
}

function GetHotelById(hotelId) {
    var url = "/Home/GetHotelById?hotelId=" + encodeURIComponent(hotelId);
    $.ajax(
        {
            url: url,
            dataType: 'html',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                $('#hotels-offers-container').html(data);
            }
        });
}

function GetToursFromSelection(hotelId) {
    var selected_hotelroomnames = $('#hotelroom-name-select').val();
    var selected_foodcategories = $('#foodcategory-select').val();
    var url = "/Home/GetToursFromSelection?hotelId=" + encodeURIComponent(hotelId) + "&selectedHotelRoomNames=" + encodeURIComponent(selected_hotelroomnames.join(';')) + "&selectedFoodCategories=" + encodeURIComponent(selected_foodcategories.join(';'));
    $.ajax(
        {
            url: url,
            dataType: 'html',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                $('#hotelroom-tours-container').html(data);
            }
        });
}

function GetHotelRoomById(hotelRoomId) {
    var url = "/Home/GetHotelRoomById?hotelRoomId=" + encodeURIComponent(hotelRoomId);
    $.ajax(
        {
            url: url,
            dataType: 'html',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                $('#hotelroom-tours-container').html(data);
            }
        });
}