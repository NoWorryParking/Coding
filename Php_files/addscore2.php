<?php
        // Configuration
        $hostname = 'sql108.byetcluster.com';
        $username = '28759885_1';
        $password = 'p7-0.68SGz';
        $database = 'unaux_28759885_691';
 
        $secretKey = "noworrypSecretKey"; // Change this value to match the value stored in the client javascript below 
 
        try {
            $dbh = new PDO('mysql:host='. $hostname .';dbname='. $database, $username, $password);
        } catch(PDOException $e) {
            echo '<h1>An error has ocurred.</h1><pre>', $e->getMessage() ,'</pre>';
        }
 
        $realHash = md5($_GET['name'] . $_GET['score'] . $secretKey); 
        if($realHash == $hash) { 
            $sth = $dbh->prepare('INSERT INTO scores VALUES (null, :name, :score)');
            try {
                $sth->execute($_GET);
            } catch(Exception $e) {
                echo '<h1>An error has ocurred.</h1><pre>', $e->getMessage() ,'</pre>';
            }
        } 
?>