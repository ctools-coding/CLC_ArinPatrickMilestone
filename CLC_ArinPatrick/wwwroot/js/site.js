//function that logs that the page has been loaded,
//is responsable for each button click to reveal cell
//logs which button was clicked
//updates board based on button location 
//checks to see if the game is over
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


//block the context menu on right click
$(document).bind("contextmenu", function (e) {
    e.preventDefault();
    console.log("Context menu blocked");
});

//function that calls the function PartialBoard inside of the Minesweeper controller
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


//js function that uses ajax
//this function is called in the function below
function placeFlag(location)
{
$.ajax(
    {
    //json data being sent
        datatype: 'json',
        method: 'POST',
        //using the method on right button click inside the minesweeper controller "On right button click"
        url: '/Minesweeper/OnRightButtonClick',
        data:
        {
            //sending the location of the button
            "location": location
        },
        //if successful data will be logged
        success: function (data) {
            console.log(data)
            $('#board').html(data)
        }
    });
};

$(function () {
    //when user presses down "Mouse down" on a gamecell this function is used
    $(document).on("mousedown", ".game-cell", function (e) {
        console.log("We are inside mouse down");
        //my right click is three(knock off japanese mouse)
        //when right click is pressed
        //1.the button number value is received(essentially the id)
        //2. the location of the button will be logged
        //3. the place flag function is called
        if (e.button == 2) {
            var buttonNumber = $(this).val();
            console.log(location);
          
            placeFlag(buttonNumber);
        }
    });
});

//function checks if game is over, we call the check game over method inside of the minesweeper controller
    function gameOver() {
        $.ajax(
            {
                datatype: 'json',
                method: 'GET',
                url: '/Minesweeper/CheckGameOver',
                data: {},
                success: function (data) {
                    console.log(data);
                    $('#gameOver').html(data);
                }
            });
    };
