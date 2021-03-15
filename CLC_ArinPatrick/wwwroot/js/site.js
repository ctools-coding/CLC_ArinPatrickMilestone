$(function ()
{
    console.log("Page is loaded.");
    $(document).on("click", ".game-cell", function (event)
    {
        event.preventDefault();

        var location = $(this).val();
        console.log("Cell: " + location + " was clicked.");

        doBoardUpdate(location);
        gameOver();

    });
});


//block the context menu
$(document).bind("contextmenu", function (e) {
    e.preventDefault();
    console.log("Context menu blocked");
});
function doBoardUpdate(location)
{
    $.ajax(
        {
            datatype: 'json',
            method: 'POST',
            url: '/Minesweeper/PartialBoard',
            data:
            {
                "location": location
            },
            success: function (data)
            {
                console.log(data)
                $('#board').html(data)
            }
        });
};


function placeFlag(location)
{
$.ajax(
{
        datatype: 'json',
        method: 'POST',
        url: '/Minesweeper/OnRightButtonClick',
        data:
        {
            "location": location
        },
        success: function (data) {
            console.log(data)
            $('#board').html(data)
        }
    });
};

$(function  () {
    $(document).on("mousedown", ".game-cell", function (e) {
        console.log("We are inside mouse down");
        //my right click is three
        if (e.button == 2) {
            var buttonNumber = $(this).val();
            console.log(location);
          
            placeFlag(buttonNumber);

        }
    });
});

    function gameOver() {
        $.ajax(
            {
                datatype: 'json',
                method: 'GET',
                url: 'Minesweeper/CheckGameOver',
                data: {},
                success: function (data) {
                    console.log(data);
                    $('#gameOver').html(data);
                }
            });
    };
