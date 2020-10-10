<?php
session_start();
if(isset($_POST['Submit'])){
    //$logins = array('harsh' => 'jivani','jacob' => 'nguyen','sunny' => 'patel','guest' => 'guest');
    // getData('db.txt') returns an associative array
    $file = 'db.txt';
    
    $data = file($file);
    $logins = array();
    foreach($data as $line) {
        $explode = explode(" ", $line);
        $logins[$explode[0]] = $explode[1];
    }   
    
    $strTemp = trim(file_get_contents('rank.txt'));
    $_SESSION['tempArray'] = explode(" ",$strTemp);
    $_SESSION['Username'] = isset($_POST['Username']) ? $_POST['Username'] : '';
    $_SESSION['Password'] = isset($_POST['Password']) ? $_POST['Password'] : '';
    $_SESSION['ranking'] = array();
    if(strcmp($strTemp,'') != 0){
        for($i = 0 ; $i< count($_SESSION['tempArray']); $i+=2){
            $array = array();
            $array[$_SESSION['tempArray'][$i]] = $_SESSION['tempArray'][$i+1];
            $_SESSION['ranking'] = array_merge($_SESSION['ranking'],$array);
        }
    } else {
        $_SESSION['ranking'][$_SESSION['Username']] = 0;
    }

    $_SESSION['numbWin'] = 0;
    if (isset($logins[$_SESSION['Username']]) && $logins[$_SESSION['Username']].strcmp($_SESSION['Password'])==0){
        if (array_key_exists($_SESSION['Username'],$_SESSION['ranking'])){
            $_SESSION['numbWin'] = $_SESSION['ranking'][$_SESSION['Username']];
        }
        header("location:index.php");
        exit();
    } else {
        $msg="<h1 style='color:red; text-align: center; font-weight:bold'>Invalid Login Details</h1>";
        echo $msg;
    }
}
?>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Login Page</title>
    <link rel="stylesheet" href="styles.css">
</head>

<body>
<br><br><br>
<div class="login-wrap">
        <form class="login-form" action="" method="post" name="Login_Form">
            <div class="login-form-header">
                <h3 style="color:red">Rock, Paper, Scissors!</h3>
                <p>Made by: Jacob Nguyen, Sunny Patel, Harsh Jivani</p>
            </div>
            <label for="Username"><b>Username</b></label>
            <div class="login-form-group">
                <input type="text" name="Username" class="login-form-input" placeholder="username" required="" autofocus="">
            </div>
            <!--Password Input-->
            <label for="Password"><b>Password</b></label>
            <div class="login-form-group">
                <input type="password" class="login-form-input" name="Password" placeholder="password" required="">
            </div>
            <!--Login Button-->
            <div class="login-form-group">
                <button class="login-form-button button" name="Submit" value="Login" type="submit">Log in</button>
            </div>
            Need an account? <a style="text-decoration: none;" href="register.php">Click to Register</a>
            <br><br><hr><br>
            ************* Rules: *************<br>
            Select either Rock, Paper, or Scissors. <br> * Rock wins against scissors. <br> * Scissors win against paper. <br> * Paper wins against rock.
            </p>
        </form>
        
    </div>

</body>
</html>