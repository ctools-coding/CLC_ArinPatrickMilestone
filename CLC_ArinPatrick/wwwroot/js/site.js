﻿$(function ()
{
    console.log("Page is loaded.");
    $(document).on("click", ".game-button", function (event)
    {
        event.preventDefault();

        var location = $(this).val();
        console.log("Cell: " + location + " was clicked.");


    });
});

function doBoardUpdate(location)
{
    $.ajax(
        {
            datatype: 'json',
            method: 'POST',
            url: '/Minesweeper/OneCell',
            data:
            {
                "location": location
            },
            success: function (data)
            {
                console.log(data)
                $("#" + location).html(data);
            }
        });
};

function gameOver()
{
    $.ajax(
        {
            datatype: 'json',
            method: 'GET',
            url: 'Minesweeper/CheckGameOver',
            data: {},
            success: function (data)
            {
                console.log(data);
                $('#gameOver').html(data);
            }
        });
}