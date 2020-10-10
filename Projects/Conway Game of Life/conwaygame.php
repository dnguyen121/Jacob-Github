<?php

include("auth.php"); //include auth.php file on all secure pages
?>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css" integrity="sha384-Vkoo8x4CGsO3+Hhxv8T/Q5PaXtkKtu6ug5TOeNV6gBiFeWPGFN9MuhOf23Q9Ifjh" crossorigin="anonymous">    
    <link rel="stylesheet" href="conwaygame.css">

    <title>Conway's Game of Life</title>
</head>
<style>
    body {
        background-image: linear-gradient(to right, #fa709a 0%, #fee140 100%);    }
</style>
<body>

<h3 class="text-center">Enjoy the game, <?php echo $_SESSION['username']; ?>!</h3>
    <div class="buttons">
        <!-- <label for="numrow">Number of Rows:</label>
        <input type="text" id="numrow" name="numrow">
        <label for="numcol">Number of Columns:</label>
        <input type="text" id="numcol" name="numcol"> -->

        <label for="partern">Choose a pattern:</label>
        <select id="pattern">
            <option value="none">None</option>
            <option value="boat">Boat</option>
            <option value="beacon">Beacon</option>
            <option value="toad">Toad</option>
            <option value="glider">Glider</option>
        </select>
        <button type="button" class="start btn btn-primary">Start</button>
        <button type="button" class="stop btn btn-primary">Stop</button>
        <button type="button" class="increment1 btn btn-primary">Increment 1 generation</button>
        <button type="button" class="increment23 btn btn-primary">Increment 23 generations</button>
        <button type="button" class="reset btn btn-primary">Reset</button>
        <a role="button" class="btn btn-danger" href="logout.php">Logout</a></p>

    </div>

    <div class="display">
        <span class="generation">Number of generations: </span>
    </div>

    <div class="display">
        <div class="box">
        </div>
    </div>

    <div class="table">
        <div class="container-fluid">
        </div>
    </div>
        <!-- <div class="container">
        </div> -->

    <script src="conwaygame.js"></script>
</body>
</html>


