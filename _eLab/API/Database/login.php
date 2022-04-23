<?php 
	include( dirname(__FILE__) . '/database.php');
	if (isset($_POST["email"]) && !empty($_POST["email"]) && 
		isset($_POST["password"]) && !empty($_POST["password"])){

        $email = $_POST["email"];
        $password = $_POST["password"];

        $sql = "SELECT id, name, email, password FROM user WHERE email='$email'";
        $result = $conn->query($sql);
        if($result !== FALSE){
            if ($result->num_rows > 0) {
                $row = $result->fetch_assoc();
                if($row["password"] != $password) echo "Password is Incorrect";
                else{
                    echo "GRANTED:";
                    echo "|";
                    echo $row["id"];
                    echo "|";
                    echo $row["name"];
                    echo "|";
                    echo $row["email"];
                }
            }else {
                echo "User Not Found";
            }
        }else echo "MySQL ERROR";
	}
?>