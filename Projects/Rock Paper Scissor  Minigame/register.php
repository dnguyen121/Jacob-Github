<?php
session_start();
if(isset($_POST['submit'])){ // Fetching variables of the form which travels in URL
    //storing user's username
    $user = $_POST['Username'];
    //storing user's password
    $pwd = $_POST['Password'];

    $file = 'db.txt';
    // Open the file to get existing content
    $current = file_get_contents($file);
    // Append a new person to the file
    $current .= $user. ' ' .$pwd ."\n";
    // Write the contents back to the file
    file_put_contents($file, $current);

    //after registration
    header("location:login.php");/* Redirect to login page */
    exit();
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
<br><br>
<div class="login-wrap">
        <form action="register.php" method="post" name="Register_Form">
        <div class="container2">
            <h1>Register</h1>
            <p>Please fill in this form to create an account.</p>
            <hr class="hr1">

            <label for="Username"><b>Username</b></label>
            <input class="register-form-input" type="text" placeholder="Enter Username" name="Username" required>

            <label for="Password"><b>Password</b></label>
            <input class="register-form-input" type="password" placeholder="Enter Password" name="Password" required>
            <hr class="hr1">
            <button name="submit" type="submit" class="register-form-button button">Register</button>
            
        </div>
        
        <div class="container signin">
            <p>Already have an account? <a href="login.php">Sign in</a>.</p>
        </div>
        </form>
    </div>

</body>
</html>