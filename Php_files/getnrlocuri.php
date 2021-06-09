<?php
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
        date_default_timezone_set('Europe/Bucharest');

        $parcare = $_GET['idParcare'];
        echo $parcare;
        $sql2 = "SELECT 1  from Parcare where id = :id"; //emailul e unic

            $stmt2 = $dbh->prepare($sql2);
            $stmt2->bindParam(':id', $parcare, PDO::PARAM_STR);
            $stmt2->execute();
        
            if($stmt2 -> rowCount() == 1){
                echo "exista parcarea " . $parcare . "\n";
                $sql4 = "SELECT nrtotallocuri FROM Parcare where id = :idP";
                            $stmt4 = $dbh->prepare($sql4);
                            $stmt4->bindParam(':idP', $parcare, PDO::PARAM_STR);
                            $stmt4->execute();
                            $locuri = $stmt4->fetch(PDO::FETCH_BOTH);

                echo "nr locuri: " . $locuri[0];
            }
            else{echo "nu exista parcarea " . $parcare . "\n";}

            //reservation.php?email=ana@mail.com&idParcare=ChIJp0tnOL3_sUARP2qzWoeWHNY&zi=1&luna=2&an=3&nrore=2&ora=10&min=12

?>

