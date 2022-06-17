<?php
    // class DB{
        // private static $instance = null;

    $servername = "localhost";
    $username = "root";
    $password = "";
    $dbname = "rpl_elab";

    // Create connection
    $conn = new mysqli($servername, $username, $password, $dbname);
    // Check connection
    if ($conn->connect_error) {
        die("Connection failed: " . $conn->connect_error);
    }

        // public static function getInstance(){
        //     if(!self::$instance){
        //         self::$instance = new DB();
        //     }
        //     return self::$instance;
        // }
    // }
?>
