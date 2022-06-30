<?php 
	include( dirname(__FILE__) . '/database.php');
    if(isset($_POST["keyword"]) && isset($_POST["date"]) && isset($_POST["filter"])){

        $keyword = $_POST["keyword"];
        $date = $_POST["date"]; 
        $filter = $_POST["filter"];

        $sql = "SELECT * FROM archive";
        
        if(!empty($_POST["keyword"]) || !empty($_POST["date"]) || !empty($_POST["filter"])){
            $sql .= " WHERE ";
            if(!empty($_POST["keyword"])){
                $sql .= "title LIKE '%$keyword%'";
            }
            if(!empty($_POST["date"]) && empty($_POST["keyword"])){
                $sql .= "DATE(date)='$date'";
            }else if(!empty($_POST["date"]) && !empty($_POST["keyword"])){
                $sql .= " OR DATE(date)='$date'";
            }
            if(!empty($_POST["filter"]) && empty($_POST["date"])){
                $sql .= "type='$filter'";
            }else if(!empty($_POST["filter"]) && !empty($_POST["date"])){
                $sql .= " OR type='$filter'";
            }
        }

        $result = $conn->query($sql);
        if($result !== FALSE){
            if ($result->num_rows > 0) {
                $counter = 0;
                while($row = $result->fetch_assoc()){
                    echo $row["archive_id"];
                    echo "|";
                    echo $row["title"];
                    echo "|";
                    echo $row["desc"];
                    echo "|";
                    echo $row["type"];
                    echo "|";
                    echo $row["authorName"];
                    echo "|";
                    echo $row["date"];
                    echo "|";
                    if(empty($row["image"])) echo "null";
                    else echo base64_encode($row['image']);
                        
                    if (++$counter < $result->num_rows) echo "#";
                }
            }else echo "No Archives Found!";
        }else echo "MySQL ERROR";
    }else echo "Form ERROR";
?>