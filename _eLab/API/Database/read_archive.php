<?php 
	include( dirname(__FILE__) . '/database.php');
    if(isset($_POST["date"]) && isset($_POST["filter"])){
        $date = $_POST["date"]; 
        $filter = $_POST["filter"];

        $sql = "";

        if(empty($_POST["date"]) && empty($_POST["filter"])){
            $sql = "SELECT * FROM archive";
        }else if(!empty($_POST["date"]) && !empty($_POST["filter"])){
            $sql = "SELECT * FROM archive WHERE date='$date' AND type='$filter'";
        }else if(empty($_POST["date"]) && !empty($_POST["filter"])){
            $sql = "SELECT * FROM archive WHERE type='$filter'";
        }else if(!empty($_POST["date"]) && empty($_POST["filter"])){
            $sql = "SELECT * FROM archive WHERE date='$date'";
        }

        $result = $conn->query($sql);
        if($result !== FALSE){
            if ($result->num_rows > 0) {
                $counter = 0;
                while($row = $result->fetch_assoc()){
                    echo $row["title"];
                    echo "|";
                    echo $row["desc"];
                    echo "|";
                    echo $row["type"];
                    echo "|";
                    echo $row["authorName"];
                    echo "|";
                    if(empty($row["image"])) echo "null";
                    else echo base64_encode($row['image']);
                        
                    if (++$counter < $result->num_rows) echo "#";
                }
            }else echo "No Archives Found!";
        }else echo "MySQL ERROR";
    }else echo "Form ERROR";
?>