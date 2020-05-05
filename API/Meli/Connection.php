<?php
 
    $dsn = "mysql:dbname=ni5j59u7t7s5hnxj;host=un0jueuv2mam78uv.cbetxkdyhwsb.us-east-1.rds.amazonaws.com";
    $user = 'osjbrvcac0xin80x';
    $password = 'z3raw5tibpopobio';
    $conn = 2;

    try {
        $conn = new PDO ("$dsn", $user, $password);
        $conn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);    
    } catch (PDOException $e) {
        echo 'Connection failed: ' . $e->getMessage();
    }
?>