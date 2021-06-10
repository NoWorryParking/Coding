<?php
    // Configuration
    $hostname = 'localhost';
    $username = 'noworryp_wp939';
    $password = 'S]9Spyn71]';
    $database = 'noworryp_wp939';
 
    try {
        $dbh = new PDO('mysql:host='. $hostname .';dbname='. $database, $username, $password);
    } catch(PDOException $e) {
        echo '<h1>An error has occurred.</h1><pre>', $e->getMessage() ,'</pre>';
    }
    
    
    $dataCurenta = new DateTime('now', new DateTimeZone('Europe/Bucharest'));
    $dataC = $dataCurenta->format('Y-m-d H:i:s');
    $datafinala = (new DateTime('now', new DateTimeZone('Europe/Bucharest'))) ->add(new DateInterval("PT2H"));
    $dataF = $datafinala->format('Y-m-d H:i:s');

    $data = ['llng' => $_GET['lng']-1, 'llat' => $_GET['lat']-1, 'ulng'  => $_GET['lng']+1, 'ulat' => $_GET['lat']+ 1, 'dataR' => $dataC, 'datafinala' => $dataF];

    $sth = $dbh->prepare("SELECT p.*, (SELECT p.nrtotallocuri - COUNT(*) FROM `Rezervare` WHERE idParcare= p.id AND dataRezevare BETWEEN STR_TO_DATE(:dataR, '%Y-%m-%d %H:%i:%s') AND STR_TO_DATE(:datafinala, '%Y-%m-%d %H:%i:%s') ) as nrlocurilibere FROM `Parcare` p WHERE p.logitudine BETWEEN :llng AND :ulng AND p.latitudine BETWEEN :llat AND :ulat");
    //SELECT *, () FROM Parcare WHERE logitudine BETWEEN :llng AND :ulng AND latitudine BETWEEN :llat AND :ulat');
    $sth -> execute($data);
    //$sth->debugDumpParams();
    $result = $sth->fetchAll();

    echo json_encode($result);
?>