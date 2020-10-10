<?php
    session_start();
    $_SESSION['ranking'][$_SESSION['Username']] = $_SESSION['numbWin'];
    $strTemp3 = '';
    foreach ($_SESSION['ranking'] as $user=>$score){
        $strTemp3 = $strTemp3." ".$user." ".$score;
    }
    file_put_contents('rank.txt',$strTemp3);
?>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Ranking Page</title>
    <link rel="stylesheet" href="styles.css">
</head>

<body>

<form class="rank-form" action="login.php" method="post">
    <button class="rank-button-logout button" name="Submit" value="Logout" type="submit">Log out</button>
</form>
    <br>    
<form class="rank-form" action="index.php" method="post">
    <button class="rank-button-back button" name="Submit" value="back" type="submit">Back to the game</button>
</form>

<?php
    print '<div class="rank-leadingboard">';
    arsort($_SESSION['ranking']);
    $i = 1;

    foreach($_SESSION['ranking'] as $user=>$score){
        print $i.". ".$user." ".$score."<br>";
        $i += 1;
    }
    print '</div>';

    print '<h1 class="rank-cup"><img src="cup.png" alt="The Soccer Ball"></h1>
    <br>
    <h1 class="rank-winner">Congratulations, '.ucfirst(array_keys($_SESSION['ranking'])[0]).'!</h1>
    <br>
    <h1 class="rank-win">You win!</h1>';
?>   


    
</body>
</html>
