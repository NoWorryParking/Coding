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

        $zi = $_GET['zi'];
        $luna = $_GET['luna'];
        $an = $_GET['an'];
        $ora =  $_GET['ora']; 
        $min = $_GET['min'];
        $nrore = $_GET['nrore'];
        $email = $_GET['email'];
        $parcare = $_GET['idParcare'];

        $realHash = md5($zi . $luna. $an . $ora . $min . $email . $secretKey); 
        
        if($realHash == $_GET['hash']) { //ca sa nu dea insert toata lumea doar noi din unity hashul trb sa fie 

            //intai trb sa iau id-ul userului cu acel email
            $sql = "SELECT id  from  Utilizator where email = :mail"; //emailul e unic

            $stmt = $dbh->prepare($sql);
            $stmt->bindParam(':mail', $email, PDO::PARAM_STR);
            $stmt->execute();
          

            $sql2 = "SELECT 1  from Parcare where id = :id"; //emailul e unic

            $stmt2 = $dbh->prepare($sql2);
            $stmt2->bindParam(':id', $parcare, PDO::PARAM_STR);
            $stmt2->execute();

            //daca nu gasesc utilizatorul cu acest email sau nu gasesc parcarea cu acel id
            if($stmt ->rowCount() == 1 && $stmt2 ->rowCount() == 1){
                $row  = $stmt->fetch(PDO::FETCH_BOTH);
                $idUser = $row[0];
                //echo $row[0]; //id-ul userului
                
                //'YYYY-MM-DD hh:mm:ss' datetime format in mysql

            $dataI =  new DateTime( $an . '-' . $luna . '-' . $zi . ' ' . $ora . ':' . $min . ':00');
            $h = intval($nrore);
            $datafinala = $dataI->add(new DateInterval("PT{$h}H"));
            //$datafinala = date_modify($date, "+{$h} hour");
            $dataCurenta = new DateTime('now', new DateTimeZone('Europe/Bucharest'));
            $dataC = $dataCurenta->format('Y-m-d H:i:s');
            $dataI = (new DateTime( $an . '-' . $luna . '-' . $zi . ' ' . $ora . ':' . $min . ':00'))-> format('Y-m-d H:i:s');
            $dataF = $datafinala->format('Y-m-d H:i:s');

            $nrore = $nrore . ':00:00'; //ore nu secunde 

            //verific daca mai sunt locuri cand vrea el sa faca rezevarea
           // echo $dataI . ' ---  '. $dataF . "\n";

            $sql3 = "SELECT COUNT(*)  from  Rezervare where idParcare = :idP AND dataRezevare BETWEEN STR_TO_DATE(:dataR, '%Y-%m-%d %H:%i:%s') AND STR_TO_DATE(:datafinala, '%Y-%m-%d %H:%i:%s')"; 
            $stmt3 = $dbh->prepare($sql3);
            $stmt3->bindParam(':idP', $parcare, PDO::PARAM_STR);
            $stmt3->bindParam(':dataR', $dataI, PDO::PARAM_STR);
            $stmt3->bindParam(':datafinala', $dataF, PDO::PARAM_STR);
            $stmt3->execute();
            $reze  = $stmt3->fetch(PDO::FETCH_BOTH);

            $sql4 = "SELECT nrtotallocuri FROM Parcare where id = :idP";
            $stmt4 = $dbh->prepare($sql4);
            $stmt4->bindParam(':idP', $parcare, PDO::PARAM_STR);
            $stmt4->execute();
            $locuri = $stmt4->fetch(PDO::FETCH_BOTH);

            //echo 'locuri rezervate: '. $reze[0] . "\n";
            //echo 'locuri totale: '. $locuri[0] . "\n";

            if($locuri[0] - $reze[0] == 0){
                echo "nu mai sunt locuri la ora si minutul selectate";

            } 
            else{           

                $data = [
                        'id' => $idUser, 'idP' => $parcare,
                        'durata' => $nrore, 'dataR' => $dataC,//date_default_timezone_get(),
                        'dataPentruR' => $dataI
                    ];
                    $sth = $dbh->prepare('INSERT INTO Rezervare(idUser, idParcare, data, dataRezevare, durataRezervare) VALUES (:id, :idP, :dataR, :dataPentruR, :durata)');
                    try {
                        $sth->execute($data);
                    } catch(Exception $e) {
                        echo '<h1>An error has ocurred.</h1><pre>', $e->getMessage() ,'</pre>';
                    }

                    echo "ok";
        }
            }
            else{
                echo "rezevare incorecta";
            }

            
            
        } 
?>