<?php
?>

<!DOCTYPE html>
<html>
<head>
<meta charset="UTF-8">
<meta name="viewport" content="width=device-width, initial-scale=1.0">
<title>Registration</title>
<link rel="stylesheet" href="css/style.css" />
</head>
<style>
    body {
        background-image: linear-gradient(to right, #fa709a 0%, #fee140 100%);    }
</style>
<body>
<?php
	require('db.php');
    // If form submitted, insert values into the database.
    if (isset($_REQUEST['username'])){
		$username = stripslashes($_REQUEST['username']); // removes backslashes
		$username = mysqli_real_escape_string($con,$username); //escapes special characters in a string
		$email = stripslashes($_REQUEST['email']);
		$email = mysqli_real_escape_string($con,$email);
		$password = stripslashes($_REQUEST['password']);
		$password = mysqli_real_escape_string($con,$password);

        $trn_date = date("Y-m-d H:i:s");
        $query = "SELECT * from `users` where username = '$username'";
        $result = mysqli_query($con,$query);

        if (mysqli_num_rows($result) == 0) {
            $query = "INSERT into `users` (username, password, email, trn_date) VALUES ('$username', '".md5($password)."', '$email', '$trn_date')";
            $result = mysqli_query($con, $query);
            if (!$result) {
                echo 'Query Failed ';
            }
            if (mysqli_affected_rows($dbc) == 1) {
                //skip
            } else { // If it did run OK.
                echo "<div class='form'><h3>You are registered successfully.</h3><br/>Click here to <a href='login.php'>Login</a></div>";
                
                    /* $query2 = "SELECT * from `users` where username = '$username'";
                    $result2 = mysqli_query($con,$query2);
                    $to = $email;
                    $subject = "User Verification E-mail";
                    $message = '<?php while ($rows = mysql_fetch_assoc($result2)) {
                        echo rows["RandomNumber"];
                        echo rows["username"];
                        echo rows["email"];
                        echo rows["trn_date"];
                    }?>
                        ';

                    // Always set content-type when sending HTML email
                    $headers = "MIME-Version: 1.0" . "\r\n";
                    $headers .= "Content-type:text/html;charset=UTF-8" . "\r\n";

                    // More headers
                    $headers .= 'From: <harsh.jivani7.com>' . "\r\n";

                    mail($to,$subject,$message,$headers); */
            }
        }else { // The username is not available.
            echo "<div class='form'><h3>That username has already been registered.</h3><br/>Click here to <a href='registration.php'>Register again.</a></div>";
        }
    }else{
        ?>
<div class="form">
<h1>Registration</h1>
<form name="registration" action="" method="post">
<input type="text" name="username" placeholder="Username" required />
<input type="password" name="password" placeholder="Password" required />
<input type="email" name="email" placeholder="Email" required />
<input type="submit" name="submit" value="Register" />
</form>
    <p>Registered already? <a href='login.php'>Login Here</a></p>
    <br /><br />
</div>
<?php }mysql_close($connection); // Closing Connection with Server ?>
</body>
</html>
