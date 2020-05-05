<?php
require 'Meli/meli.php';
require 'configApp.php';

$meli = new Meli($appId, $secretKey);
if (isset($_POST['access_token'])) {
    $response = $meli->get('/users/me', array('access_token' => $_POST['access_token']));
    
    $id_conta = $response['body']->id;
    $url = 'questions/search';
    $response = "";
    $params = [
        'seller_id' => $id_conta,
        'status' => 'UNANSWERED',
        'access_token' => $_POST['access_token'],
    ];
  
    $response = $meli->get($url, $params);
    $qtd_perguntas = $response['body']->total;
    $perguntas = $response['body']->questions;

    $array_retorno = array();
    $i = 0;

    if (is_int($qtd_perguntas) && $qtd_perguntas > 0 && !empty($perguntas)): 
        foreach($perguntas as $pergunta_produto) {
            $product_url = "";
            $url = '/items/' . $pergunta_produto->item_id;
            $anuncio = "";
            $anuncio = $meli->get($url, array('access_token' => $_POST['access_token']));
            $nome = $anuncio['body']->title;

            $array_retorno[$i]['pergunta_id'] = $pergunta_produto->id;
            $array_retorno[$i]['pergunta_texto'] = $pergunta_produto->text;
            $array_retorno[$i]['produto_nome'] = $nome;
            
            $i++;
        }
    $json_retorno = json_encode($array_retorno, JSON_UNESCAPED_SLASHES | JSON_UNESCAPED_UNICODE);
    print_r($json_retorno);
    else: 
        echo "Nenhuma pergunta para listar!";             
    endif; 
    
   
}
else {
    echo "Sem acesso ! ";
}
?>






