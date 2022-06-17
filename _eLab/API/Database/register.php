<?php 
	include( dirname(__FILE__) . '/database.php');
	if (isset($_POST["fullname"]) && !empty($_POST["fullname"]) && 
		isset($_POST["email"]) && !empty($_POST["email"]) &&
        isset($_POST["password"]) && !empty($_POST["password"]) &&
        isset($_POST["cpassword"]) && !empty($_POST["cpassword"])){

        $fullname = $_POST["fullname"];
        $email = $_POST["email"];
        $password = $_POST["password"];
        $cpassword = $_POST["cpassword"];

        $sql = "SELECT email FROM user WHERE email='$email'";
        $result = $conn->query($sql);
        if($result !== TRUE){
            $sql = "INSERT INTO `user`(`email`, `name`, `password`, `isAuthorized`, `saved_archive`) VALUES ('$email','$fullname','$password',0,'')";
            $result = $conn->query($sql);
            if($result !== FALSE){
                echo "GRANTED:";
                echo "|REGISTRATION COMPLETE|";
                echo $fullname;
            }else{
                echo "MySQL ERROR";
            }
        }else {
            echo "Account already existed!";
        }
	}else echo "Form ERROR";
?>