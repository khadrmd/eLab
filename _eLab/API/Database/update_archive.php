<?php 
	include( dirname(__FILE__) . '/database.php');
    if(isset($_POST["id"]) && !empty($_POST["id"])){

        $id = $_POST['id'];
        $title = $_POST['title'];
        $desc = $_POST['desc'];
        $type = $_POST['type'];
        $date = $_POST['date'];

        $sql = "UPDATE archive SET `title`='$title', `desc`='$desc', `type`='$type', `date`='$date' WHERE `archive_id`='$id'";
        $result = $conn->query($sql);
        if($result !== FALSE){
            echo "SUCCESS";
        }else echo $conn->error;
    }else echo "Form ERROR";
?>