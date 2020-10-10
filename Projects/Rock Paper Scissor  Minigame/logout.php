<?php
session_start(); /* Starts the session */

$_SESSION['ranking'][$_SESSION['Username']] = $_SESSION['numbWin'];
$strTemp2 = '';
foreach ($_SESSION['ranking'] as $user=>$score){
    $strTemp2 = $strTemp2." ".$user." ".$score;
}

file_put_contents('rank.txt',$strTemp2);

session_destroy(); /* Destroy started session */
header("location:login.php");/* Redirect to login page */
exit();
?>



<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Logout</title>
</head>
<body>
</body>
</html>