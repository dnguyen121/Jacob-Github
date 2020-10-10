<?php

include("auth.php"); //include auth.php file on all secure pages ?>
<!DOCTYPE html>
<html>
<head>
<meta charset="utf-8">
<title>Welcome Home</title>
<link rel="stylesheet" href="css/style.css" />
<link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css" integrity="sha384-Vkoo8x4CGsO3+Hhxv8T/Q5PaXtkKtu6ug5TOeNV6gBiFeWPGFN9MuhOf23Q9Ifjh" crossorigin="anonymous">    
</head>
<style>
    body {
        background-image: linear-gradient(to right, #fa709a 0%, #fee140 100%);    }
</style>
<body>

<div class="container">
<h3 class="text-center">Welcome <?php echo $_SESSION['username']; ?>!</h3>
<img class="rounded mx-auto d-block" alt="Responsive image" src="logo.png">
    <p class="font-weight-bold">Rules of the game:</p>
    <p>The game consists of a grid of cells, each of which can be alive or dead. For every cycle of the game,
    the cells can be turned on or off based on the following rules:
    <br>
1.  Any live cell with fewer than two live neighbors dies, as if caused by under population. <br>
2.  Any live cell with more than three live neighbors dies, as if by overcrowding. <br>
3.  Any live cell with two or three live neighbors’ lives on to the next generation. <br>
4.  Any dead cell with exactly three live neighbors becomes a live cell.<br>
5.  If a dead cell has exactly three live neighbors, it comes to <br>
6. If a live cell has less than two live neighbors, it dies<br>
7. If a live cell has more than three live neighbors, it dies<br>
8. If a live cell has two or three live neighbors, 
it continues living. life - Therefore by repeating the cycle over and over, 
these simple rules create interesting, often unpredictable patterns.<br>
</p>
<p class="text-center"><a role="button" class="btn btn-primary" href="conwaygame.php">Play Now!</a>
<a role="button" class="btn btn-danger" href="logout.php">Logout</a></p>

<div class="card rounded mx-auto d-block" style="width: 18rem;">
  <div class="card-header text-center">
    Game Made By:
  </div>
  <ul class="text-center list-group list-group-flush">
    <li class="text-center list-group-item">Jacob Nguyen</li>
    <li class="text-center list-group-item">Sunny Patel</li>
    <li class="text-center list-group-item">Harsh Jivani</li>
  </ul>
</div>


<br /><br /><br /><br />

</div>
</body>
</html>
