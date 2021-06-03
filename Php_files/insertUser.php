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
        $nume = $_GET['nume'];
        $prenume = $_GET['prenume'];
        $email = $_GET['email'];
        $parola =  $_GET['parola']; //parola o primesc cu hash deja
        $realHash = md5($prenume . $nume . $email . $secretKey); 
        
        if($realHash == $_GET['hash']) { //ca sa nu dea insert toata lumea doar noi din unity hashul trb sa fie 
            $data = [
                'nume' => $nume, 'pren' => $prenume,
            'mail' => $email,'parola' => $parola
            ];
            $sth = $dbh->prepare('INSERT INTO Utilizator(nume, prenume, email, parola) VALUES (:nume, :pren, :mail, :parola)');
            try {
                $sth->execute($data);
            } catch(Exception $e) {
                echo '<h1>An error has ocurred.</h1><pre>', $e->getMessage() ,'</pre>';
            }
        } 
?>