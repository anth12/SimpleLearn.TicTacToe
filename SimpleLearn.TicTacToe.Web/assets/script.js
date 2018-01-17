// potential wins:
// [top, middle, bottom, left, center, right, topleft-bottomRight, topRight-bottomLeft]
var counters = [
    [0, 0, 0, 0, 0, 0, 0, 0],
    [0, 0, 0, 0, 0, 0, 0, 0]
]
var player = 0;
var moves = 0;
var end = false;
var machineMoves = [];
var gameResult = null;

var MachineWon = 0,
    Draw = 1,
    HumanWon = 2;

function updateCounters(row, column) {

    // increase the row/column counters for the players move
    counters[player][row]++;
    counters[player][column + 3]++;

    // check if move is on a diagonal
    if (row == column) {
        counters[player][6]++;
    }
    if (row + column == 2) {
        counters[player][7]++;
    }
}

function checkWinner() {

    // check if board full
    if (moves >= 9) {
        end = true;
        $("#result").html("Draw!");
        gameResult = Draw;
    }

    // check if player has won (line of 3)
    var win = counters[player].indexOf(3);
    if (win != -1) {
        end = true;
        $("#result").html(["Noughts", "Crosses"][player] + " Wins!");

        if (win < 3) {
            // row win
            _(3).times(function (n) {
                cellNo = (win * 3) + n;
                $("#" + cellNo).css("color", "#22ff00");
                $("#" + cellNo).css("background-color", "#0b4f00");
            });
        } else if (win < 6) {
            // column win
            _(3).times(function (n) {
                cellNo = (n * 3) + win - 3;
                $("#" + cellNo).css("color", "#22ff00");
                $("#" + cellNo).css("background-color", "#0b4f00");
            });
        } else if (win == 6) {
            // diagonal TL-BR win
            _(3).times(function (n) {
                cellNo = 4 * n;
                $("#" + cellNo).css("color", "#22ff00");
                $("#" + cellNo).css("background-color", "#0b4f00");
            });
        } else {
            // diagonal TR-BL win
            _(3).times(function (n) {
                cellNo = 2 * (n + 1);
                $("#" + cellNo).css("color", "#22ff00");
                $("#" + cellNo).css("background-color", "#0b4f00");
            });
        }

        gameResult = player == 1 ? MachineWon : HumanWon;
    }


    if (end) {
        retrainMachine();
    }
}

function computerMove() {

    var grid = [];
    for (var y = 0; y < 3; y++) {

        grid[y] = [' ', ' ', ' '];

        for (var x = 0; x < 3; x++) {

            var index = (y * 3) + x;

            var value = $('#' + index).html();
            if(value != '')
            grid[y][x] = value;
        }
    }
    console.log(grid);
    $.ajax({
        type: 'post',
        url: '/api/ml/next-move',
        data: {
            Value: grid
        }
    }).success(function(response) {

        machineMoves.push(response);

        // display
        $("#" + response.Position.Index).html("OX"[player]);
        
        // update the counters for potential wins
        updateCounters(response.Position.X, response.Position.Y);

        // increase moves taken
        moves++;

        // check for a winner/draw
        checkWinner();

        // next player
        player = (player + 1) % 2;

        // TODO store move for training

    }).fail(function(response) {
        alert(':( Oops, something went wrong.');
        reset();
    });


}

function reset() {

    // remove O/X from each cell, reset color
    $(".cell").each(function () {
        $(this).html("");
        $(this).css("color", "white");
        $(this).css("background-color", "black");
    });

    // reset result
    $("#result").html("");

    // reset variables
    counters = [
        [0, 0, 0, 0, 0, 0, 0, 0],
        [0, 0, 0, 0, 0, 0, 0, 0]
    ];
    player = 0;
    moves = 0;
    end = false;
    machineMoves = [];
}

function retrainMachine() {

    console.log(gameResult + ': Retraining');
    console.log(machineMoves);

    $.ajax({
        type: 'post',
        url: '/api/ml/train',
        data: {
            Game: {
                States: machineMoves
            },
            Result: gameResult
        }
    });
}

$(function () {

    // when a cell is clicked
    $(".cell").on(
        "click",
        function (eventObject) {

            // no winner/draw yet
            if (!end) {
                // get cell div
                var cell = eventObject.target;

                // row and column of cell
                var cellRow = Math.floor(cell.id / 3);
                var cellCol = cell.id % 3;

                // check position is empty
                if (cell.innerHTML == "") {

                    // display O/X
                    cell.innerHTML = "OX"[player];

                    // increase moves taken
                    moves++;

                    // update the counters for the potential wins
                    updateCounters(cellRow, cellCol);

                    // check for a winner/draw
                    checkWinner();


                    // next player
                    player = (player + 1) % 2;

                    if (!end) {
                        // computers turn
                        computerMove();

                    }
                }
            }
        });

    // reset button
    $("#reset").on(
        "click",
        reset);

    // change number of players
    $(".players").on(
        "change",
        reset);

    $(document).on('keypress', function(e) {
        
        switch (e.key) {
            case '7':
                $('#0').click();
                break;
            case '8':
                $('#1').click();
                break;
            case '9':
                $('#2').click();
                break;

            case '4':
                $('#3').click();
                break;
            case '5':
                $('#4').click();
                break;
            case '6':
                $('#5').click();
                break;

            case '1':
                $('#6').click();
                break;
            case '2':
                $('#7').click();
                break;
            case '3':
                $('#8').click();
                break;


            case 'Enter':
                $('#reset').click();
                break;
        }
    })
});
