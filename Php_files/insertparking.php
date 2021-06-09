<?php
        echo "<p>1<p>";
    // Configuration
    $hostname = 'localhost';
    $username = 'noworryp_wp939';
    $password = 'S]9Spyn71]';
    $database = 'noworryp_wp939';
 
    $secretKey = "noworrypSecretKey"; // Change this value to match the value stored in the client javascript below 
 
        try {
            $dbh = new PDO('mysql:host='. $hostname .';dbname='. $database, $username, $password);
        } catch(PDOException $e) {
            echo '<h1>An error has ocurred.</h1><pre>', $e->getMessage() ,'</pre>';
        }
        $id = $_GET['id'];
        $name = $_GET['name'];
        $location = $_GET['loc'];
        $lng =  $_GET['lng'];
        $lat =  $_GET['lat'];
        $nrloc = $_GET['nrloc'];
        $realHash = md5($name . $location . $secretKey); 
        
        if($realHash == $_GET['hash']) { //ca sa nu dea insert toata lumea doar noi din unity hashul trb sa fie 
            $data = [
                'id' => $id, 'name' => $name, 'loc' => $location,
            'lng' => $lng,'lat' => $lat, 'nrloc' => $nrloc
            ];
            $sth = $dbh->prepare('INSERT INTO Parcare VALUES (:id, :name, :lng, :lat, :nrloc, :loc)');
            try {
                $sth->execute($data);
            } catch(Exception $e) {
                echo '<h1>An error has ocurred.</h1><pre>', $e->getMessage() ,'</pre>';
            }
        } 
?>