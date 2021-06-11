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


        $email = $_GET['email'];

        $realHash = md5($email . $secretKey); 

        $dataCurenta = new DateTime('now', new DateTimeZone('Europe/Bucharest'));
        $dataC = $dataCurenta->format('Y-m-d H:i:s');
        //parcarile active au data actuala intre dataRezevare si dataRezervare finala ( adica + nrore) 

        if($realHash == $_GET['hash']) {
        //intai trb sa iau id-ul userului cu acel email
        $sql = "SELECT id  from  Utilizator where email = :mail"; //emailul e unic

        $stmt = $dbh->prepare($sql);
        $stmt->bindParam(':mail', $email, PDO::PARAM_STR);
        $stmt->execute();
        if($stmt ->rowCount() == 1)
        {
            $row  = $stmt->fetch(PDO::FETCH_BOTH);
            $idUser = $row[0];
            echo $idUser;
            $sql2 = "SELECT (Select nume from Parcare p where p.id = r.idParcare) as numeParcare, dataRezevare, durataRezervare from Rezervare r where idUser = :id and (STR_TO_DATE(:dataC, '%Y-%m-%d %H:%i:%s') between dataRezevare and DATE_ADD(dataRezevare, INTERVAL +durataRezervare HOUR_SECOND))";
            $stmt2 = $dbh->prepare($sql2);
            $stmt2->bindParam(':id', $idUser, PDO::PARAM_INT);
            $stmt2->bindParam(':dataC', $dataC, PDO::PARAM_STR);
            $stmt2->execute();
            if($stmt2 ->rowCount() == 0)
            {
                echo "nu aveti parcari active";
            }
            else{
            $result = $stmt2->fetchAll();
            // foreach($result as $row) {
            //     // also amend here to address the contents of the allRows i.e. $row as objects
            //   echo 'Nume parcare: ' . $row->nume . 'data rezervarii: ' . $row->dataRezevare . ' durata rezervarii '. $row->durataRezervare . '<br />';
            // }
            echo json_encode($result);
            }
        }
        else{
            echo "utilizator invalid";
        }
    }


?>