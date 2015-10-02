<?php
    include 'config.php';
    
    $userName = $_GET['user'];
    $pass = $_GET['pass'];
    
    $PlayerId;
    
    $query = "SELECT id FROM Users WHERE Username = '$userName' AND Pass = '$pass'";
    
    $result = mysqli_query($conn, $query)
    or die('Error: Could not connect to the database');
    
    while ($row = mysqli_fetch_array($result))
    {
        $PlayerId =$row['id'];        
        
        $query = "SELECT * FROM Stats WHERE id = '$PlayerId'";
        
        $result = mysqli_query($conn, $query)
        or die('Error: Could not connect to the database');
        
        while ($row = mysqli_fetch_array($result))
        {
            echo $row['hp'] . "<br>";
            echo $row['level'] . "<br>";
            echo $row['xp'] . "<br>";
            echo $row['posX'] . "<br>";
            echo $row['posY'] . "<br>";
            echo $row['posZ'] . "<br>";
            
        }

    }
?>