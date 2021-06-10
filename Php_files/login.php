<?php
<<<<<<< HEAD
        echo "<p>1<p>";
    // Configuration
=======
       
    // Configuration
    //
>>>>>>> Paula
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
        $email = $_GET['email'];
        $parola =  $_GET['parola']; //parola o primesc cu hash deja
<<<<<<< HEAD
        $realHash = md5($prenume . $nume . $email . $secretKey); 
        

$sql2 = "SELECT id, email, parola from  Utilizator where email = ?";
        if($stmt = mysqli_prepare($link, $sql2))//daca nu e logat cu username -ul poate e cu email
        {
            mysqli_stmt_bind_param($stmt2, "s", $param_email);
            $param_email = $username;
            
            if(mysqli_stmt_execute($stmt2))
            { 
                mysqli_stmt_store_result($stmt2);
=======
        //hashul din unity
        $realHash = md5($email . $parola . $secretKey); 
        

        $sql = "SELECT id  from  Utilizator where email = :mail AND parola = :parola";

        $stmt = $dbh->prepare($sql);
        $stmt->bindParam(':mail', $email, PDO::PARAM_STR);
        $stmt->bindParam(':parola', $email, PDO::PARAM_STR);
        $stmt->execute();


        if($stmt -> rowCount() > 0){

            echo "1"; // nu s-a gasit id-ul respectiv

        }
        else{
            echo "0"; //s-a gasit deci se poate face login
        }
        

>>>>>>> Paula

?>