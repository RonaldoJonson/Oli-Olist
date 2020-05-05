<?php
require_once 'Meli/Connection.php';

try {
    $consulta = $conn->query("SELECT token FROM info;");
    while ($linha = $consulta->fetch(PDO::FETCH_ASSOC)) {
        echo $linha['token'];
    }
} 
catch(PDOException $e) {
    echo $sql . "<br>" . $e->getMessage();
}

?>