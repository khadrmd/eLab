<?php 
	include( dirname(__FILE__) . '/database.php');
    if(isset($_POST["id"]) && !empty($_POST["id"])){

        $id = $_POST["id"];

        $sql = "DELETE FROM archive WHERE archive_id='$id'";
        $result = $conn->query($sql);
        if($result !== FALSE){
            echo "SUCCESS";
        }else echo $conn->error;
    }else echo "Form ERROR";
?>