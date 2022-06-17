<?php 
	/** Login API - provides enpoint between user and the database and also used to authenticate a user
	  * @author Aulia Rahman Arif Wahyudi
	*/

	/**
	  * Accessed the database and check whether the email and password input are not empty
	*/
	include( dirname(__FILE__) . '/database.php');
	if (isset($_POST["email"]) && !empty($_POST["email"]) && 
		isset($_POST["password"]) && !empty($_POST["password"])){
	
	/**
	  * Assign the NON NULL email and password input to a variable 
	*/
        $email = $_POST["email"];
        $password = $_POST["password"];


        
	/**
	  * Access the database and authenticate whether the account
	  information submitted are listed in the database or not
	  * If incorrect, either shows "User Not Found" or "Password is Incorrect"
	  * If succeded, authenticate the user.
	*/
        $sql = "SELECT id, name, email, password, isAuthorized FROM user WHERE email='$email'";

        $result = $conn->query($sql);
        if($result !== FALSE){
            if ($result->num_rows > 0) {
                $row = $result->fetch_assoc();
                if($row["password"] != $password) echo "Password is Incorrect";
                else{
                    echo "GRANTED:";
                    echo "|";
                    echo $row["name"];
                    echo "|";
                    echo $row["email"];
                    echo "|";
                    echo $row["isAuthorized"];
                }
            }else {
                echo "User Not Found";
            }
        }else echo "MySQL ERROR";
	}
?>
