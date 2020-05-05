<?php

require 'Meli/meli.php';
require 'configApp.php';
require_once 'Meli/Connection.php';

if (isset($_POST['question_id'], $_POST['text'], $_POST['access_token'])) {
    $question_id = $_POST["question_id"];
    $text = $_POST["text"];
    $access_token = $_POST['access_token'];

    $answer = [
        "question_id" => $question_id,
        "text" => $text
    ];

    try {
        $meli = new Meli($appId, $secretKey);
        $response = $meli->post('/answers', $answer, array('access_token' => $access_token));
        if(isset($response['body']->status) && isset($response['body']->error) && $response['body']->status != 200)
            throw new Exception($response['body']->message);
        echo "Resposta enviada com sucesso !";
    }
    catch (Exception $e) {
        echo "Erro: ". $e->getMessage();
    }
}
else {
    echo "Requisição inválida";
}

?>

