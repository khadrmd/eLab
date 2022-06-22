<?php 
	include( dirname(__FILE__) . '/database.php');
    if(isset($_POST["title"]) && !empty($_POST["title"]) &&
    isset($_POST["desc"]) && !empty($_POST["desc"]) &&
    isset($_POST["type"]) && !empty($_POST["type"]) &&
    isset($_POST["date"]) && !empty($_POST["date"])){

        $title = $_POST["title"]; 
        $desc = $_POST["desc"]; 
        $type = $_POST["type"]; 
        $date = $_POST["date"];
        $author = $_POST["author"];

        $sql = "INSERT INTO `archive`(`title`, `desc`, `type`, `date`, `authorName`) VALUES ('$title','$desc','$type','$date', '$author')";
        $result = $conn->query($sql);
        if($result !== FALSE){
            echo "SUCCESS";
        }else echo $conn->error;
    }else echo "Form ERROR";
?>