<?php
    include 'config.php';
    
    $userName = $_GET['user'];
    $pass = $_GET['pass'];
    
    $startX = 0;
    $startY = 0;
    $startZ = 0;


    $sql = "INSERT INTO Users(Username, Pass) VALUES('$userName', md5('$pass'))";
    
    if ($conn->query($sql) === TRUE) {
        echo "New record created successfully<br>";
    }
    else {
        echo "Error: " . $sql . "<br>" . $conn->error;
    }
    
    $sql = "INSERT INTO Stats(Username, hp, level, xp, posX, posY, posZ)    VALUES('$userName', '100', '1', '0', '$startX', '$startY', '$startZ')";
    
    if ($conn->query($sql) === TRUE) {
    echo "New record created successfully";
    } else {
        echo "Error: " . $sql . "<br>" . $conn->error;
    }
?>